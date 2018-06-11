using System;
using System.Linq;
using System.Timers;
using ApiCore.DB;
using ApiCore.JwtSecurity;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Load the Jwt Configuration from the appsettings.json (See 'JwtConfiguration' section in appsettings.json)
            JwtTokenDefinitions.LoadFromConfiguration(Configuration);
            //Configuring Authentication
            services.ConfigureJwtAuthentication();
            //Configuring Authorization
            services.ConfigureJwtAuthorization();
            services.AddMvc();
            DataConnection.DefaultSettings = new LinqToDbSettings();
            SetTimer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        private static Timer aTimer;
        private static bool isRunning;

        private static void SetTimer()
        {
            aTimer = new Timer(10000);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if(isRunning)
                return;
            isRunning = true;
            var checkDate = DateTime.Now;
            using (var db = new PeerDb())
            {
                var bindings = db.ArticleToUserBindings.Where(b =>
                    b.Deadline <= checkDate && !b.Accepted.HasValue);
                if (bindings.Count() != 0)
                {
                    var uIds = bindings.Select(b => b.UserId).ToList();
                    bindings.Set(b => b.Accepted, false).Update();
                    var users = db.Users.Where(u => uIds.Contains(u.Id)).ToList();
                    foreach (var user in users)
                    {
                        user.Rating -= 1;
                        db.Update(user);
                    }
                }
            }
        }
    }
}
using System;
using ApiCore.DB;
using ApiCore.DTO;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Регистрируем нового пользователя.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            try
            {
                using (var db = new PeerDb())
                {
                    db.Insert(new BO.User(user));
                }
            }
            catch (Exception e)
            {
                return BadRequest($"{e.Message}");
            }

            var jwtSecurity = new JwtSecurity.JwtSecurity();
            return Ok(jwtSecurity.Login(user));
        }

        [Route("token")]
        [HttpPost]
        public IActionResult Token([FromBody] User user)
        {
            var jwtSecurity = new JwtSecurity.JwtSecurity();
            return Ok(jwtSecurity.Login(user));
        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using ApiCore.DB;
using User = ApiCore.DTO.User;

namespace ApiCore.Converters
{
    public static class UserConverter
    {
        public static User ToDto(this BO.User user)
        {
            using (var db = new PeerDb())
            {
                return new User
                {
                    Id = user.Id,
                    Name = user.Name,
                    ReferralId = user.ReferralId,
                    Articles = db.Articles
                        .Where(a => a.UserId == user.Id)
                        .Select(x => x.ToDto()).ToList(),
                    RegisteredOn = user.RegisteredOn.ToString("dd/MM/yyyy"),
                    Rating = user.Rating,
                    Reviews = db.Reviews
                        .Where(a => a.UserId == user.Id)
                        .Select(x => x.ToDto()).ToList(),
        
                };
            }
        }

        public static BO.User ToBo(this User user)
        {
            return new BO.User
            {
                Id = user.Id,
                Name = user.Name,
                ReferralId = user.ReferralId,
                RegisteredOn = DateTime.ParseExact(user.RegisteredOn, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                Rating = user.Rating
            };
        }
    }
}
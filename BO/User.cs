using System;
using System.Linq;
using ApiCore.DB;
using LinqToDB.Mapping;

namespace ApiCore.BO
{
    [Table("User")]
    public class User
    {
        public User()
        {
        }

        public User(DTO.User user)
        {
            using (var db = new PeerDb())
            {
                if (string.IsNullOrEmpty(user.Name))
                    throw new NullReferenceException(user.Name);
                if(string.IsNullOrEmpty(user.Password))
                    throw new NullReferenceException(user.Password);
                if (db.Users.Any(u => u.Name == user.Name))
                    throw new Exception($"User with name {user.Name} already exists.");
                if (user.ReferralId.HasValue && !db.Users.Any(u => u.Id == user.ReferralId))
                    throw new Exception($"User with id {user.Id} does not exist.");
            }

            Id = Guid.NewGuid();
            Name = user.Name;
            ReferralId = user.ReferralId;
            RegisteredOn = DateTime.Now;
            Password = user.Password;
        }
        [Column, NotNull, PrimaryKey] public Guid Id { get; set; }
        [Column, NotNull] public int Rating { get; set; }
        [Column, NotNull] public string Name { get; set; }
        [Column, Nullable] public Guid? ReferralId { get; set; }
        [Column, NotNull] public DateTime RegisteredOn { get; set; }
        [Column, NotNull] public string Password { get; set; }
    }
}
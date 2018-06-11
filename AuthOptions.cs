using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ApiCore
{
    public class AuthOptions
    {
        public const string Issuer = "MyAuthServer"; // издатель токена
        public const string Audience = "http://localhost:62664/"; // потребитель токена
        private const string Key = "mysupersecret_secretkey!123";   // ключ для шифрации
        public const int Lifetime = 10; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}

using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MedicalLaboratory20.WebAPI.Helpers
{
    public class AuthorizationOptions
    {
        private const string KEY = "MD20_C73DEE3A92CD854EE7BBCF76D4488";
        public const string ISSUER = "MedicalLaboratory20.Auth";    // Издатель
        public const string AUDIENCE = "MedicalLaboratory20.User";  // Потребитель
        public const int LIFETIME = 1;                              // Время жизни (в мин)

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

    }
}

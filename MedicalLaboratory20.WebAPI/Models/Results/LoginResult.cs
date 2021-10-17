using System.Text.Json.Serialization;

namespace MedicalLaboratory20.WebAPI.Models.Results
{
    public class LoginResult
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Login)}:{Login}\r\n" +
                $"{nameof(Name)}:{Name}\r\n" +
                $"{nameof(Role)}:{Role}\r\n" +
                $"{nameof(AccessToken)}:{AccessToken}\r\n" +
                $"{nameof(RefreshToken)}:{RefreshToken}\r\n";
        }
    }
}

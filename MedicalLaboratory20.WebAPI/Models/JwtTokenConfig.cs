using System.Text.Json.Serialization;

namespace MedicalLaboratory20.WebAPI.Models
{
    public class JwtTokenConfig
    {
        [JsonPropertyName("secret")]
        public string Secret { get; set; }

        [JsonPropertyName("issuer")]
        public string Issuer { get; set; }

        [JsonPropertyName("audience")]
        public string Audience { get; set; }

        [JsonPropertyName("accessTokenLifetime")]
        public int AccessTokenLifetime { get; set; }

        [JsonPropertyName("refreshTokenLifetime")]
        public int RefreshTokenLifetime { get; set; }
    }
}

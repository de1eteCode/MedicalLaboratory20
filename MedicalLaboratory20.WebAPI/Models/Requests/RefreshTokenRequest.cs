using System.Text.Json.Serialization;

namespace MedicalLaboratory20.WebAPI.Models.Requests
{
    public class RefreshTokenRequest
    {
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }
    }
}

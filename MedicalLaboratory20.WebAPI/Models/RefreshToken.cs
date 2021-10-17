using System;
using System.Text.Json.Serialization;

namespace MedicalLaboratory20.WebAPI.Models
{
    public class RefreshToken
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("expireAt")]
        public DateTime ExpireAt { get; set; }
    }
}
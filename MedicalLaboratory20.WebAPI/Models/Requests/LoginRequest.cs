using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace MedicalLaboratory20.WebAPI.Models.Requests
{
    public class LoginRequest
    {
        [Required]
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}

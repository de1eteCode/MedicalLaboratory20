using DataModels.Abstract;
using System;
using System.Text.Json.Serialization;

namespace DataModels.Entities
{
    public class Auth : BaseEntity
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
    }
}

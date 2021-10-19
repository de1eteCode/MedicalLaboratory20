using System;
using System.Text.Json.Serialization;

namespace SharedModels
{
    public class LogingAuth
    {
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [JsonPropertyName("time")]
        public DateTime Date { get; set; }

        [JsonPropertyName("result")]
        public string Result { get; set; }
    }
}

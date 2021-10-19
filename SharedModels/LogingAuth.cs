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

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonIgnore]
        public object this[string propertyName]
        {
            get
            {
                var propertyInfo = this.GetType().GetProperty(propertyName);
                return propertyInfo.GetValue(this, null);
            }
        }
    }
}

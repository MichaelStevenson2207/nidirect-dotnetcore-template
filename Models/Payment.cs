using Newtonsoft.Json;

namespace nidirect_app_frontend.Models
{
    public sealed class Payment
    {
        [JsonProperty("amount")]
        public int Amount { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("return_url")]
        public string ReturnUrl { get; set; }
    }
}
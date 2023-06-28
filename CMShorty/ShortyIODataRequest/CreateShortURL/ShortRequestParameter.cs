namespace ShortyIODataRequest.CreateShortURL
{
    using Newtonsoft.Json;

    public class ShortRequestParameter
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }

        [JsonProperty("allowDuplicates")]
        public bool AllowDuplicates { get; set; }

        [JsonProperty("originalURL")]
        public string OriginalURL { get; set; }

        [JsonProperty("redirectType")]
        public int RedirectType { get; set; }

        [JsonIgnore]
        public string Authorization { get; set; }
    }
}

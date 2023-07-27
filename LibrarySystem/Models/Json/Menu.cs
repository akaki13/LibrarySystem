using Newtonsoft.Json;

namespace LibrarySystem.Models.Json
{
    public class Menu
    {
        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("Title")]
        public string Title;

        [JsonProperty("Url")]
        public string Url;

        [JsonProperty("ParentId")]
        public int? ParentId;

        [JsonProperty("Role")]
        public string Role;
    }
}

using Newtonsoft.Json;

namespace LibrarySystem.Models.Json
{
    public class Menu
    {
        [JsonProperty("Id")]
        public int Id;

        [JsonProperty("Title")]
        public string Title;

        [JsonProperty("ParentId")]
        public int? ParentId;
    }
}


using LibrarySystem.Data;
using LibrarySystem.Models.Json;
using Newtonsoft.Json;

namespace LibrarySystem.Util
{
    public static class JsonUtil
    {
        public static List<Menu> TakeBookData()
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataUtil.JsonPath);
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Menu>>(json);
            }

        }
    }
}

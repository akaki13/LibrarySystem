
using LibrarySystem.Data;
using LibrarySystem.Models.Json;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace LibrarySystem.Util
{
    public static class JsonUtil
    {
        public static List<Menu> TakeBookData()
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataUtil.NavJsonPath);
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<List<Menu>>(json);
            }
        }

        public static string SerializeObject<T>(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}

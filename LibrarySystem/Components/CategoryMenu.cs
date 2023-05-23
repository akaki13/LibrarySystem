using LibrarySystem.Data;
using LibrarySystem.Models.Json;
using LibrarySystem.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.IO;

namespace LibrarySystem.Components
{
    public class CategoryMenu : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            List<Menu> menu = new List<Menu>();
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DataUtil.JsonPath);
            using (StreamReader r = new StreamReader(filepath))
            {
                string json = r.ReadToEnd();
                menu = JsonConvert.DeserializeObject<List<Menu>>(json);

            }
            
            return View(menu);
        }
        /*public IViewComponentResult Invoke()
        {
            var categories = JsonUtil.TakeBookData();
            return View(categories);
        }*/
    }
}

using LibrarySystemData;
using LibrarySystemModels;

namespace LibrarySystem.Models.View
{
    public class ProfileView
    {
        public string Role { get; set; }
        public Person Person { get; set; }
    }
}

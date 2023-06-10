using System.Xml.Linq;

namespace LibrarySystem.Data
{
    public static class DataUtil
    {
        public const string Role = "User";
        public const string UserNameExist = "This Username already exists";
        public const string EmailExist = "This Email address is already registered";
        public const string PhoneExist = "This Phone number is already registered";
        public const string LoginEror = "The user name or password is incorrect";
        public const string PasswordsMatch = "Passwords do not match";
        public const string DoNotSaved = "Something Went Wrong, Please Try Again";
        public const string EmailConfirmed = "Your email addres is confirmed";
        public const string PasswordValidator = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)";
        public const string PersonTableName = "Person";
        public const string PublisherTableName = "Publisher";
        public const string StorageTableName = "Storage";
        public const string LanguageTableName = "Language"; 
        public const string AuthorTableName = "Author";
        public const string PositionTableName = "Position";
        public const string UserTableName = "Users";
        public const string GenreTableName = "Genre";
        public const string UserRoleTableName = "Role_Users";
        public const string JsonPath = @"Data\Books.json";
        public const string ModelNotValid = "model is not valid";
       
    }
}

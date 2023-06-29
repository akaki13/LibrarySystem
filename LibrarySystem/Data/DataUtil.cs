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
        public const string AuthorBookTableName = "Author_Book";
        public const string BookTableName = "Book";
        public const string BookGenreTableName = "Book_Genre";
        public const string BookPublisherTableName = "Book_Publisher";
        public const string BookLanguageTableName = "Book_Language";
        public const string BookStorageTableName = "Book_Storage";
        public const string PositionTableName = "Position";
        public const string UserTableName = "Users";
        public const string GenreTableName = "Genre";
        public const string UserRoleTableName = "Role_Users";
        public const string JsonPath = @"Data\Books.json";
        public const string PasswordHtml = @"Data\Password.html";
        public const string EmailHtml = @"Data\Email.html";
        public const string ModelNotValid = "model is not valid";
        public const string PasswordEmailSubject = "Reset your password";
        public const string ConfirmEmailSubject = "Confirm your email";
        public const string TableStatusInfo = "Info";
        public const string TableStatusError = "Error";
        public const string NewData = "Created New data";
        public const string UpdateData = "Data Updated";
        public const string DeleteData = "Data Deleted";

    }
}

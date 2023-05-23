using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace LibrarySystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IPersonService _personService;
        private readonly ITableLogService _tableLogService;
        private readonly IRoleService _roleService;
        private readonly IRoleUserService _roleUserService;
        private readonly IEmailSender _emailSender;
        public AccountController(IConfiguration configuration, IUserService userService,IRoleUserService roleUserService,
            IPersonService personService,ITableLogService tableLogService, IRoleService roleService, IEmailSender emailSender)
        {
            _configuration = configuration;
            _userService = userService;
            _personService = personService;
            _tableLogService = tableLogService;
            _roleService = roleService;
            _roleUserService = roleUserService;
            _emailSender = emailSender;
        }  

        [AllowAnonymous]
        public ActionResult Register()
        {

            return View();
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {

            return View();
        }
        public ActionResult ResetPassword(string token)
        {
            var id = TokenUtil.DecodeToken(token);
            var user = _userService.GetById(id);
            if (user == null)
            {
                return View("Register");
            }
            var data = new ResetPasswordView
            {
                Token = token,
            };
            return View(data);
        }

        public ActionResult EmailConfirmation(string tokenEmail)
        {
            var id = TokenUtil.DecodeToken(tokenEmail);
            var person = _personService.GetById(id);
            if (person == null)
            {
                ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                return View();
            }
            person.EmailIsConfiormed = true;
            _personService.Update(person);
            _personService.Save();
            ViewBag.ErrorMessage = DataUtil.EmailConfirmed;
            return View();
        }

        public ActionResult SavePassword(ResetPasswordView resetPassword)
        {
            if (ModelState.IsValid)
            {
                var id =  TokenUtil.DecodeToken(resetPassword.Token);
                if (resetPassword.Password != resetPassword.ConfirmPassword)
                {
                    ViewBag.ErrorMessage = DataUtil.PasswordsMatch;
                    return View("ResetPassword");
                }
                var user = _userService.GetById(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View("ResetPassword", resetPassword.Token);
                }
                user.Password = resetPassword.Password;
                _userService.Update(user);
                _userService.Save();
                return View();
            }
            else
            {
                return View("ResetPassword",resetPassword.Token);
            }
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();

        }

        public  ActionResult EditProfile()
        {
            var userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user =  _userService.GetById(userID);
            var person =  _personService.GetById(user.PersonId);
            var edit = new EditProfileView
            {
                FirstName = person.Firstname,
                LastName = person.Lastname,
                Address = person.Address,
                Email = person.Email,
                Phone = person.Phone,
                DateOfBirth = (DateTime)person.DateOfBirth,
            };
            return View(edit);
        }

        public  ActionResult EditProfileSave(EditProfileView edit)
        {
            if (ModelState.IsValid)
            {
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = _userService.GetById(userID);
                var person =  _personService.GetById(user.PersonId);
                person.Firstname = edit.FirstName;
                person.Lastname = edit.LastName;
                person.Address = edit.Address;
                person.Email = edit.Email;
                person.Phone = edit.Phone;
                person.DateOfBirth = edit.DateOfBirth;
                _personService.Update(person);
                _personService.Save();
                _tableLogService.Update(person.LogsId);
                _tableLogService.Save();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(edit);
            }
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(RegisterView registerDetails)
        { 
            if (ModelState.IsValid)
            {
                var loginCheck = _userService.GetByUserName(registerDetails.Login);
                var emailChack = _personService.GetByEmail(registerDetails.Email);
                var phoneCheck = _personService.GetByPhone(registerDetails.Phone);
                if (loginCheck != null)
                {
                    ViewBag.ErrorMessage = DataUtil.UserNameExist;
                    return View("Register", registerDetails);
                }
                if (emailChack != null)
                {
                    ViewBag.ErrorMessage = DataUtil.EmailExist;
                    return View("Register", registerDetails);

                }
                if (phoneCheck != null)
                {
                    ViewBag.ErrorMessage = DataUtil.EmailExist;
                    return View("Register", registerDetails);

                }
                var personLog = _tableLogService.Add(DataUtil.PersonTableName);
                _tableLogService.Save();

                Person person = new Person
                {
                    Firstname = registerDetails.FirstName,
                    Lastname = registerDetails.LastName,
                    Phone = registerDetails.Phone,
                    Address = registerDetails.Address,
                    DateOfBirth = registerDetails.DateOfBirth,
                    Email = registerDetails.Email,
                    LogsId = personLog.Id,
                }; 
                _personService.Add(person);
                _personService.Save();
                var userLog = _tableLogService.Add(DataUtil.UserTableName);
                _tableLogService.Save();
                User user = new User
                {
                    Login = registerDetails.Login,
                    Password = registerDetails.Password,
                    PersonId = person.Id,
                    LogsId = userLog.Id,
                };
                _userService.Add(user);
                _userService.Save();
                var role = _roleService.GetByTitle(DataUtil.Role);
                var roleUserLog = _tableLogService.Add(DataUtil.UserRoleTableName);
                _tableLogService.Save();
                RoleUser roleUser = new RoleUser
                {
                    RoleId = role.Id,
                    UsersId = user.Id,
                    LogsId = roleUserLog.Id,
                };
                _roleUserService.Add(roleUser);
                _roleUserService.Save();
                var tokenEmail = TokenUtil.CreateToken(person.Id.ToString(), _configuration);
                var link = Url.Action("EmailConfirmation", "Account", new { tokenEmail = tokenEmail }, "https");
                var body = $"Confirm your email address by clicking here: {link}";
                _emailSender.SendEmailAsync(person.Email, "Email confirmed", body);
                return View();
            }
            else
            {
                return View("Register", registerDetails);
            }
        }

        [HttpPost]
        public ActionResult LoginInDetails(LoginView loginDetails)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetByLoginInfo(loginDetails.Usarname, loginDetails.Password);
                if (user != null)
                {
                    var role = _roleUserService.GetByUserId(user.Id);
                    var token = TokenUtil.CreateToken(user, role, _configuration);
                    HttpContext.Response.Cookies.Append("Token", token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = DataUtil.LoginEror;
                    return View("Login");
                }
            }
            else
            {
                return View("Login");
            }
        }

        [Authorize]
        public ActionResult Profile()
        {
            if (ModelState.IsValid)
            {
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var role = User.FindFirstValue(ClaimTypes.Role);
                var user= _userService.GetById(userID);
                if (user != null)
                {
                    var person = _personService.GetById(user.PersonId);
                    return View(new ProfileView { Role = role, Person = person });
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Token");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public  async Task<ActionResult>  ForgotPasswordDetails(ForgotPassworedView forgot)
        {
            var person = _personService.GetByEmail(forgot.Email);
            if (person != null)
            {
                var user = _userService.GetByPersonId(person.Id);
                if (user != null)
                {
                    var token = TokenUtil.CreateToken(user.Id.ToString(), _configuration);
                    var link =  Url.Action("ResetPassword", "Account", new { token = token }, "https");
                    var body = $"Please reset your password by clicking here: {link}";
                    await _emailSender.SendEmailAsync(forgot.Email, "Reset Password", body);
                }
            }
            return View();
        }
    }
}

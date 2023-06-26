using AutoMapper;
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
        private readonly IMapper _mapper;

        public AccountController(IConfiguration configuration, IUserService userService,IRoleUserService roleUserService,
            IPersonService personService,ITableLogService tableLogService, IRoleService roleService,IMapper mapper)
        {
            _configuration = configuration;
            _userService = userService;
            _personService = personService;
            _tableLogService = tableLogService;
            _roleService = roleService;
            _roleUserService = roleUserService;
            _mapper = mapper;
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
            var edit = _mapper.Map<EditProfileView>(person);
            return View(edit);
        }

        public  ActionResult EditProfileSave(EditProfileView edit)
        {
            if (ModelState.IsValid)
            {
                int userID = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = _userService.GetById(userID);
                var person = _personService.GetById(user.PersonId);
                _mapper.Map(edit, person);
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
        public async Task<IActionResult> SaveRegisterDetails(RegisterView registerDetails)
        { 
            if (ModelState.IsValid)
            {
                IActionResult result = RegisterCheck(registerDetails);
                if (result != null)
                {
                    return result;
                }
                var personLog = _tableLogService.Add(DataUtil.PersonTableName);
                _tableLogService.Save();
                var person = _mapper.Map<Person>(registerDetails);
                person.LogsId = personLog.Id;
                person.EmailIsConfiormed = false;
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
                var link = Url.Action("EmailConfirmation", "Account", new { tokenEmail = tokenEmail }, Request.Scheme);
                await EmailUtil.EmailConfirmedLink(person.Email,link, _configuration, person);
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
                    var link =  Url.Action("ResetPassword", "Account", new { token = token }, Request.Scheme);
                    await EmailUtil.PassworResetLink(forgot.Email, link , _configuration , person);
                }
            }
            return View();
        }

        private IActionResult RegisterCheck(RegisterView registerDetails)
        {
            var emailCheck = _personService.GetByEmail(registerDetails.Email);
            var phoneCheck = _personService.GetByPhone(registerDetails.Phone);
            var loginCheck = _userService.GetByUserName(registerDetails.Login);
            if (loginCheck != null)
            {
                ViewBag.ErrorMessage = DataUtil.UserNameExist;
                return View("Register", registerDetails);
            }
            if (emailCheck != null)
            {
                ViewBag.ErrorMessage = DataUtil.EmailExist;
                return View("Register", registerDetails);

            }
            if (phoneCheck != null)
            {
                ViewBag.ErrorMessage = DataUtil.EmailExist;
                return View("Register", registerDetails);
            }
            if (registerDetails.ConfirmPassword == registerDetails.Password)
            {
                ViewBag.ErrorMessage = DataUtil.PasswordsMatch;
                return View("Register", registerDetails);
            }
            return null;
        }

    }
}

using AutoMapper;
using LibraryService;
using LibrarySystem.Data;
using LibrarySystem.Models.Email;
using LibrarySystem.Models.View;
using LibrarySystem.Util;
using LibrarySystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using System;
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
                _tableLogService.AddDataError( DataUtil.TableStatusError, DataUtil.DataDoMotFound, id);
                ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                return View();
            }
            try
            {
                person.EmailIsConfiormed = true;
                _personService.Update(person);
                _personService.Save();
                _tableLogService.Update(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                ViewBag.ErrorMessage = DataUtil.EmailConfirmed;
                return View();
            }
            catch (Exception e)
            {
                _tableLogService.Discard();
                _tableLogService.Update(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusError, e.Message);
                return View();
            }
            
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
                try
                {
                    user.Password = resetPassword.Password;
                    _userService.Update(user);
                    _userService.Save();
                    _tableLogService.Update(DataUtil.UserTableName, user.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                    return View();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.UserTableName, user.Id, DataUtil.TableStatusError, e.Message);
                    return View();
                }
                
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
                try
                {
                    _mapper.Map(edit, person);
                    _personService.Update(person);
                    _personService.Save();
                    _tableLogService.Update(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusInfo, DataUtil.UpdateData);
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.Update(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusError, e.Message);
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View(edit);
                }
                
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
                try
                {
                    var person = _mapper.Map<Person>(registerDetails);
                    person.EmailIsConfiormed = false;
                    _personService.Add(person);
                    _personService.Save();

                    User user = new User
                    {
                        Login = registerDetails.Login,
                        Password = registerDetails.Password,
                        PersonId = person.Id,
                    };
                    _userService.Add(user);
                    _userService.Save();
                    _tableLogService.AddData(DataUtil.PersonTableName, person.Id, DataUtil.TableStatusInfo, DataUtil.NewData, user.Id);
                    _tableLogService.AddData(DataUtil.UserTableName, user.Id, DataUtil.TableStatusInfo, DataUtil.NewData, user.Id);
                    var role = _roleService.GetByTitle(DataUtil.Role);
                    RoleUser roleUser = new RoleUser
                    {
                        RoleId = role.Id,
                        UsersId = user.Id,
                    };
                    _roleUserService.Add(roleUser);
                    _roleUserService.Save();
                    _tableLogService.AddData(DataUtil.UserRoleTableName, roleUser.Id, DataUtil.TableStatusInfo, DataUtil.NewData, user.Id);

                    var tokenEmail = TokenUtil.CreateToken(person.Id.ToString(), _configuration);
                    var link = Url.Action("EmailConfirmation", "Account", new { tokenEmail = tokenEmail }, Request.Scheme);
                    var emailmodel = new EmailConfirmation
                    {
                        FirstName = person.Firstname,
                        LastName = person.Lastname,
                        EmailConfirmationLink = link,
                    };
                    await EmailUtil.CreateTextAndSend(DataUtil.EmailHtmlPath, DataUtil.ConfirmEmailSubject , person.Email, _configuration, emailmodel);
                    return View();
                }
                catch (Exception e)
                {
                    _tableLogService.Discard();
                    _tableLogService.AddDataError(DataUtil.TableStatusError, e.Message, null);
                    ViewBag.ErrorMessage = DataUtil.DoNotSaved;
                    return View("Register", registerDetails);
                }
                
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
                    _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, null);
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
                _tableLogService.AddDataError(DataUtil.TableStatusError, DataUtil.DataDoMotFound, userID);
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
                    var emailmodel = new PasswordReset
                    {
                        FirstName = person.Firstname,
                        LastName = person.Lastname,
                        PasswordResetLink = link,
                    };
                    await EmailUtil.CreateTextAndSend(DataUtil.PasswordHtmlPath, DataUtil.PasswordEmailSubject, forgot.Email, _configuration, emailmodel);
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
            if (registerDetails.ConfirmPassword != registerDetails.Password)
            {
                ViewBag.ErrorMessage = DataUtil.PasswordsMatch;
                return View("Register", registerDetails);
            }
            return null;
        }

    }
}

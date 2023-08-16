using Azure;
using cargosiklink.Models.ViewModel.Account;
using cargosiklink.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace cargosiklink.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> InfoAboutUser()
        {
            if(User.Identity.Name == null)
                return NotFound();
            
            var data = _accountService.GetAboutInfo(User.Identity.Name);
            return View(data.Result);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
           => View();
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var isCreatedUser = await _accountService.Register(registerViewModel);
                if (isCreatedUser)
                    return RedirectToAction("Index", "NumberTracks");
                else
                    ModelState.AddModelError("", "При создании пользователя произошла ошибка, попробуйте заново!");
            }
            return View(registerViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
            => View();
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                if (changePasswordViewModel == null)
                    ModelState.AddModelError("", "Не правильно введены данные, введите данные по новой");
                if (changePasswordViewModel.OldPassword == string.Empty || changePasswordViewModel.NewPassword == string.Empty
                    || changePasswordViewModel.ConfirmNewPassword == string.Empty)
                    ModelState.AddModelError("", "Не правильно введены данные, введите данные по новой");

                var isChanged = await _accountService.ChangePassword(User.Identity.Name, changePasswordViewModel);
                if(!isChanged)
                    ModelState.AddModelError("", "Ошибка при изменении пароля, введите данные по новой");
                else
                    return RedirectToAction("Index", "NumberTracks");
            }
            return View(changePasswordViewModel);   
            
        }
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if(loginViewModel.Login == string.Empty || loginViewModel.Password == string.Empty)
                return NotFound();
            
            var claims = await _accountService.Login(loginViewModel);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claims));
            ViewBag.UserId = await _accountService.GetUserId(User.Identity.Name);

            return RedirectToAction("Index", "NumberTracks");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

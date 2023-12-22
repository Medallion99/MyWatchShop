using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;

namespace MyWatchShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                { UserName =  model.Email,
                  Email = model.Email,
                  FirstName = model.FirstName,
                  LastName = model.LastName,
                };
                var createUser = await _userManager.CreateAsync(user, model.Password);

                if (createUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "regular");

                    //Send email with email confirmation link
                    var token = _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var link = Url.Action("ConfirmEmail", "Auth", new {model.Email, token}, Request.Scheme);
                    ViewBag.Link = link;
                    //return RedirectToAction("BestSeller", "Product");
                    return View();

                }

                foreach (var err in createUser.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Login (string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginViewModel, string? returnUrl)
        {
           
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

                if (user != null)
                {

                    var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);

                    if (loginResult.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("BestSeller", "Product");
                    }
                }
                ModelState.AddModelError("Login error", "Invalid Credential");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("BestSeller", "Product");   
        }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IEmailService emailService )
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._emailService = emailService;
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
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, "regular");

                    //Send email with email confirmation link
                    

                    if (addToRoleResult.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        var link = Url.Action("ConfirmEmail", "Account", new { user.Email, token }, Request.Scheme);
                        var body = @$"Hi {user.FirstName}, 
Please click the link <a href='{link}'>Here</a> to confirm your account's email";
                        await _emailService.SendEmailAsync(user.Email, "Confirm Email", body);

                        return RedirectToAction("EmailConfirmation", "Account", new { name = user.FirstName });
                    }
                    foreach(var err in addToRoleResult.Errors)
                    {
                        ModelState.AddModelError(err.Code, err.Description);
                    }

                    //return RedirectToAction("BestSeller", "Home");

                }

                foreach (var err in createUser.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EmailConfirmation(string name)
        {
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string Email, string token)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user != null)
            {
                var confirmedEmailResult = await _userManager.ConfirmEmailAsync(user, token);
                if (confirmedEmailResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var err in confirmedEmailResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                    return View(ModelState);
                }
            }
            ModelState.AddModelError("", "Email Confirmation Failed");
            return View(ModelState);
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
                    if(await _userManager.IsEmailConfirmedAsync(user))
                    {
                        var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);

                        if (loginResult.Succeeded)
                        {
                            if (!string.IsNullOrEmpty(returnUrl))
                            {
                                return LocalRedirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid Credentials");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email Not Confirmed");
                    }
                }
                ModelState.AddModelError("Login error", "Invalid Credential");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");   
        }
        

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                //Find User by Email
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    //Send Reset Password Link

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var link = Url.Action("PasswordReset", "Account", new { user.Email, token }, Request.Scheme);
                    var body = @$" Oops!!. {user.FirstName} Did you request for a password reset? 
Looks like you forgot your Password. Please click the link <a href='{link}'>Here</a> to reset your account password if this request was made by you";
                    await _emailService.SendEmailAsync(user.Email, "Forgot Password", body);

                    ViewBag.Message = "A reset password link has been sent to the provided email, proceed to your email and follow the link to reset password";
                    return View();
                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View();
        }

        [HttpGet]
        public IActionResult PasswordReset(string Email, string token)
        {
            var resetPasswordView = new ResetPasswordViewModel { Email = Email, Token = token };
            return View(resetPasswordView);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var resetPasswordResult = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
                    if (resetPasswordResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        foreach(var error in resetPasswordResult.Errors)
                        {
                            ModelState.AddModelError(error.Code, error.Description);
                        }
                        return View(model);
                    }

                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

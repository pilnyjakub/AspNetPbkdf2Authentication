using AspNetPbkdf2Authentication.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace AspNetPbkdf2Authentication.Controllers
{
    public class AccountController : BaseController
    {
        private readonly MyContext context = new();

        public IActionResult Login()
        {
            if (ViewBag.SessionUsername is not null) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            if (!ValidatePassword(loginViewModel))
            {
                ModelState.AddModelError("WrongCredentials", "Incorrect username or password.");
                return View(loginViewModel);
            }
            HttpContext.Session.SetString("SessionUsername", loginViewModel.Username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            if (ViewBag.SessionUsername is not null) { return RedirectToAction("Index", "Home"); }
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }
            if (registerViewModel.Password != registerViewModel.ConfirmationPassword)
            {
                ModelState.AddModelError("NotSamePassword", "Passwords must be same.");
                return View(registerViewModel);
            }
            User? user = context.Users.FirstOrDefault(u => u.Username == registerViewModel.Username);
            if (user is not null)
            {
                ModelState.AddModelError("UsedUsername", "This username is already used.");
                return View(registerViewModel);
            }
            RegisterUser(registerViewModel);
            HttpContext.Session.SetString("SessionUsername", registerViewModel.Username);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("SessionUsername");
            return RedirectToAction("Index", "Home");
        }

        private static void RegisterUser(RegisterViewModel registerViewModel)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128);
            byte[] hash = KeyDerivation.Pbkdf2(registerViewModel.Password, salt, KeyDerivationPrf.HMACSHA512, 100000, 256);
            User user = new(registerViewModel.Username, Convert.ToBase64String(hash), Convert.ToBase64String(salt));
            user.DbAdd();
        }

        private bool ValidatePassword(LoginViewModel loginViewModel)
        {
            User? user = context.Users.FirstOrDefault(u => u.Username == loginViewModel.Username);
            if (user is null) { return false; }
            byte[] salt = Convert.FromBase64String(user.PasswordSalt);
            byte[] hash = KeyDerivation.Pbkdf2(loginViewModel.Password, salt, KeyDerivationPrf.HMACSHA512, 100000, 256);
            return Convert.ToBase64String(hash) == user.PasswordHash;
        }
    }
}

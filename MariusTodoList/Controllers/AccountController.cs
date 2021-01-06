using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MariusTodoList.Data.Abstractions;
using MariusTodoList.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;


namespace MariusTodoList.Controllers
{
    public class AccountController : Controller
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
           IConfiguration configuration) : base()
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            //_unitOfWork = unitOfWork;            
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser model)
        {
            ApplicationUser user = null;
            var userNameSess = HttpContext.Session.GetString("UserName");
            var userIdSess = HttpContext.Session.GetString("UserID");

            //var testUserName = System.Web.HttpContext.Current.User.Identity.Name;         

            if (userNameSess != null && userIdSess != null )                          
                user = await _userManager.FindByIdAsync(userIdSess);            
            else
             user = await _userManager.FindByNameAsync(model.UserName);

            if (user != null)
            {
                var SignIn = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (SignIn.Succeeded)
                {                   
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("UserID", user.Id);

                    var currentUser = await GetCurrentUserAsync();

                    return RedirectToAction("Index", "Tasks");
                }
                else {

                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ApplicationUser model)
        {
            HttpContext.Session.SetString("UserName", "");
            HttpContext.Session.SetString("UserID", "");

            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserID");

            var user = new ApplicationUser
            {
                Id = model.Id,
                UserName = model.UserName,
                Password = model.Password
            };
            var result = await _userManager.CreateAsync(user, user.Password);

            if (result.Succeeded)
            {
                var SignIn = await _signInManager.CheckPasswordSignInAsync(user, user.Password, false);
                if (SignIn.Succeeded)
                {
                    HttpContext.Session.SetString("UserName", user.UserName);
                    HttpContext.Session.SetString("UserID", user.Id);

                    return RedirectToAction("Index", "Tasks");
                }
            }
            else
            {

                
            }

            return RedirectToAction("Register", "Account");
        }

        public IActionResult Logout()
        {

            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserID");
            return RedirectToAction("Index", "Tasks");
        }


        public IActionResult Index()
        {
            return View();
        }
    }
    
}
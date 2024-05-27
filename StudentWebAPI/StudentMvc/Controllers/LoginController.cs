using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using StudentCore.Models;
using StudentMvc.Services;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication;

namespace StudentMvc.Controllers
{
    public class LoginController : Controller
    {
        private HttpClientService _httpClientService;

        public LoginController(HttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        [HttpPost]
        public async Task<IActionResult> Index(string login, string password)
        {
            var student = await _httpClientService.Post<StudentAuthDto, StudentGetDto>("/Auth", new StudentAuthDto { Login = login, Password = password }); 

            if(student != null)
            {
                var claims = new List<Claim> {
                    new Claim("Id", student.Id.ToString()),
                    new Claim(ClaimTypes.Name, student.Email)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            }

            return Redirect("/");
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}

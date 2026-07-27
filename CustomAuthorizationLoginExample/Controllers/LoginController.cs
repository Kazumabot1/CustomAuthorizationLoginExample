using CustomAuthorizationLoginExample.Domain.Features.Login;
using Microsoft.AspNetCore.Mvc;

namespace CustomAuthorizationLoginExample.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginRequestModel requestModel)
        {
            var loginResponse = await _loginService.HandleAsync(requestModel);
            var opts = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddHours(12),
                SameSite = SameSiteMode.Lax,
                Secure = true
            };
            string token = loginResponse.UserId + "|" + loginResponse.SessionId;

            HttpContext.Response.Cookies.Delete("Authorization");
            HttpContext.Response.Cookies.Append("Authorization", token , opts);

            return RedirectToAction("Index", "Home");
        }
    }
}

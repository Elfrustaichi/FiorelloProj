using Fiorello.UI.Filters;
using Fiorello.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiorello.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;
        public AccountController()
        {
            _client = new HttpClient();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest userLoginRequest,string returnUrl)
        {
            if(!ModelState.IsValid) return View();

            StringContent content=new StringContent(JsonConvert.SerializeObject(userLoginRequest),System.Text.Encoding.UTF8,"application/json");
            using (var response = await _client.PostAsync("https://localhost:7081/api/Auth/Login", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var token = "Bearer " + responseContent;

                    HttpContext.Response.Cookies.Append("auth_token", token);

                    return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("index", "dashboard");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("","Username or password is not correct");
                    return View();
                }
                return View("error");
                    
            }
        }
        [ServiceFilter(typeof(AuthFilter))]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("auth_token");

            return RedirectToAction("login");
        }
    }
}

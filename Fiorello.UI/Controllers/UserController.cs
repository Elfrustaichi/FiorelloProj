using Fiorello.UI.Filters;
using Fiorello.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Fiorello.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class UserController : Controller
    {
        private readonly HttpClient _client;
        public UserController()
        {
            _client = new HttpClient();
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            using (var response = await _client.GetAsync("https://localhost:7081/api/Users/GetAll"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<UserGetAllItems>>(content);

                    return View(data);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Login", "Account");
            }
            return View("error");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateRequest createRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            if (!ModelState.IsValid) return View();


            StringContent content = new StringContent(JsonConvert.SerializeObject(createRequest), System.Text.Encoding.UTF8, "application/json");

            using (var response = await _client.PostAsync("https://localhost:7081/api/Users/Create", content))
            {
                if (response.IsSuccessStatusCode) return RedirectToAction("index");

                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var contentError = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                    foreach (var error in contentError.Errors)
                    {
                        ModelState.AddModelError(error.Key, error.Message);
                    }

                    return View();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "account");
                }
                return View("error");
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            using (var response = await _client.DeleteAsync($"https://localhost:7081/api/Users/Delete/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "account");
                }
                return View("error");
            }
        }

    }
}

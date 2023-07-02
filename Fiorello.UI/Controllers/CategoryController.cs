using Fiorello.UI.Filters;
using Fiorello.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Fiorello.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class CategoryController : Controller
    {
        private readonly HttpClient _client;

        public CategoryController()
        {
            _client=new HttpClient();
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            using(var response=await _client.GetAsync("https://localhost:7081/api/Categories/GetAll"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var content=await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<CategoryGetAllItems>>(content);

                    return View(data);
                }
                else if(response.StatusCode==System.Net.HttpStatusCode.Unauthorized||response.StatusCode==System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Login","Account");
            }
            return View("error");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateRequest createRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            if (!ModelState.IsValid) return View();

            
            StringContent content = new StringContent(JsonConvert.SerializeObject(createRequest), System.Text.Encoding.UTF8, "application/json");

            using (var response =await _client.PostAsync("https://localhost:7081/api/Categories/Create", content))
            {
                if (response.IsSuccessStatusCode) return RedirectToAction("index");

                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseContent=await response.Content.ReadAsStringAsync();
                    var contentError = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                    foreach (var error in contentError.Errors)
                    {
                        ModelState.AddModelError(error.Key,error.Message);
                    }

                    return View();
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login","account");
                }
                return View("error");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            using(var response =await _client.DeleteAsync($"https://localhost:7081/api/Categories/Delete/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else if(response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "account");
                }
                return View("error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            using(var response=await _client.GetAsync($"https://localhost:7081/api/Categories/Get/{id}"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var responseContent=await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<CategoryGetResponse>(responseContent);

                    return View(new CategoryUpdateRequest { Name=data.Name});
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "account");
                }
                return View("error");
            }

            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,CategoryUpdateRequest categoryUpdateRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            if (!ModelState.IsValid) return View();

            StringContent content = new StringContent(JsonConvert.SerializeObject(categoryUpdateRequest),System.Text.Encoding.UTF8,"application/json");

            using(var response =await _client.PutAsync($"https://localhost:7081/api/Categories/Update/{id}", content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(responseContent);

                    foreach (var item in error.Errors)
                        ModelState.AddModelError(item.Key, item.Message);

                    return View();
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

using Fiorello.UI.Filters;
using Fiorello.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Fiorello.UI.Controllers
{
    [ServiceFilter(typeof(AuthFilter))]
    public class SliderController : Controller
    {
        private readonly HttpClient _client;
        public SliderController()
        {
            _client = new HttpClient();
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            using(var response=await _client.GetAsync("https://localhost:7081/api/Sliders/GetAll"))
            {
                if(response.IsSuccessStatusCode)
                {
                    var responseContent=await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<SliderGetAllItems>>(responseContent);

                    return View(data);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Login", "Account");

                return View("error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateRequest createRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            if (!ModelState.IsValid) return View();

            MultipartFormDataContent content= new MultipartFormDataContent();

            var BackgroundFileContent = new StreamContent(createRequest.BackgroundImage.OpenReadStream());
            BackgroundFileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(createRequest.BackgroundImage.ContentType);
            var SignatureFileContent = new StreamContent(createRequest.SignatureImage.OpenReadStream());
            SignatureFileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(createRequest.SignatureImage.ContentType);

            content.Add(BackgroundFileContent,"BackgroundImage",createRequest.BackgroundImage.FileName);
            content.Add(SignatureFileContent,"SignatureImage",createRequest.SignatureImage.FileName);
            content.Add(new StringContent(createRequest.Title), "Title");
            content.Add(new StringContent(createRequest.Description), "Description");
           


            using (var response =await _client.PostAsync("https://localhost:7081/api/Sliders/Create", content))
            {
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var requestErrors = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var err in requestErrors.Errors) ModelState.AddModelError(err.Key, err.Message);

                    return View();
                }
                return View("error");

            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            using(var response=await _client.DeleteAsync($"https://localhost:7081/api/Sliders/Delete/{id}"))
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

        public async Task<IActionResult> Edit(int id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            using (var response= await _client.GetAsync($"https://localhost:7081/api/Sliders/Get/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent=await response.Content.ReadAsStringAsync();
                    var data=JsonConvert.DeserializeObject<SliderGetResponse>(responseContent);

                    var vm = new SliderUpdateRequest
                    {
                        Title= data.Title,
                        Description= data.Description
                    };
                    ViewBag.BackgroundImageUrl=data.BackgroundImageUrl;
                    ViewBag.SignatureImageUrl=data.SignatureImageUrl;
                    return View(vm);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return RedirectToAction("login", "account");
                }
                return View("error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,SliderUpdateRequest updateRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            if (!ModelState.IsValid) return View();

            MultipartFormDataContent content= new MultipartFormDataContent();

            if (updateRequest.BackgroundImage != null)
            {
                var backgroundFileContent = new StreamContent(updateRequest.BackgroundImage.OpenReadStream());
                backgroundFileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(updateRequest.BackgroundImage.ContentType);
                content.Add(backgroundFileContent,"BackgroundImage",updateRequest.BackgroundImage.FileName);
            }
            if (updateRequest.SignatureImage != null)
            {
                var signatureFileContent = new StreamContent(updateRequest.SignatureImage.OpenReadStream());
                signatureFileContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(updateRequest.SignatureImage.ContentType);
                content.Add(signatureFileContent, "BackgroundImage", updateRequest.SignatureImage.FileName);
            }

            content.Add(new StringContent(updateRequest.Title), "Title");
            content.Add(new StringContent(updateRequest.Description), "Description");

            using (var response = await _client.PutAsync($"https://localhost:7081/api/Sliders/Update/{id}", content))
            {
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var err in error.Errors) ModelState.AddModelError(err.Key, err.Message);

                    
                    return View();
                }
            }
            return View("error");

        }

    }
}

using Fiorello.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Fiorello.UI.Controllers
{
    public class FlowerController : Controller
    {
        private readonly HttpClient _client;
        public FlowerController()
        {
            _client = new HttpClient();
        }
        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);
            using(var response =await _client.GetAsync("https://localhost:7081/api/Flowers/GetAll"))
            {
                if (response.IsSuccessStatusCode) 
                {
                    var responseContent=await response.Content.ReadAsStringAsync();
                    var data=JsonConvert.DeserializeObject<List<FlowerGetAllItems>>(responseContent);

                    return View(data);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Login", "Account");

                return View("error");
            }

        }

        public async Task<IActionResult> Create()
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            ViewBag.Categories =await _getCategories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FlowerCreateRequest createRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);
            if (!ModelState.IsValid)
            {
                using(var response =await _client.GetAsync("https://localhost:7081/api/Categories/GetAll"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var categoryContent=await response.Content.ReadAsStringAsync();
                        var data = JsonConvert.DeserializeObject<List<CategoryGetAllItems>>(categoryContent);

                        ViewBag.Categories=data;
                        return View();
                    }
                }
            }

            MultipartFormDataContent content= new MultipartFormDataContent();

            var PosterImageContent = new StreamContent(createRequest.PosterImage.OpenReadStream());
            PosterImageContent.Headers.ContentType=System.Net.Http.Headers.MediaTypeHeaderValue.Parse(createRequest.PosterImage.ContentType);
            content.Add(PosterImageContent,"PosterImage",createRequest.PosterImage.FileName);

            foreach (var item in createRequest.Images)
            {
                var ImageContent = new StreamContent(item.OpenReadStream());
                ImageContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(item.ContentType);
                content.Add(ImageContent, "Images", item.FileName);
            }

            content.Add(new StringContent(createRequest.Name),"Name");
            content.Add(new StringContent(createRequest.Description),"Description");
            content.Add(new StringContent(createRequest.SalePrice.ToString("0.00")),"SalePrice");
            content.Add(new StringContent(createRequest.CostPrice.ToString("0.00")),"CostPrice");
            content.Add(new StringContent(createRequest.CategoryId.ToString()),"CategoryId");


            using (var response= await _client.PostAsync("https://localhost:7081/api/Flowers/Create",content))
            {
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var err in error.Errors) ModelState.AddModelError(err.Key, err.Message);

                    ViewBag.Categories = await _getCategories();

                    return View();
                }
                return View("error");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);

            using (var response=await _client.DeleteAsync($"https://localhost:7081/api/Flowers/Delete/{id}"))
            {
                if(response.IsSuccessStatusCode)
                {
                    return Ok();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    return RedirectToAction("Login", "Account");

                return View("error");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization, token);
            using (var response=await _client.GetAsync($"https://localhost:7081/api/Flowers/Get/{id}"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseContent=await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<FlowerGetResponse>(responseContent);

                    var vm = new FlowerUpdateRequest
                    {
                        CategoryId=data.Category.Id,
                        Name=data.Name,
                        SalePrice=data.SalePrice,
                        CostPrice=data.CostPrice,
                        Description=data.Description

                    };

                    ViewBag.PosterImageUrl=data.PosterImageUrl;
                    ViewBag.ImageUrls=data.ImageUrls;

                    ViewBag.Categories = await _getCategories();
                    return View(vm);
                }
            }
            return View("error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,FlowerUpdateRequest updateRequest)
        {
            var token = Request.Cookies["auth_token"];
            _client.DefaultRequestHeaders.Add(HeaderNames.Authorization,token);

            if(!ModelState.IsValid)
            {
                ViewBag.Categories=await _getCategories();
                return View();
            }

            MultipartFormDataContent content = new MultipartFormDataContent();
            if (updateRequest.PosterImage != null)
            {
                var PosterImageContent = new StreamContent(updateRequest.PosterImage.OpenReadStream());
                PosterImageContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(updateRequest.PosterImage.ContentType);
                content.Add(PosterImageContent, "PosterImage", updateRequest.PosterImage.FileName);
            }
            
            if(updateRequest.Images != null)
            {
                foreach (var item in updateRequest.Images)
                {
                    var ImageContent = new StreamContent(item.OpenReadStream());
                    ImageContent.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse(item.ContentType);
                    content.Add(ImageContent, "Images", item.FileName);
                }
            }

            content.Add(new StringContent(updateRequest.Name), "Name");
            content.Add(new StringContent(updateRequest.Description), "Description");
            content.Add(new StringContent(updateRequest.SalePrice.ToString("0.00")), "SalePrice");
            content.Add(new StringContent(updateRequest.CostPrice.ToString("0.00")), "CostPrice");
            content.Add(new StringContent(updateRequest.CategoryId.ToString()), "CategoryId");

            using(var response=await _client.PutAsync($"https://localhost:7081/api/Flowers/Update/{id}", content))
            {
                if(response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());
                    foreach (var err in error.Errors) ModelState.AddModelError(err.Key, err.Message);

                    ViewBag.Categories = await _getCategories();

                    return View();
                }
                return View("error");
            }
        }

        private async Task<List<CategoryGetAllItems>> _getCategories()
        {
            List<CategoryGetAllItems> categories = new List<CategoryGetAllItems>();
            using (var response=await _client.GetAsync("https://localhost:7081/api/Categories/GetAll"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var categoryContent=await response.Content.ReadAsStringAsync();
                    categories=JsonConvert.DeserializeObject<List<CategoryGetAllItems>>(categoryContent);
                }
                return categories;
            }
        }
    }
}

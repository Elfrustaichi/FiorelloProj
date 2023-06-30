using Fiorello.Services.Dtos.Category;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("Get/{id}")] 
        public ActionResult<CategoryGetDto> Get(int id)
        {
            var data=_categoryService.Get(id);

            return Ok(data);
            
        }

        [HttpGet("GetAll")]
        public ActionResult<List<CategoryGetAllItemDto>> GetAll()
        {
            var data = _categoryService.GetAll();
            return Ok(data);
        }

        [HttpPut("Update/{id}")]
        public ActionResult Update(int id,CategoryUpdateDto updateDto)
        {
            _categoryService.Update(id,updateDto);
            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _categoryService.Delete(id);

            return NoContent();
        }

        [HttpPost("Create")]
        public ActionResult<CreatedItemGetDto> Create(CategoryCreateDto createDto)
        {
            var data=_categoryService.Create(createDto);
            return StatusCode(201, data);
        }
    }
}

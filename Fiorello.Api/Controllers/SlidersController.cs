using Fiorello.Core.Repositories;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Slider;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlidersController : ControllerBase
    {
        private readonly ISliderService _sliderService;

        public SlidersController(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }
        [HttpPost("Create")]
        public ActionResult<CreatedItemGetDto> Create([FromForm]SliderCreateDto dto)
        {
            var data=_sliderService.Create(dto);

            return StatusCode(201, data);
        }
        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            _sliderService.Delete(id);

            return NoContent();
        }
        [HttpPut("Update/{id}")]
        public IActionResult Edit(int id,[FromForm]SliderUpdateDto dto)
        {
            _sliderService.Update(id, dto);

            return NoContent();
        }

        [HttpGet("Get/{id}")]
        public ActionResult<SliderGetDto> Get(int id)
        {
            var data=_sliderService.Get(id);
            return data;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<SliderGetAllItemDto>> GetAll()
        {
            var data = _sliderService.GetAll();

            return data;
        }
        

    }
}

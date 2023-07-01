using AutoMapper;
using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.Flower;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Fiorello.Api.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class FlowersController : ControllerBase
    {
        private readonly IFlowerService _flowerService;


        public FlowersController(IFlowerService flowerService)
        {
            _flowerService = flowerService;
           
        }
        [HttpPost("Create")]
        public ActionResult<CreatedItemGetDto> Create([FromForm]FlowerCreateDto dto)
        {
            var data=_flowerService.Create(dto);

            return data;
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            _flowerService.Delete(id);

            return NoContent();
        }

        [HttpGet("Get/{id}")]
        public ActionResult<FlowerGetDto> Get(int id)
        {
            var data= _flowerService.Get(id);

            return data;
        }

        [HttpGet("GetAll")]
        public ActionResult<List<FlowerGetAllItemDto>> GetAll()
        {
            var data=_flowerService.GetAll();

            return data;
        }

        [HttpPut("Update/{id}")]
        public ActionResult Update(int id,[FromForm]FlowerUpdateDto dto)
        {
            _flowerService.Update(id, dto);

            return NoContent();
        }
    }
}

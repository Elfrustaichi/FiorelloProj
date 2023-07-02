using Fiorello.Services.Dtos.Common;
using Fiorello.Services.Dtos.User;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("GetAll")]
        public ActionResult<List<UserGetAllItemsDto>> GetAll()
        {
            var data=_userService.GetAll();

            return Ok(data);
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult Delete(string id)
        {
            _userService.Delete(id);
            return NoContent();
        }

        [HttpPost("Create")]
        public ActionResult<CreatedItemGetDto> Create(UserCreateDto dto)
        {
            var data=_userService.Create(dto);
            return Ok(data);
        }
    }
}

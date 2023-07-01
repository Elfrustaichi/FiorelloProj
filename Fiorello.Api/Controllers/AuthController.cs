using Fiorello.Api.Dtos.UserDtos;
using Fiorello.Api.Services;
using Fiorello.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager,JwtService jwtService)
        {
           _userManager = userManager;
           _roleManager = roleManager;
            _jwtService = jwtService;
        }
     
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            AppUser user=await _userManager.FindByNameAsync(dto.UserName);

            if (user == null) return Unauthorized();

            if(!await _userManager.CheckPasswordAsync(user,dto.Password)) return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(_jwtService.GenerateToken(user,roles));
        }
       
    }
}

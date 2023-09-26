using BVBM.API.Dto;
using BVBM.API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BVBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(IAuthService authService, SignInManager<IdentityUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserDto userDto) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Échec de l'identification de l'utilisateur");
            }

            var result = await _authService.Login(userDto);
            if (result == true)
            {
                var tokenString = await _authService.GenerateTokenString(userDto);

                return Ok(tokenString);
            }
            return BadRequest();
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}

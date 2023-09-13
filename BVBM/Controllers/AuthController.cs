using BVBM.API.Dto;
using BVBM.API.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BVBM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserDto userDto)
        {
            var result = await _authService.RegisterUser(userDto);

            if (!result)
            {
                return BadRequest("Échec de la création de l'utilisateur");
            }

            return Ok(result);
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
                var tokenString = _authService.GenerateTokenString(userDto);
                return Ok(tokenString);
            }
            return BadRequest();
        }
    }
}

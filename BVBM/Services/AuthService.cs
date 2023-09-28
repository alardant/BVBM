using BVBM.API.Data;
using BVBM.API.Dto;
using BVBM.API.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BVBM.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _config = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        // Check the credential of the user upon Login
        public async Task<bool> Login(UserDto userDto)
        {
            //Search for user with the email
            var identityUser = await _userManager.FindByEmailAsync(userDto.Email);

            if (identityUser == null)
            {
                return false;
            }

            // Check if the password is correct
            return await _userManager.CheckPasswordAsync(identityUser, userDto.Password);
        }

        //Generate a JWT token
        public async Task<string> GenerateTokenString(UserDto userDto)
        {
            //Create the claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,userDto.Email)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            //Create the credential to sign the token
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Create token
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                issuer: _config.GetSection("Jwt:Issuer").Value,
                audience: _config.GetSection("Jwt:Audience").Value,
                signingCredentials: signingCred
                );

            //Transform token into string
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;

        }

        public async Task<bool> IsLoggedIn()
        {
            return await Task.FromResult(_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated);
        }
    }
}

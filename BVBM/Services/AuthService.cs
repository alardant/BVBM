using BVBM.API.Dto;
using BVBM.API.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BVBM.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _config = configuration;
        }

        //Register a new user
        public async Task<bool> RegisterUser(UserDto user)
        {
            var identityUser = new IdentityUser
            {
                Email = user.Email,
                UserName = user.Email,
            };

            //Create this user in the database
            var result = await _userManager.CreateAsync(identityUser, user.Password);
            return result.Succeeded;
        }

        // Checl the credential of the user upon Login
        public async Task<bool> Login(UserDto user)
        {
            //Search for suer with the email
            var identityUser = await _userManager.FindByEmailAsync(user.Email);

            if (identityUser == null)
            {
                return false;
            }

            // Check if the password is correct
            return await _userManager.CheckPasswordAsync(identityUser, user.Password);
        }

        //Generate a JWT token
        public string GenerateTokenString(UserDto userDto)
        {
            //Create the claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,userDto.Email)
            };

            // get the security Key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

            //Create the credential to sign the token
            var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Create token
            var securityToken = new JwtSecurityToken(
                claims:claims,
                expires:DateTime.Now.AddMinutes(60),
                issuer:_config.GetSection("Jwt:Issuer").Value,
                audience:_config.GetSection("Jwt:Audience").Value,
                signingCredentials:signingCred
                );

            //Transform token into string
            string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return tokenString;
        }
    }
}

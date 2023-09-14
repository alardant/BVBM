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
        private readonly DataContext _context;
        public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration, DataContext context)
        {
            _userManager = userManager;
            _config = configuration;
            _context = context;
        }

        //Register a new user
        public async Task<bool> RegisterUser(UserDto userDto)
        {
            var identityUser = new IdentityUser
            {
                Email = userDto.Email,
                UserName = userDto.Email,
            };

            //Create this user in the database
            var result = await _userManager.CreateAsync(identityUser, userDto.Password);
            return result.Succeeded;
        }

        // Checl the credential of the user upon Login
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

            // get the security Key
            var jwtSecret= await _context.JwtSecrets.FirstOrDefaultAsync();

            if (jwtSecret == null)
            {
                throw new Exception("Data not found.");
            }

            var storedHashedJwtKey = jwtSecret.SecretKeyHash;
            Console.WriteLine(1);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(storedHashedJwtKey));

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
            Console.WriteLine(2);
            return tokenString;

        }
    }
}

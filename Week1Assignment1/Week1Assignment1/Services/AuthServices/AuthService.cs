using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Week1Assignment1.DTO.User;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Services.AuthServices;

namespace Week1Assignment1.Data
{
    public class AuthService : IAuthService
    {
        
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Injecting DbContext and Configuration Services
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public AuthService(IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>data</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> Login(UserLoginDto userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Username, userLogin.Password, false, false);

            if (!result.Succeeded)
                return null;

            var authClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userLogin.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSignKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaim,
                signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns>data</returns>
        /// <exception cref="Exception"></exception>
        public async Task<IdentityResult> RegisterUser(MyUser user, string password)
        {
            var iuser = new IdentityUser()
            {
                UserName = user.Username
            };
            var result = await _userManager.CreateAsync(iuser, password);
            return result;
        }
    }
}

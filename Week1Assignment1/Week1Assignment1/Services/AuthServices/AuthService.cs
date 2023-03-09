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

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Injecting DbContext and Configuration Services
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        public AuthService(DataContext context, IConfiguration configuration,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager
            )
        {
            _context = context;
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

            if(!result.Succeeded)
            {
                return null;
            }

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
            //if (await UserExists(user.Username))
            //{
            //    throw new Exception(MsgKeys.UserExist);
            //}
            var result = await _userManager.CreateAsync(iuser, password);
            return result;
            //CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;
            //await _context.Users.AddAsync(user);
            //await _context.SaveChangesAsync();
            //var data = user.Id;
            //return data;
        }

        /// <summary>
        /// Verify if user exists
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        //public async Task<bool> UserExists(string username)
        //{
        //    if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// verify password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        /// <returns></returns>
        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[0] != passwordHash[0])
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// Creating jwt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string CreateToken(MyUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetting:Token").Value)
                );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }


    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Week1Assignment1.Data;
using Week1Assignment1.DTO.User;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;
using Week1Assignment1.Services.AuthServices;

namespace Week1Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IAuthService _authUser;
        public UserController(IAuthService authUser)
        {
            _authUser = authUser;
        }

        /// <summary>
        /// Register User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RegUser")]
        public async Task<IActionResult> RegUser(UserRegDto request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var result = await _authUser.RegisterUser(
                new MyUser {Username = request.Username }, request.Password
                );
                return Ok(new { result }, MsgKeys.RegisterUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto request)
        {
            try
            {
                    var result = await _authUser.Login(request);

                    if (string.IsNullOrEmpty(result))
                        return Unauthorized();

                    return Ok(new { result }, MsgKeys.LoginUserSuccess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

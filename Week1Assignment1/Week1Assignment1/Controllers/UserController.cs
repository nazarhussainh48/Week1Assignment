using Microsoft.AspNetCore.Mvc;
using Week1Assignment1.Data;
using Week1Assignment1.DTO.User;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;

namespace Week1Assignment1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private readonly IAuthRepository _authUser;

        public UserController(IAuthRepository authUser)
        {
            _authUser = authUser;
        }

        [HttpPost("RegUser")]
        public async Task<IActionResult> RegUser(UserRegDto request)
        {
            try { 
            var result = await _authUser.RegisterUser(
                new MyUser { Username = request.Username }, request.Password
                );
            
            return Ok(result, "User Registered Successfully");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser(UserLoginDto request)
        {
            try
            {
                var result = await _authUser.Login(
                    request.Username, request.Password
                    );

                return Ok(result, "User Loged in Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

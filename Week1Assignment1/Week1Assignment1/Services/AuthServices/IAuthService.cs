using Microsoft.AspNetCore.Identity;
using Week1Assignment1.DTO.User;
using Week1Assignment1.Models;

namespace Week1Assignment1.Services.AuthServices
{
    public interface IAuthService
    {
        /// <summary>
        /// register user interface
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterUser(MyUser user, string password);

        /// <summary>
        /// login user interface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Login(UserLoginDto userLogin);
    }
}

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
        Task<int> RegisterUser(MyUser user, string password);
        /// <summary>
        /// login user interface
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> Login(string username, string password);
        /// <summary>
        /// user exists interface
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> UserExists(string username);
    }
}

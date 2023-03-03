using Week1Assignment1.Models;

namespace Week1Assignment1.Data
{
    public interface IAuthRepository
    {
        /// <summary>
        /// it will return int id
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<int> RegisterUser(MyUser user, string password); 
        Task<string> Login(string username, string password);
        Task<bool> UserExists(string username);
        
    }
}

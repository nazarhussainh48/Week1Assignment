using Week1Assignment1.Models;

namespace Week1Assignment1.Services.AuthService
{
    public interface IAuthService
    {
        Task<int> RegisterUser(MyUser user, string password);
        Task<string> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}

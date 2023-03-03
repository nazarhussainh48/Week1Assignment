using Microsoft.EntityFrameworkCore;
using Week1Assignment1.Helper;
using Week1Assignment1.Models;

namespace Week1Assignment1.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<string> Login(string username, string password)
        {
            MyUser user = await _context.Users.FirstOrDefaultAsync(x => x.Username.ToLower().Equals(username.ToLower()));
            if (user == null)
            {
                throw new Exception(MsgKeys.InvalidUser);
            }
            else if (!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception(MsgKeys.WrongPassword);
            }
            else
            {
                var data = user.Id.ToString();
                return data;
            }
        }


        public async Task<int> RegisterUser(MyUser user, string password)
        {
            if (await UserExists(user.Username))
            {
                throw new Exception(MsgKeys.UserExist);
            }
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            var data = user.Id;
            return data;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username.ToLower() == username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

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


    }
}

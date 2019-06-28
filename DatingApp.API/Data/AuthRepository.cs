using System;
using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{

    public class AuthRepository : IAuthRepository
    {
        private DataContext _Context { get; }
        public AuthRepository(DataContext context)
        {
            _Context = context;

        }
        public Task<User> Login(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordsalt;
            createPasswordHash(password, out passwordHash, out passwordsalt) ;
            
            user.PasswordHash = passwordHash;
            user.Passwordsalt = passwordsalt;   
            await _Context.Users.AddAsync(user);  
            await _Context.SaveChangesAsync();

            return user;
            }

        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public Task<bool> UserExists(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
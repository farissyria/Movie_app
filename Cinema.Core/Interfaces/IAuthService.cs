using Cinema.Core.Entities;

namespace Cinema.Core.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password, string firstName, string lastName);
        Task<string> LoginAsync(string email, string password);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
       
    }
}

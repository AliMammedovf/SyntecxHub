using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Interfaces
{
    public interface IUserRepository
    {

        Task<User?> GetByEmailAsync(string email);
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
    }
}

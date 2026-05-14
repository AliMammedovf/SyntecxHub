using SyntecxhubUserApi.Business.DTOs;

namespace SyntecxhubUserApi.Interfaces
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDTO dto);

        Task<string> LoginAsync(LoginDTO dto);

        Task<List<UserGetAllDTO>> GetAllUsersAsync();

        Task BlockUserAsync(int id);

        Task PromoteToAdminAsync(int id);

    }
}

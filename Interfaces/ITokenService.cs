using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}

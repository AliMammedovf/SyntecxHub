using Microsoft.AspNetCore.Http.HttpResults;
using SyntecxhubUserApi.Business.DTOs;
using SyntecxhubUserApi.Interfaces;
using SyntecxhubUserApi.Models;

namespace SyntecxhubUserApi.Business.Services
{
    public class AuthService
    {
        private readonly IUserRepository _repo;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;

        public AuthService(
            IUserRepository repo,
            IPasswordHasher hasher,
            ITokenService tokenService)
        {
            _repo = repo;
            _hasher = hasher;
            _tokenService = tokenService;
        }

        public async Task RegisterAsync(RegisterDTO dto)
        {
            // 1. Email check
            if (await _repo.ExistsByEmailAsync(dto.Email))
                 throw new BadHttpRequestException("Email already exsist!");
              

            // 2. Password hash 
            var hash = _hasher.Hash(dto.Password);

            // 3. User create
            var user = new User
            {
                Email = dto.Email,
                Name = dto.Name,
                PasswordHash = hash
            };

            await _repo.AddAsync(user);
        }

        public async Task<string> LoginAsync(LoginDTO dto)
        {
            // 1. User find
            var user = await _repo.GetByEmailAsync(dto.Email);

            if (user == null) throw new BadHttpRequestException("Email or Password invalid!");


            // 2. Password verify
            var valid = _hasher.Verify(dto.Password, user.PasswordHash);

            if (!valid)
                return null;

            // 3. Token create
            return _tokenService.GenerateToken(user);
        }

        
    }
}

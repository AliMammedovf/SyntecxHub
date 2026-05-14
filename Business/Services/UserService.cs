using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SyntecxhubUserApi.Business.DTOs;
using SyntecxhubUserApi.Data;
using SyntecxhubUserApi.Interfaces;
using SyntecxhubUserApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SyntecxhubUserApi.Business.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public UserService(
            AppDbContext context,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _context = context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        // REGISTER
        public async Task<string> RegisterAsync(RegisterDTO dto)
        {
            var existUser = await _context.Users
                .FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (existUser != null)
                throw new Exception("Email already exists");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,

                // Normalda hashlenmelidir
                PasswordHash = dto.Password,

                Role = "User"
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return "User registered successfully";
        }

        // LOGIN
        public async Task<string> LoginAsync(LoginDTO dto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x =>
                    x.Email == dto.Email &&
                    x.PasswordHash == dto.Password);

            if (user == null)
                throw new Exception("Email or password is wrong");

            if (user.IsBlocked)
                throw new Exception("User is blocked");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET ALL USERS
        public async Task<List<UserGetAllDTO>> GetAllUsersAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(x => new UserGetAllDTO
            {
                
                Name = x.Name,
                Email = x.Email,
                Role = x.Role
            }).ToList();
        }

        // BLOCK USER
        public async Task BlockUserAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new Exception("User not found");

            user.IsBlocked = true;

            // Audit log
            await _context.AuditLogs.AddAsync(new AuditLog
            {
                Action = $"Blocked user with id: {id}",
                PerformedByUserId = user.Id,
                CreatedDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

        // PROMOTE USER
        public async Task PromoteToAdminAsync(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
                throw new Exception("User not found");

            user.Role = "Admin";

            // Audit log
            await _context.AuditLogs.AddAsync(new AuditLog
            {
                Action = $"Promoted user with id: {id} to admin",
                PerformedByUserId = user.Id,
                CreatedDate = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
        }

       

       
    }
}

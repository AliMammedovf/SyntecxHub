using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntecxhubUserApi.Interfaces;

namespace SyntecxhubUserApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _service;

        public AdminController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _service.GetAllUsersAsync();

            return Ok(users);
        }

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockUser(int id)
        {
            await _service.BlockUserAsync(id);

            return Ok("User blocked");
        }

        [HttpPut("promote/{id}")]
        public async Task<IActionResult> PromoteUser(int id)
        {
            await _service.PromoteToAdminAsync(id);

            return Ok("User promoted to admin");
        }
    }
}

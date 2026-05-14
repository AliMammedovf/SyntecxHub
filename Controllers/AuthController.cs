using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyntecxhubUserApi.Business.DTOs;
using SyntecxhubUserApi.Business.Services;
using SyntecxhubUserApi.Interfaces;

namespace SyntecxhubUserApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    
    
    public class AuthController : ControllerBase
    {

       
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok("secured data");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var result = await _service.RegisterAsync(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var token = await _service.LoginAsync(dto);

            return Ok(token);
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok("Protected endpoint");
        }
    }
}

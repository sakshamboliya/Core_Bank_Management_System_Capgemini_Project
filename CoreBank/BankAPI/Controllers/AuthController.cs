using Microsoft.AspNetCore.Mvc;
using BankAPI.Services;
using BankAPI.DTOs;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly CustomerService _service;
        private readonly AuthService _authService;

        public AuthController(CustomerService service, AuthService authService)
        {
            _service = service;
            _authService = authService;
        }

        // US1 - Register
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCustomerDTO dto)
        {
            var response = await _service.Register(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // US2 - Login
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.Login(request);

            if (result == null)
                return Unauthorized("Invalid credentials");

            return Ok(result);
        }
    }
}
using BankAPI.Data;
using BankAPI.DTOs;
using BankAPI.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace BankAPI.Services
{
    public class AuthService
    {
        private readonly BankDbContext _context;
        private readonly TokenService _tokenService;

        public AuthService(BankDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            // Find customer by email
            var customer = await _context.Customers
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (customer == null)
                return null;

            // Verify password using BCrypt
            bool passwordValid = BCrypt.Net.BCrypt.Verify(
                request.Password,
                customer.PasswordHash
            );

            if (!passwordValid)
                return null;

            // Generate JWT access token
            string accessToken = _tokenService.GenerateAccessToken(customer);

            // Generate refresh token
            string refreshToken = _tokenService.GenerateRefreshToken();

            // Save refresh token
           var refresh = new RefreshToken
{
    CustomerId = customer.Id,
    Token = refreshToken,
    ExpiresAt = DateTime.UtcNow.AddDays(7)
};

            _context.RefreshTokens.Add(refresh);

            await _context.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
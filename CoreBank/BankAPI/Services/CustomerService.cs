using BankAPI.DTOs;
using BankAPI.Models;
using BankAPI.Repositories;
using BankAPI.Security;

namespace BankAPI.Services
{
    public class CustomerService
    {
        private readonly ICustomerRepository _repo;
        private readonly PasswordHasher _hasher;

        public CustomerService(ICustomerRepository repo, PasswordHasher hasher)
        {
            _repo = repo;
            _hasher = hasher;
        }

        // -------------------------
        // US1 - Register Customer
        // -------------------------
     public async Task<ApiResponse<CustomerResponseDTO>> Register(RegisterCustomerDTO dto)
{
    var existing = await _repo.GetByEmail(dto.Email);

    if (existing != null)
    {
        return new ApiResponse<CustomerResponseDTO>
        {
            Success = false,
            Message = "Email already registered"
        };
    }

    var customer = new Customer
    {
        FullName = dto.FullName,
        Email = dto.Email,
        PasswordHash = _hasher.HashPassword(dto.Password)
    };

    await _repo.CreateCustomer(customer);

    var response = new CustomerResponseDTO
    {
        Id = customer.Id,
        FullName = customer.FullName,
        Email = customer.Email,
        CreatedAt = customer.CreatedAt
    };

    return new ApiResponse<CustomerResponseDTO>
    {
        Success = true,
        Message = "Customer registered successfully",
        Data = response
    };
}

        // -------------------------
        // US3 - View Customer Profile
        // -------------------------
        public async Task<ApiResponse<CustomerResponseDTO>> GetProfile(int customerId)
        {
            var customer = await _repo.GetByIdAsync(customerId);

            if (customer == null)
            {
                return new ApiResponse<CustomerResponseDTO>
                {
                    Success = false,
                    Message = "Customer not found"
                };
            }

            var response = new CustomerResponseDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                Email = customer.Email,
                CreatedAt = customer.CreatedAt
            };

            return new ApiResponse<CustomerResponseDTO>
            {
                Success = true,
                Message = "Customer profile fetched successfully",
                Data = response
            };
        }
    }
}
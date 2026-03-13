using BankAPI.Models;

namespace BankAPI.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer?> GetByEmail(string email);

        // US3 requirement
        Task<Customer?> GetByIdAsync(int id);
    }
}
using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
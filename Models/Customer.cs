using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}

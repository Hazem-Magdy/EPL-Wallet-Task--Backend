using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Wallet_Project.Data.Enums;

namespace Wallet_Project.Models
{
    public class User : IdentityUser
    {
        
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only letters.")]
        [MaxLength(100)]
        public string Name { get; set; }
        
        [RegularExpression(@"^(\+20|0)?1\d{9}$", ErrorMessage = "Mobile number must be in Egyptian format.")]
        public string Mobile { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }

        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }
    }
}

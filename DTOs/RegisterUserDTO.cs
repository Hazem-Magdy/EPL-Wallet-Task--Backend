using System.ComponentModel.DataAnnotations;
using Wallet_Project.Data.Enums;

namespace Wallet_Project.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only letters.")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^(\+20|0)?1\d{9}$", ErrorMessage = "Mobile number must be in Egyptian format.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
        ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "You must Enter the role")]
        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }


    }
}

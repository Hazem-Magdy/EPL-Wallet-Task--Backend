using System.ComponentModel.DataAnnotations;
using Wallet_Project.Data.Enums;

namespace Wallet_Project.DTOs
{
    public class RegisterUserDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Mobile { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "You must Enter the role")]
        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }


    }
}

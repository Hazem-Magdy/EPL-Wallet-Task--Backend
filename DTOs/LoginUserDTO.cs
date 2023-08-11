using System.ComponentModel.DataAnnotations;

namespace Wallet_Project.DTOs
{
    public class LoginUserDTO
    {

        [Required]
        public string Mobile { get; set; }

        [Required]
        public string Password { get; set; }

    }
}

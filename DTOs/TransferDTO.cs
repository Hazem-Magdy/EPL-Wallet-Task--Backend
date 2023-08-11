using System.ComponentModel.DataAnnotations;

namespace Wallet_Project.DTOs
{
    public class TransferDTO
    {
        [Required]
        public string ReceiverMobile { get; set; }
        [Required]
        public string SenderMobile { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}

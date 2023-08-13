using System.ComponentModel.DataAnnotations;

namespace Wallet_Project.DTOs
{
    public class TransferDTO
    {
        [Required(ErrorMessage = "Receiver mobile is required.")]
        [RegularExpression(@"^(\+20|0)?1\d{9}$", ErrorMessage = "Receiver mobile must be in Egyptian format.")]
        public string ReceiverMobile { get; set; }

        [Required(ErrorMessage = "Sender mobile is required.")]
        [RegularExpression(@"^(\+20|0)?1\d{9}$", ErrorMessage = "Sender mobile must be in Egyptian format.")]
        public string SenderMobile { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
    }
}

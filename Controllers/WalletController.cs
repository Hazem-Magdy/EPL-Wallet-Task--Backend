using Microsoft.AspNetCore.Mvc;
using Wallet_Project.DTOs;
using Wallet_Project.Models;
using Wallet_Project.Services.InterfacesServices;

namespace Wallet_Project.Controllers
{
    [Route("api/Wallet")]
    public class WalletController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public WalletController(IUserRepository userRepository, ITransactionRepository transactionRepository)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer(TransferDTO model)
        {
            var senderUser = await _userRepository.GetUserByMobileAsync(model.SenderMobile);
            var receiverUser = await _userRepository.GetUserByMobileAsync(model.ReceiverMobile);

            if (senderUser == null)
            {
                return BadRequest("Sender user not found.");
            }

            if (receiverUser == null)
            {
                return BadRequest("Receiver user not found.");
            }

            if (senderUser.Balance < model.Amount)
            {
                return BadRequest("Insufficient balance.");
            }

            await _userRepository.BeginTransactionAsync();

            try
            {
                senderUser.Balance -= model.Amount;
                receiverUser.Balance += model.Amount;

                var transactionRecord = new Transaction
                {
                    SenderUserId = senderUser.Id,
                    ReceiverUserId = receiverUser.Id,
                    Amount = model.Amount,
                    Timestamp = DateTime.UtcNow
                };

                var updateUserResult = await _userRepository.UpdateUserAsync(senderUser);
                var addTransactionResult = await _transactionRepository.AddTransactionAsync(transactionRecord);

                if (updateUserResult && addTransactionResult)
                {
                    await _userRepository.CommitTransactionAsync();
                    return Ok("Balance transfer successful.");
                }
                else
                {
                    _userRepository.RollbackTransaction();
                    return StatusCode(500, "An error occurred during balance transfer.");
                }
            }
            catch (Exception)
            {
                _userRepository.RollbackTransaction();
                return StatusCode(500, "An error occurred during balance transfer.");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet_Project.Models;
using Wallet_Project.Services.InterfacesServices;

namespace Wallet_Project.Controllers
{
    [Route("api/admin")]
    public class AdminController : Controller
    {
        private readonly ITransactionRepository _transactionRepository;

        public AdminController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpGet("balance-report")]
        public async Task<IActionResult> GetBalanceReport()
        {
            var balanceReports = await _transactionRepository.GetBalanceReportAsync();
                
            return Ok(balanceReports);
        }
    }
}

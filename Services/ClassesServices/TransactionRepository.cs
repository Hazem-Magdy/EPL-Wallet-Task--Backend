using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wallet_Project.Data;
using Wallet_Project.Models;
using Wallet_Project.Services.InterfacesServices;

namespace Wallet_Project.Services.ClassesServices
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly WalletDbContext _context;

        public TransactionRepository(WalletDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddTransactionAsync(Transaction transaction)
        {
            try
            {
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<BalanceReport>> GetBalanceReportAsync()
        {
            var balanceReports = await _context.Users
                .Select(u => new BalanceReport
                {
                    UserMobile = u.Mobile,
                    UserName = u.Name,
                    TotalSentAmount = _context.Transactions
                        .Where(t => t.SenderUserId == u.Id)
                        .Sum(t => t.Amount)
                })
                .ToListAsync();

            return balanceReports;
        }

    }
}

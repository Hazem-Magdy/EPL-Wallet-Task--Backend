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
    }
}

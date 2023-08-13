using Wallet_Project.Models;

namespace Wallet_Project.Services.InterfacesServices
{
    public interface ITransactionRepository
    {
        Task<bool> AddTransactionAsync(Transaction transaction);

        Task<List<BalanceReport>> GetBalanceReportAsync();
    }
}

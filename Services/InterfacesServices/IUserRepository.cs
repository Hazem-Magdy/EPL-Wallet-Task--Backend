using Wallet_Project.Models;

namespace Wallet_Project.Services.InterfacesServices
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string userId);
        Task<User> GetUserByMobileAsync(string mobile);
        Task<bool> UpdateUserAsync(User user);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        void RollbackTransaction();
    }
}

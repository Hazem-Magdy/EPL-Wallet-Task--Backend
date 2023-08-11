using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Wallet_Project.Data;
using Wallet_Project.Models;
using Wallet_Project.Services.InterfacesServices;

namespace Wallet_Project.Services.ClassesServices
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly WalletDbContext _context;
        private IDbContextTransaction _transaction;

        public UserRepository(UserManager<User> userManager, WalletDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<User> GetUserByMobileAsync(string mobile)
        {
            return await _userManager.FindByNameAsync(mobile);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
                _transaction.Dispose();
            }
        }

        public void RollbackTransaction()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wallet_Project.Models;

namespace Wallet_Project.Data
{
    public class WalletDbContext : IdentityDbContext<User>
    {
        public virtual DbSet<Transaction> Transactions { get; set; }
        public WalletDbContext(DbContextOptions<WalletDbContext> options) : base(options)
        {
        }
    }
   
}

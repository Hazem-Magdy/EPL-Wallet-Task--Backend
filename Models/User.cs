using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Wallet_Project.Data.Enums;

namespace Wallet_Project.Models
{
    public class User : IdentityUser
    { 
        public string Name { get; set; }
        public string Mobile { get; set; }
        public decimal Balance { get; set; }

        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }
    }
}

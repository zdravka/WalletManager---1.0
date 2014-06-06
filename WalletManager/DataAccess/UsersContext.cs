using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WalletManager.Models;

namespace WalletManager.DataAccess
{
    public class UsersContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public UsersContext()
            : base("name=Wallet")
        {
        }

        public void AddUserRole(UserRole userRole)
        {
            var roleEntry = UserRoles.SingleOrDefault(r => r.UserId == userRole.UserId);
            // var roleEntryAll = (d in ) (from d in this._detailPriceRepository.GetDetailsPrice() where idDetail == d.DetailId select d.Id).FirstOrDefault();
            var roleEntryAll = (from d in this.Roles select d);
            if (roleEntry != null)
            {
                UserRoles.Remove(roleEntry);
                SaveChanges();
            }
            UserRoles.Add(userRole);
            SaveChanges();
        }

        public User GetUser(string userName)
        {
            var user = User.SingleOrDefault(u => u.Username == userName);
            return user;
        }

        public User GetUser(string userName, string password)
        {
            var user = User.SingleOrDefault(u => u.Username == userName && u.Password == password);
            return user;
        }


    }
}
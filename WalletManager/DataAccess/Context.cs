using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WalletManager.Models;

namespace WalletManager.DataAccess
{
    public class Context : DbContext
    {
        public Context()
            : base("name=Wallet")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<Context>(null); // <--- This is what i needed

      

        base.OnModelCreating(modelBuilder);

        }

        public DbSet<MovementOfFundsModel> MovementOfFunds { get; set; }

        public DbSet<FundsTypeModel> FundsType { get; set; }
    }
}
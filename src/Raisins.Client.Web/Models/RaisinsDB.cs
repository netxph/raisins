using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class RaisinsDB : DbContext
    {

        public RaisinsDB()
            : base("Default")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>().HasRequired(p => p.Currency).WithMany().HasForeignKey(p => p.CurrencyID);
            modelBuilder.Entity<Payment>().HasRequired(p => p.Beneficiary).WithMany().HasForeignKey(p => p.BeneficiaryID);
            modelBuilder.Entity<Payment>().HasRequired(p => p.CreatedBy).WithMany().HasForeignKey(p => p.CreatedByID);
            modelBuilder.Entity<Payment>().HasOptional(p => p.AuditedBy).WithMany().HasForeignKey(p => p.AuditedByID);

            modelBuilder.Entity<Account>().HasMany(a => a.Roles).WithMany();

            modelBuilder.Entity<Activity>().HasMany(a => a.Roles).WithMany();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Activity> Activities { get; set; }

    }
}
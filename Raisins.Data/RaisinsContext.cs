using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data
{
    public class RaisinsContext : DbContext
    {
        private static RaisinsContext _instance;

        public static RaisinsContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RaisinsContext();
                }

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        public RaisinsContext() : base("Raisins")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountProfile>().HasMany(ap => ap.Beneficiaries).WithMany();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountProfile> Profiles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<MailQueue> MailQueues { get; set; }
        public DbSet<PaymentSource> Sources { get; set; }
        public DbSet<PaymentType> Types { get; set; }
    }
}
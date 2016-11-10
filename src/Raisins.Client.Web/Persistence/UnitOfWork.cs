using Raisins.Client.Web.Core;
using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Persistence.Repository;

namespace Raisins.Client.Web.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RaisinsDB _raisinsDb;

        public IBeneficiaryRepository Beneficiaries { get; private set; }
        public IAccountRepository Accounts { get; private set;  }
        public IActivityRepository Activities { get; private set; }
        public ICurrencyRepository Currencies { get; private set; }
        public IPaymentRepository Payments { get; private set; }
        public IMailQueuesRepository MailQueues { get; private set; }
        public IExecutiveRepository Executives { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public ITicketRepository Tickets { get; private set; }

        public UnitOfWork(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
            Beneficiaries = new BeneficiaryRepository(_raisinsDb);
            Accounts = new AccountRepository(_raisinsDb);
            Activities = new ActivityRepository(_raisinsDb);
            Currencies = new CurrencyRepository(_raisinsDb);
            Payments = new PaymentRepository(_raisinsDb);
            MailQueues = new MailQueuesRepository(_raisinsDb);
            Executives = new ExecutiveRepository(_raisinsDb);
        }

        public void Complete()
        {
            _raisinsDb.SaveChanges();
        }

        public void Dispose()
        {
            _raisinsDb.Dispose();
        }
    }
}
using Raisins.Client.Web.Core.Repository;

namespace Raisins.Client.Web.Core
{
    public interface IUnitOfWork
    {
        IBeneficiaryRepository Beneficiaries { get; }
        IAccountRepository Accounts { get; }
        IPaymentRepository Payments { get; }
        IActivityRepository Activities { get; }
        ICurrencyRepository Currencies { get; }
        IMailQueuesRepository MailQueues { get; }
        IExecutiveRepository Executives { get; }
        IRoleRepository Roles { get; }
        ITicketRepository Tickets { get; }
        void Complete();
        void Dispose();

    }
}
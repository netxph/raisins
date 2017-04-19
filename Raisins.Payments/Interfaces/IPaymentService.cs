
using D = Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Interfaces
{
    public interface IPaymentService
    {
        IEnumerable<D.PaymentSummary> GetSummary();
        decimal GetTotal();
        D.Payment Get(int paymentID);
        D.Payments GetAll();
        D.Payments GetByAccount(string userName);
        D.Payments GetByBeneficiary(string name);
        D.Payments GetByProfile(string userName);
        IEnumerable<D.AccountProfile> GetAllAccount();
        D.AccountProfile GetProfile(string userName);
        void Create(D.Payment payment);
        void Import(IEnumerable<D.Payment> payments);
        void Publish(D.Payment payment);
        void PublishAll(IEnumerable<D.Payment> payments);
        void Edit(D.Payment payment);
        
    }
}

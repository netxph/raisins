using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Interfaces
{
    public interface IPaymentRepository
    {
        Models.Payments GetAll();
        Payment GetByID(int paymentID);
        bool Exists(Payment payment);
        Models.Payments GetByAccount(string userName);
        Models.Payments GetByBeneficiary(int[] beneficiaryIds);
        Models.Payments GetByBeneficiary(int beneficiaryID);
        Models.Payments GetByBeneficiary(string name);
        Models.Payments GetLocked();
        Models.Payments GetWithCurrency();
        void Edit(Payment payment);
        void Add(Payment payment);
        void Delete(Payment payment);
    }
}

using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface IPaymentRepository
    {
        IEnumerable<Payment> GetAll();
        Payment GetPayment(int id);
        IEnumerable<Payment> GetPayment(Account currentAccount);
        IEnumerable<Payment> GetPaymentByBeneficiary(int[] beneficiaryIds);
        IEnumerable<Payment> GetLockedPayments();
        IEnumerable<Payment> GetPaymentWithCurrency();
        void Edit(Payment payment);
        void Add(Payment payment);
        void Delete(Payment payment);
    }


}
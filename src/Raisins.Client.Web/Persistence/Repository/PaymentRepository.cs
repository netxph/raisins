using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private RaisinsDB _raisinsDb;

        public PaymentRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _raisinsDb.Payments
                    .Include(p => p.Tickets)
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.AuditedBy)
                    .Include(p => p.Executive);
        }

        public IEnumerable<Payment> GetPaymentWithCurrency()
        {
            return _raisinsDb.Payments.Include(path => path.Currency);
        }
        public IEnumerable<Payment> GetPaymentByBeneficiary(int[] beneficiaryIds)
        {
            return GetAll().Where(p => beneficiaryIds.Contains(p.BeneficiaryID));
        }

        public Payment GetPayment(int id = 0)
        {
            return GetAll().FirstOrDefault(p => p.ID == id);
        }

        public IEnumerable<Payment> GetPayment(Account currentAccount)
        {

            var beneficiaryIds = currentAccount.Profile.Beneficiaries.Select(b => b.ID).ToArray();

            IEnumerable<Payment> payments = null;

            //convert to activity
            if (currentAccount.Roles.Exists(r => r.Name == "User"))
            {
                payments = GetPaymentByBeneficiary(beneficiaryIds).Where(p => p.CreatedByID == currentAccount.ID);
            }
            else if (currentAccount.Roles.Exists(r => r.Name == "Accountant"))
            {
                payments = GetPaymentByBeneficiary(beneficiaryIds);
            }
            else
            {
                payments = GetAll();
            }

            return payments;    
        }

        public IEnumerable<Payment> GetLockedPayments()
        {
            return _raisinsDb.Payments
                    .Where(p => p.Locked == true)
                    .Include(p => p.Tickets)
                    .ToList();
        }

        //TODO: payment.CreatedByID = Account.GetCurrentUser().ID; before edit payment
        public void Edit(Payment payment)
        {
            _raisinsDb.Entry(payment).State = EntityState.Modified;
        }

        //TODO: payment.CreatedByID = Account.GetCurrentUser().ID; before adding payment
        public void Add(Payment payment)
        {
            _raisinsDb.Payments.Add(payment);
        }

        public void Delete(Payment payment)
        {
            _raisinsDb.Payments.Remove(payment);
        }
    }
}
using Raisins.Payments.Interfaces;
using D = Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private RaisinsContext _context;

        public PaymentRepository() : this(new RaisinsContext())
        {
        }
        public PaymentRepository(RaisinsContext context)
        {
            _context = context;
        }

        public D.Payments GetAll()
        {
            return ConvertToDomainList(_context.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy));
        }

        public D.Payments GetWithCurrency()
        {
            return ConvertToDomainList(_context.Payments.Include(path => path.Currency));
        }
        public D.Payments GetByAccount(string userName)
        {
            int accountID = _context.Accounts.FirstOrDefault(a => a.UserName == userName).AccountID;
            return ConvertToDomainList(GetAllEF().Where(p => p.CreatedByID == accountID));
        }

        public D.Payments GetByBeneficiary(int[] beneficiaryIds)
        {
            return ConvertToDomainList(GetAllEF().Where(p => beneficiaryIds.Contains(p.BeneficiaryID)));
        }

        public D.Payments GetByBeneficiary(int beneficiaryID)
        {
            return ConvertToDomainList(GetAllEF().Where(p => p.BeneficiaryID == beneficiaryID));
        }
        public D.Payments GetByBeneficiary(string name)
        {
            return ConvertToDomainList(GetAllEF().Where(p => p.Beneficiary.Name == name));
        }

        public D.Payment GetByID(int paymentID)
        {
            return ConvertToDomain(GetAllEF().FirstOrDefault(p => p.PaymentID == paymentID));
        }

        public bool Exists(D.Payment payment)
        {
            return GetAllEF().Equals(payment);
        }

        public D.Payments GetLocked()
        {
            return ConvertToDomainList(_context.Payments
                    .Where(p => p.Locked == true)
                    .ToList());
        }

        //TODO: payment.CreatedByID = Account.GetCurrentUser().ID; before edit payment
        public void Edit(D.Payment payment)
        {
            EF.Payment efpayment = ConverToEFwithID(payment);
            _context.Entry(efpayment).State = EntityState.Modified;
            _context.SaveChanges();
        }

        //TODO: payment.CreatedByID = Account.GetCurrentUser().ID; before adding payment
        public void Add(D.Payment payment)
        {
            _context.Payments.Add(ConvertToEF(payment));
            _context.SaveChanges();
        }

        public void Delete(D.Payment payment)
        {
            _context.Payments.Remove(ConvertToEF(payment));
            _context.SaveChanges();
        }

        private D.Payment ConvertToDomain(EF.Payment efPayment)
        {
            EF.Beneficiary efBeneficiary = _context.Beneficiaries.FirstOrDefault(b => b.BeneficiaryID == efPayment.BeneficiaryID);
            EF.Currency efCurrency = _context.Currencies.FirstOrDefault(c => c.CurrencyID == efPayment.CurrencyID);

            D.Beneficiary beneficiary = new D.Beneficiary(efBeneficiary.Name, efBeneficiary.Description);
            D.Currency currency = new D.Currency(efCurrency.CurrencyCode, efCurrency.Ratio, efCurrency.ExchangeRate);

            D.PaymentSource source = new D.PaymentSource(efPayment.PaymentSource.Source);
            D.PaymentType type = new D.PaymentType(efPayment.PaymentType.Type);

            string createdBy = _context.Accounts.FirstOrDefault(b => b.AccountID == efPayment.CreatedByID).UserName;
            string modifiedBy = "";
            if(efPayment.ModifiedByID > 0)
            {
                modifiedBy = _context.Accounts.FirstOrDefault(b => b.AccountID == efPayment.ModifiedByID).UserName;
            }

            return new D.Payment(efPayment.PaymentID, efPayment.Name, efPayment.Amount, currency, beneficiary, efPayment.Locked,
                efPayment.Email, efPayment.CreatedDate, efPayment.ModifiedDate, efPayment.PaymentDate, efPayment.PublishDate, createdBy, modifiedBy, source, type, efPayment.OptOut);
        }

        private D.Payments ConvertToDomainList(IEnumerable<EF.Payment> efPaymets)
        {
            D.Payments payments = new D.Payments();
            foreach (var efPayment in efPaymets)
            {
                payments.Add(ConvertToDomain(efPayment));
            }
            return payments;
        }

        private IEnumerable<EF.Payment> GetAllEF()
        {
            return _context.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy);
        }
        private EF.Payment ConvertToEF(D.Payment payment)
        {
            int beneficiaryID = _context.Beneficiaries.FirstOrDefault(b => b.Name == payment.Beneficiary.Name).BeneficiaryID;
            int currencyID = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == payment.Currency.CurrencyCode).CurrencyID;
            int sourceID = _context.Sources.FirstOrDefault(c => c.Source == payment.Source.Source).PaymentSourceID;
            int typeID = _context.Types.FirstOrDefault(c => c.Type == payment.Type.Type).PaymentTypeID;
            int createdByID = _context.Accounts.FirstOrDefault(c => c.UserName == payment.CreatedBy).AccountID;
            return new EF.Payment(payment.Name, payment.Amount, beneficiaryID, currencyID, payment.Email, payment.CreatedDate,
                payment.PaymentDate, createdByID, sourceID, typeID, payment.OptOut);
        }
        private EF.Payment ConverToEFwithID(D.Payment payment)
        {
            int beneficiaryID = _context.Beneficiaries.FirstOrDefault(b => b.Name == payment.Beneficiary.Name).BeneficiaryID;
            int currencyID = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == payment.Currency.CurrencyCode).CurrencyID;
            int sourceID = _context.Sources.FirstOrDefault(c => c.Source == payment.Source.Source).PaymentSourceID;
            int typeID = _context.Types.FirstOrDefault(c => c.Type == payment.Type.Type).PaymentTypeID;
            int createdByID = _context.Accounts.FirstOrDefault(c => c.UserName == payment.CreatedBy).AccountID;
            int modifiedByID = _context.Accounts.FirstOrDefault(c => c.UserName == payment.ModifiedBy).AccountID;
            return new EF.Payment(payment.PaymentID, payment.Name, payment.Amount, beneficiaryID, currencyID, payment.Locked,
                payment.Email, payment.CreatedDate, payment.PaymentDate, payment.ModifiedDate, payment.PublishDate, createdByID,
                modifiedByID, sourceID, typeID, payment.OptOut);
        }
    }
}

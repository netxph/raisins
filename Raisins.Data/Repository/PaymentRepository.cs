using Raisins.Payments.Interfaces;
using D = Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA = Raisins.Data.Models;
using System.Reflection;

namespace Raisins.Data.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private RaisinsContext _context;

        public PaymentRepository() : this(RaisinsContext.Instance)
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
                    .Include(p => p.CreatedBy)
                    .Include(p => p.PaymentSource)
                    .Include(p => p.CreatedBy)
                    .Include(p => p.ModifiedBy)
                    .Include(p => p.PaymentType)
                  );
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
            DATA.Payment efpayment = ConverToEFwithID(payment);
            
            var tempPayment = _context.Payments.Single(a => a.PaymentID == efpayment.PaymentID);

            tempPayment.PaymentID       = efpayment.PaymentID;
            tempPayment.Name            = efpayment.Name;
            tempPayment.Amount          = efpayment.Amount;
            tempPayment.BeneficiaryID   = efpayment.BeneficiaryID;
            tempPayment.CurrencyID      = efpayment.CurrencyID;
            tempPayment.Locked          = efpayment.Locked;
            tempPayment.Email           = efpayment.Email;
            //tempPayment.CreatedDate   = efpayment.CreatedDate;
            //tempPayment.PaymentDate   = efpayment.PaymentDate;
            tempPayment.ModifiedDate    = efpayment.ModifiedDate;
            tempPayment.PublishDate     = efpayment.PublishDate;
            tempPayment.CreatedByID     = efpayment.CreatedByID;
            tempPayment.ModifiedByID    = efpayment.ModifiedByID;
            tempPayment.PaymentSourceID = efpayment.PaymentSourceID;
            tempPayment.PaymentTypeID   = efpayment.PaymentTypeID;
            tempPayment.OptOut          = efpayment.OptOut;

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
            throw new NotImplementedException();
            //DATA.Payment efpayment = ConverToEFwithID(payment);
            //_context.Payments.Attach(ConvertToEF(payment));
            //_context.Entry(efpayment).State = EntityState.Deleted;
            //_context.Payments.Remove(ConvertToEF(payment));
            //_context.SaveChanges();
        }

        private D.Payment ConvertToDomain(DATA.Payment efPayment)
        {
            var beneficiary = new D.Beneficiary(efPayment.Beneficiary.Name, efPayment.Beneficiary.Description);
            var currency = new D.Currency(efPayment.Currency.CurrencyCode, efPayment.Currency.Ratio, efPayment.Currency.ExchangeRate);
            var source = new D.PaymentSource(efPayment.PaymentSource.Source);
            var type = new D.PaymentType(efPayment.PaymentType.Type);
            var createdBy = efPayment.CreatedBy.UserName;
            var modifiedBy = efPayment.ModifiedBy.UserName;

            return new D.Payment(efPayment.PaymentID, efPayment.Name, efPayment.Amount, currency, beneficiary, efPayment.Locked,
                efPayment.Email, efPayment.CreatedDate, efPayment.ModifiedDate, efPayment.PaymentDate, efPayment.PublishDate, createdBy, modifiedBy, source, type, efPayment.OptOut);
        }

        private D.Payments ConvertToDomainList(IEnumerable<DATA.Payment> efPaymets)
        {
            D.Payments payments = new D.Payments();
            foreach (var efPayment in efPaymets)
            {
                payments.Add(ConvertToDomain(efPayment));
            }
            return payments;
        }

        private IEnumerable<DATA.Payment> GetAllEF()
        {
            return _context.Payments
                    .Include(p => p.Beneficiary)
                    .Include(p => p.Currency)
                    .Include(p => p.CreatedBy);
        }
        private DATA.Payment ConvertToEF(D.Payment payment)
        {
            int beneficiaryID = _context.Beneficiaries.FirstOrDefault(b => b.Name == payment.Beneficiary.Name).BeneficiaryID;
            int currencyID    = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == payment.Currency.CurrencyCode).CurrencyID;
            int sourceID      = _context.Sources.FirstOrDefault(c => c.Source == payment.Source.Source).PaymentSourceID;
            int typeID        = _context.Types.FirstOrDefault(c => c.Type == payment.Type.Type).PaymentTypeID;
            int createdByID   = _context.Accounts.FirstOrDefault(c => c.UserName == payment.CreatedBy).AccountID;
            int modifiedByID  = _context.Accounts.FirstOrDefault(c => c.UserName == payment.ModifiedBy).AccountID;

            return new DATA.Payment(payment.Name, payment.Amount, beneficiaryID, currencyID, payment.Email, payment.CreatedDate,
                payment.PaymentDate, payment.CreatedDate, createdByID, sourceID, typeID, payment.OptOut, modifiedByID);
        }
        private DATA.Payment ConverToEFwithID(D.Payment payment)
        {
            int beneficiaryID = _context.Beneficiaries.FirstOrDefault(b => b.Name == payment.Beneficiary.Name).BeneficiaryID;
            int currencyID    = _context.Currencies.FirstOrDefault(c => c.CurrencyCode == payment.Currency.CurrencyCode).CurrencyID;
            int sourceID      = _context.Sources.FirstOrDefault(c => c.Source == payment.Source.Source).PaymentSourceID;
            int typeID        = _context.Types.FirstOrDefault(c => c.Type == payment.Type.Type).PaymentTypeID;
            int createdByID   = _context.Accounts.FirstOrDefault(c => c.UserName == payment.CreatedBy).AccountID;

            int paymentID = _context.Payments.FirstOrDefault(p => p.PaymentID == payment.PaymentID).PaymentID;

            var accounts     = _context.Accounts;
            var account      = accounts.FirstOrDefault(c => c.UserName == payment.ModifiedBy);
            int modifiedByID = account.AccountID;

            return new DATA.Payment(payment.PaymentID, payment.Name, payment.Amount, beneficiaryID, currencyID, payment.Locked,
                payment.Email, payment.CreatedDate, payment.PaymentDate, payment.ModifiedDate, payment.PublishDate, createdByID,
                modifiedByID, sourceID, typeID, payment.OptOut);
        }
    }
}

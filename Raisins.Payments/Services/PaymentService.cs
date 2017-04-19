using Raisins.Payments.Interfaces;
using P = Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBeneficiaryForPaymentRepository _beneficiaryRepository;
        private readonly IProfileRepository _profileRepository;
        protected IPaymentRepository PaymentRepository { get { return _paymentRepository; } }
        protected IBeneficiaryForPaymentRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }
        protected IProfileRepository ProfileRepository { get { return _profileRepository; } }
        public PaymentService(IPaymentRepository paymentRepository, IBeneficiaryForPaymentRepository beneficiaryRepository, IProfileRepository profileRepository)
        {
            if (paymentRepository == null)
            {
                throw new ArgumentNullException("PaymentService:paymentRepository");
            }
            _paymentRepository = paymentRepository;
            if (beneficiaryRepository == null)
            {
                throw new ArgumentNullException("PaymentService:beneficiaryRepository");
            }
            _beneficiaryRepository = beneficiaryRepository;
            if (profileRepository == null)
            {
                throw new ArgumentNullException("PaymentService:profileRepository");
            }
            _profileRepository = profileRepository;
        }
        public P.Payment Get(int paymentID)
        {
            return PaymentRepository.GetByID(paymentID);
        }
        public IEnumerable<P.PaymentSummary> GetSummary()
        {
            var beneficiaries = BeneficiaryRepository.GetAll();
            var summaries = new List<P.PaymentSummary>();
            foreach (var beneficiary in beneficiaries)
            {
                summaries.Add(new P.PaymentSummary(beneficiary.Name, _paymentRepository.GetByBeneficiary(beneficiary.Name), "PHP"));
            }
            return summaries;
        }
        public decimal GetTotal()
        {
            return _paymentRepository.GetAll().GetTotal();
        }
        public P.Payments GetAll()
        {
            return PaymentRepository.GetAll();
        }
        public P.Payments GetByAccount(string userName)
        {
            return PaymentRepository.GetByAccount(userName);
        }
        public P.Payments GetByBeneficiary(string name)
        {
            return PaymentRepository.GetByBeneficiary(name);
        }

        public P.Payments GetByProfile(string userName)
        {
            P.AccountProfile profile = ProfileRepository.GetProfile(userName);
            IEnumerable<P.Beneficiary> beneficiaries = profile.Beneficiaries;
            var payments = new Models.Payments();
            foreach (var beneficiary in beneficiaries)
            {
                payments.AddRange(PaymentRepository.GetByBeneficiary(beneficiary.Name));
            }
            return payments;
        }

        public IEnumerable<P.AccountProfile> GetAllAccount()
        {
            return ProfileRepository.GetAll();
        }
        public P.AccountProfile GetProfile(string userName) {
            return ProfileRepository.GetProfile(userName);
        }
        public void Create(P.Payment payment)
        {
            PaymentRepository.Add(payment);
        }
        
        public void Import(IEnumerable<P.Payment> payments)
        {
            foreach (var payment in payments)
            {
                PaymentRepository.Add(payment);
            }
        }

        public void Publish(P.Payment payment)
        {
            payment.Publish();
            PaymentRepository.Edit(payment);
        }

        public void PublishAll(IEnumerable<P.Payment> payments)
        {
            foreach (var payment in payments)
            {
                payment.Publish();
                PaymentRepository.Edit(payment);
            }            
        }

        public void Edit(P.Payment payment)
        {
            PaymentRepository.Edit(payment);
        }
    }
}

using Raisins.Payments.Interfaces;
using P = Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Tickets;
using Raisins.Tickets.Interfaces;

namespace Raisins.Payments.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IBeneficiaryForPaymentRepository _beneficiaryRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ITicketService _tickerService;

        protected IPaymentRepository PaymentRepository { get { return _paymentRepository; } }
        protected IBeneficiaryForPaymentRepository BeneficiaryRepository { get { return _beneficiaryRepository; } }
        protected IProfileRepository ProfileRepository { get { return _profileRepository; } }
        protected ITicketService TicketService { get { return _tickerService; } }

        public PaymentService(
            IPaymentRepository paymentRepository,
            IBeneficiaryForPaymentRepository beneficiaryRepository,
            IProfileRepository profileRepository,
            ITicketService ticketService)
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

            if (ticketService == null)
            {
                throw new ArgumentNullException("PaymentService:ticketService");
            }
            _tickerService = ticketService;
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
            var payments = PaymentRepository.GetAll();

            FillTickets(payments);

            payments.TotalTickets = payments.Select(t => t.Tickets).Sum();

            return payments;
        }

        private void FillTickets(P.Payments payments)
        {
            foreach (var payment in payments)
            {
                if (!payment.OptOut)
                {
                    payment.Tickets = TicketService.CalculateTickets(
                                                        payment.Amount,
                                                        new Tickets.Models.Currency(
                                                                    payment.Currency.CurrencyCode,
                                                                    payment.Currency.Ratio,
                                                                    payment.Currency.ExchangeRate));
                }
                else
                {
                    payment.Tickets = 0;
                }
            }
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
            var dbPayments = PaymentRepository.GetAll();

            var paymentsById = RemoveDuplicate(payments, dbPayments);

            OnImport(paymentsById, dbPayments);
        }


        // Duplicate = Same Payment ID and payment in DB is Locked
        protected IEnumerable<P.Payment> RemoveDuplicate(IEnumerable<P.Payment> payments, P.Payments dbPayments)
        {
            var paymentsToImport = payments.ToList();

            foreach (var paymentById in payments)
            {
                if (dbPayments.FirstOrDefault(dbPayment => paymentById.PaymentID == dbPayment.PaymentID &&
                                                           dbPayment.Locked) != null)
                {
                    paymentsToImport.Remove(paymentById);
                }
            }

            return paymentsToImport;
        }

        protected virtual void OnImport(IEnumerable<P.Payment> payments, P.Payments dbPayments)
        {
            foreach (var payment in payments)
            {
                if (!string.IsNullOrEmpty(payment.Name))
                {
                    if (dbPayments.FirstOrDefault(dbPayment => dbPayment.PaymentID == payment.PaymentID) != null)
                    {
                        PaymentRepository.Edit(payment);
                    }
                    else
                    {
                        PaymentRepository.Add(payment);
                    }
                }
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

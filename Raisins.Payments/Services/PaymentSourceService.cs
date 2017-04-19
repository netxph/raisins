using Raisins.Payments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raisins.Payments.Models;

namespace Raisins.Payments.Services
{
    public class PaymentSourceService : IPaymentSourceService
    {
        private readonly IPaymentSourceRepository _paymentSourceRepository;
        protected IPaymentSourceRepository PaymentSourceRepository { get { return _paymentSourceRepository; } }

        public PaymentSourceService(IPaymentSourceRepository paymentSourceRepository)
        {
            if (paymentSourceRepository == null)
            {
                throw new ArgumentNullException("PaymentSourceService:paymentSourceRepository");
            }

            _paymentSourceRepository = paymentSourceRepository;
        }
        public PaymentSource Get(string source)
        {
            return PaymentSourceRepository.Get(source);
        }

        public IEnumerable<PaymentSource> GetAll()
        {
            return PaymentSourceRepository.GetAll();
        }
    }
}

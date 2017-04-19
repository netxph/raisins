using Raisins.Payments.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using D = Raisins.Payments.Models;

namespace Raisins.Payments.Services
{
    public class PaymentTypeService : IPaymentTypeService
    {
        private readonly IPaymentTypeRepository _paymentTypeRepository;
        protected IPaymentTypeRepository PaymentTypeRepository { get { return _paymentTypeRepository; } }

        public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository)
        {
            if (paymentTypeRepository == null)
            {
                throw new ArgumentNullException("PaymentTypeService:paymentTypeRepository");
            }

            _paymentTypeRepository = paymentTypeRepository;
        }
        public D.PaymentType Get(string type)
        {
            return PaymentTypeRepository.Get(type);
        }

        public IEnumerable<D.PaymentType> GetAll()
        {
            return PaymentTypeRepository.GetAll();
        }
    }
}

using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using Raisins.Payments.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class PaymentsListByBeneficiaryController : ApiController
    {
        private readonly IPaymentService _service;
        protected IPaymentService Service { get { return _service; } }
        public PaymentsListByBeneficiaryController() : this(new PaymentService(new PaymentRepository(), new BeneficiaryForPaymentRepository(), new ProfileRepository()))
        {
        }
        public PaymentsListByBeneficiaryController(IPaymentService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("PaymentsListAllController:service");
            }
            _service = service;
        }
        [HttpGet]
        public IEnumerable<Payment> GetPaymentsList(string beneficiarySelected)
        {
            return Service.GetByBeneficiary(beneficiarySelected);
        }
    }
}

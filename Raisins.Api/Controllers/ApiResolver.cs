using Raisins.Data.Repository;
using Raisins.Payments.Interfaces;
using Raisins.Payments.Services;
using Raisins.Tickets.Interfaces;
using Raisins.Tickets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Api.Controllers
{
    public class ApiResolver
    {
        public IPaymentService PaymentService
        {
            get
            {
                return new PaymentService(new PaymentRepository(),
                                          new BeneficiaryForPaymentRepository(),
                                          new ProfileRepository(),
                                          TicketService);
            }
        }

        public ITicketService TicketService
        {
            get
            {
                return new TicketService(new TicketRepository(),
                                         new BeneficiaryForTicketRepository(),
                                         new TicketCalculator());
            }
        }
    }
}
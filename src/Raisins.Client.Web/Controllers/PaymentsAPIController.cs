using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers
{
    [AllowAnonymous]
    public class PaymentsAPIController : ApiController
    {
        // GET api/paymentsapi
        //public IEnumerable<string> Get()
        public IEnumerable<Payment> Get()
        {
            List<Payment> payments = Payment.GetAll();
            //how to serialize payments into JSON?
            return payments;
        }

        // GET api/paymentsapi/5
        public Payment Get(int id)
        {
            Payment payment = Payment.Find(id);
            return payment;
        }

        // POST api/paymentsapi --create
        public void Post([FromBody]string value)
        {
        }

        // PUT api/paymentsapi/5 --edit
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/paymentsapi/5
        public void Delete(int id)
        {
            Payment.Delete(id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers.Api
{
    public class PaymentsController : ApiController
    {
        private RaisinsDB db = new RaisinsDB();

        // GET api/Payments
        public IEnumerable<Payment> GetPayments()
        {
            var payments = db.Payments.Include(p => p.Beneficiary).Include(p => p.Currency).Include(p => p.CreatedBy).Include(p => p.AuditedBy);
            return payments.AsEnumerable();
        }

        // GET api/Payments/5
        public Payment GetPayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return payment;
        }

        // PUT api/Payments/5
        public HttpResponseMessage PutPayment(int id, Payment payment)
        {
            if (ModelState.IsValid && id == payment.ID)
            {
                db.Entry(payment).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Payments
        public HttpResponseMessage PostPayment(Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, payment);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = payment.ID }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Payments/5
        public HttpResponseMessage DeletePayment(int id)
        {
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Payments.Remove(payment);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, payment);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
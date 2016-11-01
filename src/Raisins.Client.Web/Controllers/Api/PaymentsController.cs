using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Client.Web.Controllers.Api
{
    public class PaymentsController : ApiController
    {
        private IUnitOfWork _unitOfWork;

        public PaymentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET api/Payments
        public IEnumerable<Payment> GetPayments()
        {
            System.Threading.Thread.Sleep(3000);
            return _unitOfWork.Payments.GetAll();
        }

        // GET api/Payments/5
        public Payment GetPayment(int id)
        {
            Payment payment = _unitOfWork.Payments.GetPayment(id);
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
                _unitOfWork.Payments.Edit(payment);

                try
                {
                    _unitOfWork.Complete();
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
                _unitOfWork.Payments.Add(payment);
                _unitOfWork.Complete();

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
            Payment payment = _unitOfWork.Payments.GetPayment(id);
            if (payment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _unitOfWork.Payments.Delete(payment);

            try
            {
                _unitOfWork.Complete();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, payment);
        }

        protected override void Dispose(bool disposing)
        {
            _unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
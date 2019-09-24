using Newtonsoft.Json;
using Raisins.Client.ActionFilters;
using Raisins.Client.Models;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Controllers
{
    public class PublishController : BaseClientController
    {
        //[PaymentMultiplePermission("payments_publish")]
        [HttpPost]
        public ActionResult PublishAllPayments()
        {
            var clientPA = new RestClient(AppConfig.GetUrl("paymentslistall"));
            var requestPA = new RestRequest(Method.GET);
            var responsePA = clientPA.Execute<List<Payment>>(requestPA);

            //List<Payment> payment = JsonConvert.DeserializeObject<List<Payment>>(responsePA.Content);

            JsonDeserializer deserialize = new JsonDeserializer();
            List<Payment> paymentsList = deserialize.Deserialize<List<Payment>>(responsePA);

            List<Payment> paymentsToPublish = BuildPaymentsToPublish(paymentsList);

            var clientP = new RestClient(AppConfig.GetUrl("PaymentsPublishAll"));
            var requestP = new RestRequest(Method.POST);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(paymentsToPublish, settings);
            requestP.AddJsonBody(paymentsToPublish);
            var responseP = clientP.Execute(requestP);

            var clientT = new RestClient(AppConfig.GetUrl("TicketsAll"));
            var requestT = new RestRequest(Method.POST);
            requestT.AddParameter("Application/Json", body, ParameterType.RequestBody);
            requestT.AddJsonBody(paymentsToPublish);
            var responseT = clientT.Execute<List<Payment>>(requestT);
            //var responseT = clientT.Execute(requestT);

            var clientM = new RestClient(AppConfig.GetUrl("MailQueuesAll"));
            var requestM = new RestRequest(Method.POST);
            requestM.AddJsonBody(paymentsToPublish);
            var responseM = clientM.Execute(requestM);

            return Redirect("/Payments/ViewPaymentList");
        }

        private List<Payment> BuildPaymentsToPublish(List<Payment> paymentsList)
        {
            string modifiedBy = HttpContext.Session["user"].ToString();

            List<Payment> paymentsPublish = new List<Payment>();

            foreach (var payment in paymentsList)
            {
                if (!payment.Locked)
                {
                    payment.PublishDate = DateTime.Now;
                    payment.ModifiedDate = DateTime.Now;
                    payment.ModifiedBy = modifiedBy;

                    paymentsPublish.Add(payment);
                }
            }
            return paymentsPublish;
        }
    }
}
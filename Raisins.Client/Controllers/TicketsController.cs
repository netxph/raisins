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
    public class TicketsController : Controller
    {
        // GET: Tickets

        [TicketPermission("tickets_create")]
        [HttpPost]
        public ActionResult GenerateTickets(int paymentID)
        {
            var client = new RestClient(AppConfig.GetUrl("payments"));
            var request = new RestRequest(Method.GET);
            request.AddParameter("paymentID", paymentID);
            var response = client.Execute<List<Payment>>(request);
            JsonDeserializer deserialize = new JsonDeserializer();
            Payment payment = deserialize.Deserialize<Payment>(response);

            var clientP = new RestClient(AppConfig.GetUrl("tickets"));
            var requestP = new RestRequest(Method.POST);
            requestP.AddJsonBody(payment);
            var responseP = clientP.Execute(requestP);

            return RedirectToAction("Index", "Home");
        }
    }
}
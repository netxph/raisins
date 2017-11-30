using Raisins.Client.ViewModels;
using Raisins.Tickets.Models;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Controllers
{
    public class RaffleController : BaseClientController
    {
        public ActionResult RaffleView()
        {
            return View();
        }

        public ActionResult RaffleLocal()
        {
            return Raffle("Local");
        }

        public ActionResult RaffleExternal()
        {
            return Raffle("External");
        }

        public ActionResult RaffleInternational()
        {
            return Raffle("International");
        }

        private ActionResult Raffle(string paymentSource)
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient(AppConfig.GetUrl("raffle"));
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("paymentSource", paymentSource);

            var response = client.Execute(request);
            var ticket = deserialize.Deserialize<Ticket>(response);

            var viewModel = new RaffleViewModel();
            viewModel.Winner = ticket.Name ?? string.Empty;

            return View(viewModel);
        }
    }
}
﻿using Raisins.Client.ActionFilters;
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
        [BasicPermissions("raffle_ticket")]
        [HttpGet]
        public ActionResult RaffleView()
        {
            return View();
        }

        [BasicPermissions("raffle_ticket")]
        [HttpGet]
        public ActionResult RaffleLocal()
        {
            return Raffle("Local");
        }

        [BasicPermissions("raffle_ticket")]
        [HttpGet]
        public ActionResult RaffleExternal()
        {
            return Raffle("External");
        }

        [BasicPermissions("raffle_ticket")]
        [HttpGet]
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
            var tickets = deserialize.Deserialize<List<Ticket>>(response);

            var viewModel = new RaffleViewModel();
            
            viewModel.Winner = tickets.Select(ticket => ticket.Name).ToList();
            viewModel.TicketCode = tickets.Select(ticket => ticket.TicketCode).ToList();
            
            return View(viewModel);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raisins.Services;

namespace Raisins.Client.Web.Models
{
    public class TicketService
    {
        
        #region Operations

        public static TicketModel[] FindAll()
        {
            Ticket[] tickets = Ticket.FindAll();
            List<TicketModel> models = new List<TicketModel>();

            foreach (Ticket ticket in tickets)
            {
                models.Add(ToModel(ticket));
            }

            return models.ToArray();
        }

        public static TicketModel[] FindByPayment(long id)
        {
            var tickets = Payment.Find(id).Tickets.ToArray();
            List<TicketModel> models = new List<TicketModel>();

            foreach (var ticket in tickets)
            {
                models.Add(ToModel(ticket));
            }

            return models.ToArray();
        }

        #endregion

        #region Helper methods

        public static TicketModel ToModel(Ticket data)
        {
            TicketModel model = new TicketModel();
            model.Name = data.Name;
            model.TicketCode = data.TicketCode;

            return model;
        }

        public static Ticket ToData(TicketModel model)
        {
            Ticket data = new Ticket();
            return data;
            
        }
        #endregion


        
    }
}
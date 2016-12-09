using Raisins.Client.Randomizer.Interfaces;
using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raisins.Client.Raffle
{
    public class RaffleService
    {
        private readonly IEnumerable<Ticket> _tickets;

        protected IEnumerable<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
        }

        private readonly IRaisinsDataProvider _dataProvider;
        private readonly IIntegerRandomizerService _randomizer;

        protected IRaisinsDataProvider DataProvider
        {
            get
            {
                return _dataProvider;
            }
        }

        protected IIntegerRandomizerService Randomizer
        {
            get
            {
                return _randomizer;
            }
        }

        public RaffleService(IRaisinsDataProvider dataProvider, IIntegerRandomizerService randomizer)
        {
            if(dataProvider == null)
            {
                throw new ArgumentNullException("dataProvider");
            }

            _dataProvider = dataProvider;

            if(randomizer == null)
            {
                throw new ArgumentNullException("randomizer");
            }

            _randomizer = randomizer;
            _tickets = new List<Ticket>();
        }

        public Ticket GetRandomTicket(PaymentClass paymentClass)
        {
            var tickets = GetTickets(paymentClass);

            var index = Randomizer.GetNext(0, tickets.Count());

            return tickets.ElementAt(index);
        }

        public IEnumerable<Ticket> GetTickets(PaymentClass paymentClass)
        {
            var code = ((int)paymentClass).ToString("00");

            return DataProvider.GetTickets().Where(t => t.TicketCode.StartsWith(code));
        }

        public void LoadData()
        {
            DataProvider.LoadData();
        }
    }
}

using Raisins.Client.Randomizer.Interfaces;
using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Raisins.Client.Raffle
{
    public class RaffleService
    {
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
        }

        public Ticket GetRandomTicket(PaymentClass paymentClass)
        {
            var tickets = DataProvider.GetTicketsByPaymentClass(paymentClass);

            var index = Randomizer.GetNext(0, tickets.Count());

            return tickets.ElementAt(index);
        }

        public IEnumerable<Ticket> GetTickets(PaymentClass paymentClass)
        {
            return DataProvider.GetTicketsByPaymentClass(paymentClass);
        }
    }
}

using Raisins.Client.Randomizer.Interfaces;
using Raisins.Raffles.Interfaces;
using Raisins.Tickets.Interfaces;
using T = Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Raffles.Services
{
    public class RaffleService : IRaffleService
    {
        private readonly IEnumerable<T.Ticket> _tickets;

        private readonly IIntegerRandomizerService _randomizer;

        protected IIntegerRandomizerService Randomizer
        {
            get
            {
                return _randomizer;
            }
        }

        private readonly ITicketRepository _repository;

        public ITicketRepository Repository
        {
            get { return _repository; }
        }

        public RaffleService(ITicketRepository repository, IIntegerRandomizerService randomizer)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("Raffleservice:repository");
            }
            _repository = repository;

            if (randomizer == null)
            {
                throw new ArgumentNullException("RaffleService:randomizer");
            }
            _randomizer = randomizer;

            _tickets = new List<T.Ticket>();
        }

        public T.Ticket GetRandomTicket(string paymentSource)
        {
            var tickets = GetTickets(paymentSource);

            if (tickets.Count() > 0)
            {
                Random r = new Random();
                var index = r.Next(0,tickets.Count()-1);
                //var index = Randomizer.GetNext(0, tickets.Count() - 1);

                return tickets.ElementAt(index);
            }
            else
            {
                return new T.Ticket();
            }
        }

        public IEnumerable<T.Ticket> GetTickets(string paymentSource)
        {
            return Repository.GetAll(paymentSource);
        }
    }
}

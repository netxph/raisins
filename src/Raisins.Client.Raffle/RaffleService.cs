using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Raisins.Client.Raffle
{
    public class RaffleService
    {
        private RaisinsDB _raisinsDb;

        public List<Ticket> Tickets { get; set; }
        public Random Random { get; set; }

        public RaffleService()
        {
            //_raisinsDb = new RaisinsDB();
            Initialize();
        }

        protected virtual void Initialize()
        {
            //todo: why is EF6 here? consider moving it out as a dependency

            //Tickets = _raisinsDb.Tickets.ToList();
            //Random = new Random(this.GetHashCode());
        }
        
        public async Task<Ticket> GetRandomTicket(PaymentClass paymentClass)
        {
            var code = ((int)paymentClass).ToString("00");

            //consider moving this so we don't see entity framework operations!
            //var tickets = Tickets.Where(t => t.TicketCode.StartsWith(code)).ToList();

            var tickets = new List<Ticket>();

            var index = await CallAsyncRandomTicket(0, tickets.Count);

            return tickets[index];
        }

        private Task<int> CallAsyncRandomTicket(int start, int end)
        {
            var randomizer = new Raisins.Client.Randomizer.RandomOrg.RandomOrgIntegerRandomizerService();

            return randomizer.GetNext(start, end, 1);
        }
    }
}

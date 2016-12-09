using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Collections.Generic;

namespace Raisins.Client.Raffle
{
    public class EntityFrameworkRaisinsDataProvider : IRaisinsDataProvider
    {
        private readonly List<Ticket> _tickets;

        protected IEnumerable<Ticket> Tickets
        {
            get
            {
                return _tickets;
            }
        }

        public EntityFrameworkRaisinsDataProvider()
        {
            _tickets = new List<Ticket>();
        }

        public void LoadData()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                _tickets.AddRange(db.Tickets);
            }
        }

        public IEnumerable<Ticket> GetTickets()
        {
            return Tickets;
        }
    }
}

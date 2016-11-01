using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private RaisinsDB _raisinsDb;

        public TicketRepository(RaisinsDB raisinsDb)
        {
            _raisinsDb = raisinsDb;
        }
        public IEnumerable<Ticket> GetAll()
        {
            return _raisinsDb.Tickets;
        }
    }
}
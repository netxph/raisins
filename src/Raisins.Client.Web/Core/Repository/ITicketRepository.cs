using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface ITicketRepository
    {
        IEnumerable<Ticket> GetAll();
    }
}
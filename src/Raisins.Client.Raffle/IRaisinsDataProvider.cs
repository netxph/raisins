using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Raffle
{
    public interface IRaisinsDataProvider
    {
        IEnumerable<Ticket> GetTickets();

        void LoadData();
    }
}

using Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Interfaces
{
    public interface IBeneficiaryForTicketRepository
    {
        Beneficiary GetBeneficiary(string Name);
    }
}

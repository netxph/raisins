using D = Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Interfaces
{
    public interface ITicketService
    {
        D.Tickets GetAll();
        void GenerateTickets(int paymentID, int beneficiaryID, decimal amount, decimal exchangeRate, string name);
        D.Beneficiary GetBeneficiary(string name);
    }
}

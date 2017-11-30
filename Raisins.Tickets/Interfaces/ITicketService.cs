using MODELS = Raisins.Tickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Interfaces
{
    public interface ITicketService
    {
        MODELS.Tickets GetAll();
        void GenerateTickets(string source, int paymentID, int beneficiaryID, decimal amount, MODELS.Currency currency, string name);
        MODELS.Beneficiary GetBeneficiary(string name);
        int CalculateTickets(decimal amount, MODELS.Currency currency);
    }
}

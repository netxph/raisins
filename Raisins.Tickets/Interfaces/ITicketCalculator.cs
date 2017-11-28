using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Interfaces
{
    public interface ITicketCalculator
    {
        int Count(decimal amount, decimal exchangeRate);
        int CalculateSilver(decimal amount);
        int CalculateGold(decimal amount);
        int CalculatePlatinum(decimal amount);
        int CalculateBasic(decimal amount, int ratio);
    }
}

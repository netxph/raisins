using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets
{
    public interface ITicketCalculator
    {
        int Count(decimal amount, decimal exchangeRate);
    }
}

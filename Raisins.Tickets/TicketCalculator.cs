using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets
{
    public class TicketCalculator : ITicketCalculator
    {
        public int Count(decimal amount, decimal exchangeRate)
        {
            int count = (((int)((amount) * exchangeRate) / 2000) * (40 + 15))
                + ((((int)((amount) * exchangeRate) % 2000) / 1000) * (20 + 5))
                + ((((((int)((amount) * exchangeRate) % 2000) % 1000)) / 500) * (20 + 5))
                + (((((((int)((amount) * exchangeRate) % 2000) % 1000)) % 500) / 50) * 1);

            return count;
        }
    }
}

using Raisins.Tickets.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Tickets.Services
{
    public class TicketCalculator : ITicketCalculator
    {
        private const int TICKET_PRICE = 50;

        public int CalculatePlatinum(decimal amount)
        {
            int bonusTickets = 15;
            int price = 2000;

            var baseTickets = price / TICKET_PRICE;
            var tickets = baseTickets + bonusTickets;

            var bulks = (int)amount / price;

            var excess = amount % price;

            return (bulks * tickets) + CalculateGold(excess);
        }

        public int CalculateGold(decimal amount)
        {
            int bonusTickets = 5;
            int price = 1000;

            var baseTickets = price / TICKET_PRICE;
            var tickets = baseTickets + bonusTickets;

            var bulks = (int)amount / price;

            var excess = amount % price;

            return (bulks * tickets) + CalculateSilver(excess);
        }

        public int CalculateSilver(decimal amount)
        {
            int bonusTickets = 2;
            int price = 500;

            var baseTickets = price / TICKET_PRICE;
            var tickets = baseTickets + bonusTickets;

            var bulks = (int)amount / price;

            var excess = amount % price;

            //TODO: Get the actual ratio of PHP Currency
            return (bulks * tickets) + CalculateBasic(excess, TICKET_PRICE);
        }

        public int Count(decimal amount, decimal exchangeRate)
        {
            int count = (((int)((amount) * exchangeRate) / 2000) * (40 + 15))
                + ((((int)((amount) * exchangeRate) % 2000) / 1000) * (20 + 5))
                + ((((((int)((amount) * exchangeRate) % 2000) % 1000)) / 500) * (20 + 5))
                + (((((((int)((amount) * exchangeRate) % 2000) % 1000)) % 500) / 50) * 1);

            return count;
        }

        public int CalculateBasic(decimal amount, int ratio)
        {
            return (int)(amount / ratio);
        }
    }
}

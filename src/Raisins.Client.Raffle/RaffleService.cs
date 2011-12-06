using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raisins.Services.Models;
using Raisins.Services.Data;
using System.Configuration;

namespace Raisins.Client.Raffle
{
    public class RaffleService
    {

        public T[] RandomPermutation<T>(T[] array)
        {
            T[] retArray = new T[array.Length];
            array.CopyTo(retArray, 0);

            Random random = new Random();
            for (int i = 0; i < array.Length; i += 1)
            {
                int swapIndex = random.Next(i, array.Length);
                if (swapIndex != i)
                {
                    T temp = retArray[i];
                    retArray[i] = retArray[swapIndex];
                    retArray[swapIndex] = temp;
                }
            }

            return retArray;
        }

        public Ticket[] RetrieveRandomTickets(PaymentClass paymentClass)
        {
            var classFilter = (PaymentClass)Enum.Parse(typeof(PaymentClass), ConfigurationManager.AppSettings["paymentClass"]);

            RaisinsDB db = new RaisinsDB();
            var results = (from payment in db.Payments.Include("Tickets")
                           from ticket in payment.Tickets
                           where payment.Class == (int)classFilter
                           select ticket).ToArray();

            var tickets = RandomPermutation<Ticket>(results);

            return tickets;
        }

    }
}

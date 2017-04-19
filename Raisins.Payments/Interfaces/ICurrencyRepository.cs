using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Interfaces
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetAll();
        Currency Find(int id);
        Currency GetCurrency(string currencyCode);
        void Add(Currency currency);
        void Edit(Currency currency);
        void Delete(Currency currency);
    }
}

using Raisins.Client.Web.Core.Repository;
using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Raisins.Client.Web.Persistence.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private RaisinsDB _raisinsDb;

        public CurrencyRepository(RaisinsDB raisinsDB)
        {
            _raisinsDb = raisinsDB;
        }

        public IEnumerable<Currency> GetAll()
        {
            return _raisinsDb.Currencies;
        }

        public Currency Find(int id)
        {
            return _raisinsDb.Currencies.Find(id);
        }

        public void Add(Currency currency)
        {
            _raisinsDb.Currencies.Add(currency);
        }

        public void Edit(Currency currency)
        {
            _raisinsDb.Entry(currency).State = EntityState.Modified;
        }

        public void MultipleEdit(IEnumerable<Currency> currencies)
        {
            foreach (Currency currency in currencies)
            {
                _raisinsDb.Entry(currency).State = EntityState.Modified;
            }

        }

        public void Delete(Currency currency)
        {
            _raisinsDb.Currencies.Remove(currency);
        }

        public bool Any(string currencyCode)
        {
            return _raisinsDb.Currencies.Any(c => c.CurrencyCode == currencyCode);
        }
    }
}
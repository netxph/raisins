using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF = Raisins.Data.Models;

namespace Raisins.Data.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        public RaisinsContext _context { get; set; }
        public CurrencyRepository() : this(RaisinsContext.Instance)
        {
        }
        public CurrencyRepository(RaisinsContext context)
        {
            _context = context;
        }
        public IEnumerable<Currency> GetAll()
        {
            return ConvertCurrencyList(_context.Currencies);
        }

        public Currency Find(int id)
        {
            return convertCurrencyToDomain(_context.Currencies.Find(id));
        }

        public Currency GetCurrency(string currencyCode)
        {
            return convertCurrencyToDomain(_context.Currencies
                   .FirstOrDefault(a => a.CurrencyCode == currencyCode));
        }
        public void Add(Currency currency)
        {
            _context.Currencies.Add(convertCurrencyToEF(currency));
        }

        public void Edit(Currency currency)
        {
            _context.Entry(convertCurrencyToEF(currency)).State = EntityState.Modified;
        }

        public void Delete(Currency currency)
        {
            _context.Currencies.Remove(convertCurrencyToEF(currency));
        }

        private Currency convertCurrencyToDomain(EF.Currency efCurrency)
        {
            return new Currency(efCurrency.CurrencyCode, efCurrency.Ratio, efCurrency.ExchangeRate);
        }

        private IEnumerable<Currency> ConvertCurrencyList(IEnumerable<EF.Currency> efCurrencies)
        {
            List<Currency> currencies = new List<Currency>();
            foreach (var efCurrency in efCurrencies)
            {
                currencies.Add(convertCurrencyToDomain(efCurrency));
            }
            return currencies;
        }
        private EF.Currency convertCurrencyToEF(Currency currency)
        {
            return new EF.Currency(currency.CurrencyCode, currency.Ratio, currency.ExchangeRate);
        }
    }
}

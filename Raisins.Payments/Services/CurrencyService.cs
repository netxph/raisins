using Raisins.Payments.Interfaces;
using Raisins.Payments.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Payments.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        protected ICurrencyRepository CurrencyRepository { get { return _currencyRepository; } }
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            if (currencyRepository == null)
            {
                throw new ArgumentNullException("CurrencyService:currencyRepository");
            }

            _currencyRepository = currencyRepository;
        }
        public IEnumerable<Currency> GetCurrencies()
        {
            return CurrencyRepository.GetAll();
        }
    }
}

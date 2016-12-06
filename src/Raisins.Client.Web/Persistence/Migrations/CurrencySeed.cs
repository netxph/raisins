using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;

namespace Raisins.Client.Web.Migrations
{
    public class CurrencySeed : IDbSeeder
    {
        private UnitOfWork _unitOfWork;
        public void Seed(RaisinsDB context)
        {
            _unitOfWork = new UnitOfWork(context);

            AddCurrency("PHP", 1.0M, 50.0M); //1
            AddCurrency("USD", 44.0M, 1.0M); //2
            AddCurrency("AUD", 39.0M, 1.0M); //3
            AddCurrency("GBP", 71.0M, 1.0M); //4
            AddCurrency("HKD", 5.0M, 9.0M);  //5
            AddCurrency("SGD", 34.0M, 2.0M); //6
            AddCurrency("EUR", 56.0M, 1.0M); //7
        }

        private void AddCurrency(
            string currencyCode,
            decimal exchangeRate,
            decimal ratio)
        {

            if (!_unitOfWork.Currencies.Any(currencyCode))
            {
                _unitOfWork.Currencies.Add(new Currency()
                {
                    CurrencyCode = currencyCode,
                    ExchangeRate = exchangeRate,
                    Ratio = ratio
                });
                _unitOfWork.Complete();
            }
        }
    }
}
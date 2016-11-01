using Raisins.Client.Web.Models;
using System.Collections.Generic;

namespace Raisins.Client.Web.Core.Repository
{
    public interface ICurrencyRepository
    {
        IEnumerable<Currency> GetAll();
        Currency Find(int id);
        void Add(Currency currency);
        void Edit(Currency currency);
        void Delete(Currency currency);
    }
}
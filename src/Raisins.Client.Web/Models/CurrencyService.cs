using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class CurrencyService
    {

        protected RaisinsDB DB { get; set; }

        public CurrencyService()
            : this(new RaisinsDB())
        {

        }

        public CurrencyService(RaisinsDB db)
        {
            DB = db;
        }

        public List<Currency> GetAll()
        {
            return DB.Currencies.ToList();
        }

        public Currency Find(int id = 0)
        {
            return DB.Currencies.Find(id);
        }

        public Currency Add(Currency currency)
        {
            DB.Currencies.Add(currency);
            DB.SaveChanges();

            return currency;
        }

        public Currency Edit(Currency currency)
        {
            DB.Entry(currency).State = EntityState.Modified;
            DB.SaveChanges();

            return currency;
        }

        public void Delete(int id)
        {
            var currency = Find(id);

            DB.Currencies.Remove(currency);
            DB.SaveChanges();
        }

    }
}
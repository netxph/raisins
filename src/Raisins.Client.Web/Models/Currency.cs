using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Currency
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public string CurrencyCode { get; set; }

        [Required]
        public decimal Ratio { get; set; }

        [Required]
        public decimal ExchangeRate { get; set; }

        public static List<Currency> GetAll()
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Currencies.ToList();
            }
        }

        public static Currency Find(int id = 0)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                return db.Currencies.Find(id);
            }
        }

        public static Currency Add(Currency currency)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                db.Currencies.Add(currency);
                db.SaveChanges();

                return currency;
            }
        }

        public static Currency Edit(Currency currency)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                db.Entry(currency).State = EntityState.Modified;
                db.SaveChanges();

                return currency;
            }
        }

        public static void Delete(int id)
        {
            using (var db = ObjectProvider.CreateDB())
            {
                var currency = Find(id);

                db.Currencies.Remove(currency);
                db.SaveChanges();
            }
        }


    }
}
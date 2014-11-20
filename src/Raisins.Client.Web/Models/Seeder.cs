using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web.Models
{
    public class Seeder
    {

        public static void Seed(RaisinsDB context)
        {
            using (var db = ObjectProvider.CreateDB())
            {

                if (db.Roles.Count() == 0)
                {
                    db.Roles.Add(new Role() { Name = "Administrator" });
                    db.Roles.Add(new Role() { Name = "Accountant" });
                    db.Roles.Add(new Role() { Name = "Manager" });
                    db.Roles.Add(new Role() { Name = "User" });

                    db.SaveChanges();
                }

                if (db.Activities.Count() == 0)
                {

                    db.Activities.Add(new Activity()
                    {
                        Name = "Payment.Edit",
                        Roles = new List<Role>() 
                        { 
                            db.Roles.First(r => r.Name == "Administrator"),
                            db.Roles.First(r => r.Name == "User")
                        }
                    });


                    db.Activities.Add(new Activity()
                    {
                        Name = "Payment.Lock",
                        Roles = new List<Role>() 
                        { 
                            db.Roles.First(r => r.Name == "Administrator"),
                            db.Roles.First(r => r.Name == "Accountant"),
                        }
                    });

                    db.SaveChanges();
                }

                if (db.Beneficiaries.Count() == 0)
                {

                    db.Beneficiaries.Add(new Beneficiary() { Description = "QaiTS", Name = "QaiTS" });
                    db.Beneficiaries.Add(new Beneficiary() { Description = "MANILEÑOS", Name = "MANILEÑOS" });
                    db.Beneficiaries.Add(new Beneficiary() { Description = "The TimeJumper", Name = "The TimeJumper" });
                    db.Beneficiaries.Add(new Beneficiary() { Description = "Funny Is The New Pogi", Name = "Funny Is The New Pogi" });
                    db.Beneficiaries.Add(new Beneficiary() { Description = "OCSDO Angels", Name = "OCSDO Angels" });
                    db.Beneficiaries.Add(new Beneficiary() { Description = "The Chronicles of Naina", Name = "The Chronicles of Naina" });
                    db.Beneficiaries.Add(new Beneficiary() { Description = "*Group TBA*", Name = "*Group TBA*" });

                    db.SaveChanges();
                }

                //if (db.Executives.Count() == 0)
                //{

                //    db.Executives.Add(new Executive() { Name = "Dave Evans" });
                //    db.Executives.Add(new Executive() { Name = "Chuck Maahs" });
                //    db.Executives.Add(new Executive() { Name = "Kurt Blumberg" });

                //    db.SaveChanges();
                //}

                if (db.Currencies.Count() == 0)
                {
                    db.Currencies.Add(new Currency() { CurrencyCode = "PHP", ExchangeRate = 1, Ratio = 50M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "USD", ExchangeRate = 42, Ratio = 1M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "AUD", ExchangeRate = 43, Ratio = 1M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "SGD", ExchangeRate = 34, Ratio = 2M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "HKD", ExchangeRate = 5, Ratio = 9M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "EUR", ExchangeRate = 53, Ratio = 1M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "GBP", ExchangeRate = 66, Ratio = 1M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "NZD", ExchangeRate = 34, Ratio = 2M });
                    db.Currencies.Add(new Currency() { CurrencyCode = "MYR", ExchangeRate = 13, Ratio = 4M });

                    db.SaveChanges();
                }


                if (!Account.Exists("admin"))
                {
                    //TODO: make this db based.
                    Account.CreateUser("admin", "P@ssw0rd!1",
                        new List<Role>()  
                        { 
                            db.Roles.First(r => r.Name == "Administrator")
                        },
                        new AccountProfile() { Name = "Administrator", IsLocal = false, Currencies = db.Currencies.ToList(), Beneficiaries = db.Beneficiaries.ToList() });
                }


            }
        }

    }
}
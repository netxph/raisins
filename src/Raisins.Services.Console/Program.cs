using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord.Framework.Config;
using Castle.ActiveRecord;

namespace Raisins.Services.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurationSource source = new XmlConfigurationSource("config.xml");

            ActiveRecordStarter.Initialize(source, typeof(Beneficiary), typeof(Payment), typeof(Currency), typeof(Ticket), typeof(Account), typeof(Role), typeof(Setting), typeof(MailLog));

            try
            {
                ActiveRecordStarter.CreateSchema();
                System.Console.WriteLine("Database schema created.");

                DBHelper.Seed();
                System.Console.WriteLine("Database seeded.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error creating database. \r\n" + ex.Message);
            }
        }
    }
}

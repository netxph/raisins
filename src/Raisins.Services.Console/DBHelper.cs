using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Raisins.Services.Console
{
    public class DBHelper
    {

        

        public static void Seed()
        {
            Beneficiary team1 = new Beneficiary()
            {
                Name = "Team1",
                Description = "The Awesome Team",
            };

            team1.Create();

            Beneficiary team2 = new Beneficiary()
            {
                Name = "Team2",
                Description = "The Magnificent Team",
            };

            team2.Create();

            Currency usd = new Currency()
            {
                CurrencyCode = "USD",
                Ratio = 1
            };

            usd.Create();

            Currency php = new Currency()
            {
                CurrencyCode = "PHP",
                Ratio = 50
            };

            php.Create();

            string adminSalt = Account.GetSalt();
            Account admin = new Account()
            {
                UserName = "admin",
                Salt = adminSalt,
                Password = Account.GetHash("P@ssw0rd!1", adminSalt),
            };

            admin.Create();

            string vitalimSalt = Account.GetSalt();
            Account vitalim = new Account()
            {
                UserName = "vitalim",
                Salt = vitalimSalt,
                Password = Account.GetHash("ra151n5", vitalimSalt)
            };

            vitalim.Create();

            Setting vitalimSetting = new Setting()
            {
                Location = "PH",
                Account = vitalim,
                Beneficiary = team1,
                Class = PaymentClass.Internal,
                Currency = php
            };

            vitalimSetting.Create();

            Role vitalimRole = new Role()
            {
                Account = vitalim,
                RoleType = RoleType.User
            };

            vitalimRole.Create();

            string abayonaSalt = Account.GetSalt();
            Account abayona = new Account()
            {
                UserName = "abayona",
                Salt = abayonaSalt,
                Password = Account.GetHash("ra151n5", abayonaSalt)
            };

            abayona.Create();

            Setting abayonaSetting = new Setting()
            {
                Location = "PH",
                Account = abayona,
                Class = PaymentClass.Foreign,
                Currency = usd
            };

            abayonaSetting.Create();

            Role abayonaRole = new Role()
            {
                Account = abayona,
                RoleType = RoleType.User
            };

            abayonaRole.Create();

            string auditSalt = Account.GetSalt();
            Account audit = new Account()
            {
                UserName = "audit",
                Salt = auditSalt,
                Password = Account.GetHash("ra151n5", auditSalt)
            };

            audit.Create();

            Setting auditSetting = new Setting()
            {
                Location = "PH",
                Account = audit,
                Beneficiary = team1,
                Class = PaymentClass.Internal,
                Currency = php
            };

            auditSetting.Create();

            Role auditRole = new Role()
            {
                Account = audit,
                RoleType = RoleType.Auditor
            };

            auditRole.Create();

        }

    }
}

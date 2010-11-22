using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Raisins.Services.Console
{
    public class DBHelper
    {

        public static void Seed()
        {
            Beneficiary reservations = new Beneficiary();
            reservations.Name = "Reservations";
            reservations.Description = "Reservations Team";

            reservations.Create();

            Beneficiary qa = new Beneficiary();
            qa.Name = "QA";
            qa.Description = "QA Team";

            qa.Create();

            Account vitalim = new Account();
            vitalim.UserName = "corp\\vitalim";
            vitalim.Password = "test";

            vitalim.Create();

            Account abayona = new Account();
            abayona.UserName = "corp\\abayona";
            abayona.Password = "test";

            vitalim.Create();

            Setting vitalimSetting = new Setting();
            vitalimSetting.Account = vitalim;
            vitalimSetting.Location = "PHL";
            vitalimSetting.Currency = "PHP";
            vitalimSetting.Beneficiary = reservations;

            vitalimSetting.Create();

            Setting abayonaSetting = new Setting();
            abayonaSetting.Account = abayona;
            abayonaSetting.Location = "PHL";
            abayonaSetting.Currency = "PHP";
            abayonaSetting.Beneficiary = qa;

            abayona.Create();
        }

    }
}

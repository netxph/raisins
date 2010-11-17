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

            Setting vitalim = new Setting();
            vitalim.UserName = "corp\\vitalim";
            vitalim.Location = "PHL";
            vitalim.Currency = "PHP";
            vitalim.Beneficiary = reservations;

            vitalim.Create();

            Setting abayona = new Setting();
            abayona.UserName = "corp\\abayona";
            abayona.Location = "PHL";
            abayona.Currency = "PHP";
            abayona.Beneficiary = qa;

            abayona.Create();
        }

    }
}

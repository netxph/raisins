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
        }

    }
}

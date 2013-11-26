using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Migrations
{
    public class BeneficiarySeed : IDbSeeder
    {
        public void Seed(Models.RaisinsDB context)
        {
            AddBeneficiary(context, "MONSTROU", "Reservations development folks"); //1
            AddBeneficiary(context, "Aaronics", "SUS: Res, WebHosting, ESC"); //2
            AddBeneficiary(context, "aQApella", "QA teams"); //3
            AddBeneficiary(context, "AOPSmith", "AOPS team"); //4
            AddBeneficiary(context, "Banana Gang", "Disney, PMO, SS"); //5
            AddBeneficiary(context, "That's IT", "SUS IT"); //6
        }

        private static void AddBeneficiary(
            Models.RaisinsDB context,
            string name,
            string description)
        {
            if (!context.Beneficiaries.Any(b => b.Name == name))
            {
                Beneficiary.Add(new Beneficiary()
                {
                    Name = name,
                    Description = description
                });
            }
        }
    }
}

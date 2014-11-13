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
            AddBeneficiary(context, "Group 1", "SUS IT, Installs, PMO, PerfMon, Security, BusOps"); //1
            AddBeneficiary(context, "Group 2", "SUS OC, SDO"); //2
            AddBeneficiary(context, "Group 3", "SUS PM Res/Non-Res, Accenture Business"); //3
            AddBeneficiary(context, "Group 4", "Nav QA, Nav TS"); //4
            AddBeneficiary(context, "Group 5", "Nav Product, Taleris"); //5
            AddBeneficiary(context, "Group 6", "Nav Dev"); //6
            AddBeneficiary(context, "Group 7", "Nav SS, Disney, PM"); //7
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

using Raisins.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raisins.Data.Migrations.Seeder.Seeds
{
    public class BeneficiarySeed : IDbSeeder
    {
        public void Seed(RaisinsContext context)
        {
            AddBeneficiary(context, "QaiTS", "  NAV QA/TS "); //1
            AddBeneficiary(context, "MANILEÑOS", "NAV Res Dev"); //2
            AddBeneficiary(context, "The TimeJumpers", "NAV Product/Taleris"); //3
            AddBeneficiary(context, "Funny Is The New Pogi", "NAV SS/PMO/Disney"); //4
            AddBeneficiary(context, "OCSDO Angels", "SUS OC SDO , PMO, SS"); //5
            AddBeneficiary(context, "The Chronicles of Naina", "SUS PM/NonRes/Res/BusinessGroups"); //6
            AddBeneficiary(context, "*Group TBA*", "SUS IT/Installs/PMO/PerfMon/Security/BusOps"); //7
        }

        private static void AddBeneficiary(
            RaisinsContext context,
            string name,
            string description)
        {
            if (!context.Beneficiaries.Any(b => b.Name == name))
            {
                context.Beneficiaries.Add(new Beneficiary()
                {
                    Name = name,
                    Description = description
                });
            }
        }
    }
}

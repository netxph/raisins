using Raisins.Client.Web.Models;
using Raisins.Client.Web.Persistence;
using System.Linq;

namespace Raisins.Client.Web.Migrations
{
    public class BeneficiarySeed : IDbSeeder
    {
        
        public void Seed(RaisinsDB context)
        {
            AddBeneficiary(context, 
                            "R.A. Street Boys", 
                            "Rev Acctg QA, Dev, Product", 
                            "https://www.facebook.com/plugins/video.php?href=https%3A%2F%2Fwww.facebook.com%2F1791739697767849%2Fvideos%2F1795411737400645%2F&show_text=0&width=560"); //1
            AddBeneficiary(context, 
                            "Kingsman", 
                            "NS, Ancillary, TCLoy QA", 
                            "https://www.facebook.com/plugins/video.php?href=https%3A%2F%2Fwww.facebook.com%2F1791739697767849%2Fvideos%2F1795264210748731%2F&show_text=0&width=560"); //2

            AddBeneficiary(context, 
                            "BEY", 
                            "NS, Ancillary, TCLoy Dev", 
                            "https://www.facebook.com/plugins/video.php?href=https%3A%2F%2Fwww.facebook.com%2F1791739697767849%2Fvideos%2F1795364620738690%2F&show_text=0&width=560"); //3

            AddBeneficiary(context, 
                            "Teenage Dream", 
                            "Product. SS. PMO", 
                            "https://www.facebook.com/plugins/post.php?href=https%3A%2F%2Fwww.facebook.com%2Fpermalink.php%3Fstory_fbid%3D1793227410952411%26id%3D1791739697767849%26substory_index%3D0&width=500"); //4
        }

        private static void AddBeneficiary(
            RaisinsDB context,
            string name,
            string description, 
            string videoLink)
        {
            if (!context.Beneficiaries.Any(b => b.Name == name))
            {
                context.Beneficiaries.Add(new Beneficiary()
                {
                    Name = name,
                    Description = description,
                    VideoLink = videoLink
                });
            }
        }
    }
}


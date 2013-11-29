using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Raisins.Client.Web
{
    public class Templates
    {
        public const string EMAIL =
@"
<p style=""font-size: 25px;font-family:'Calibri'"">
<strong>Thank you for voting {0}, {1}!</strong>
</p>
<p style=""font-size: 17px;font-family:'Calibri'"">
<strong>
Ticket Number(s) for your vote(s) is/are:<br /><br />
</strong>
{2}
</p>
<p style=""font-size: 17px;font-family:'Calibri'"">Let's beat our record last year and raise Php 700,000 for scholars of the Food for Hungry Minds.</p>
<p style=""font-size: 17px;font-family:'Calibri'"">Donations in excess of Php 700,000 will go to victims of the Typhoon Yolanda.</p>
<p style=""font-size: 17px;font-family:'Calibri'"">All votes will be part of the raffle draw (1 vote = 1 entry). More votes, more chances of winning.</p>
<p style=""font-size: 17px;font-family:'Calibri'"">Voting period ends on December 05.</p>
<br />
<p style=""font-size: 18px;font-family:'Calibri'""><strong>TALENTS FOR HUNGRY MINDS 2013</strong><br />
<a href=""http://foodforhungryminds.org/"">http://foodforhungryminds.org</a>
</p>
";

    }
}
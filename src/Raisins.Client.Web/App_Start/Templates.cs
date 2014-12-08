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
<p style=""font-size: 17px;font-family:'Calibri'"">Let's beat our record last year and raise more than Php 600,000 for scholars of the Food for Hungry Minds.</p>
<p style=""font-size: 17px;font-family:'Calibri'"">All votes will be part of the raffle draw (1 vote = 1 entry).</p>
<p style=""font-size: 17px;font-family:'Calibri'"">Voting period ends on December 17, 2014.</p>
<br />
<p style=""font-size: 18px;font-family:'Calibri'""><strong>'SUS, NAV-Pasikat' Fundraising 2014</strong><br />
<a href=""http://foodforhungryminds.org/"">http://foodforhungryminds.org</a>
</p>
";

    }
}
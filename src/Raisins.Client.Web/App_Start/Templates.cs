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
<p style=""font-size: 20px"">
<strong>Thank you for voting, {0}!</strong>
</p>
<p>
<strong>
Ticket Number(s) for your vote(s) is/are:<br /><br />
</strong>
{1}
</p>
<p>All proceeds will be used to support High School scholars of the Food for Hungry Minds.</p>
<p>All votes will be part of the raffle draw (1 vote = 1 entry). More votes, more chances of winning.</p>
<p>Voting period ends on December 11.</p>
<br />
<p><strong>JAM FOR HUNGRY MINDS 2012: JAM FOR 600K</strong><br />
<a href=""http://foodforhungryminds.org/"">http://foodforhungryminds.org</a>
</p>
";

    }
}
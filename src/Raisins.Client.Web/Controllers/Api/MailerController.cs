using Raisins.Client.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Client.Web.Controllers.Api
{
    public class MailerController : ApiController
    {

        const int DEFAULT_PAGE_SIZE = 10;

        public List<MailQueue> GetMailers()
        { 
            var db = new RaisinsDB();

            var mails = db.MailQueues.Take(10);
            var items = mails.ToList();

            db.MailQueues.RemoveRange(mails);
            db.SaveChanges();

            return items;
        }
    }
}

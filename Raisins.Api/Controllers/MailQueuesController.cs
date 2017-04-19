using API = Raisins.Api.Models;
using Raisins.Data.Repository;
using Raisins.Notifications.Interfaces;
using Raisins.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Raisins.Notifications.Models;

namespace Raisins.Api.Controllers
{
    public class MailQueuesController : ApiController
    {
        private readonly IMailQueueService _mailQueueService;

        protected IMailQueueService MailQueueService { get { return _mailQueueService; } }

        public MailQueuesController() : this(new MailQueueService(new MailQueueRepository()))
        {
        }

        public MailQueuesController(IMailQueueService mailQueueService)
        {
            if (mailQueueService == null)
            {
                throw new ArgumentNullException("MailQueuesController:mailQueueService");
            }
            _mailQueueService = mailQueueService;
        }
        [HttpPost]
        public HttpResponseMessage AddToMailQueue([FromBody]API.Payment payment)
        {
            MailQueueService.AddToQueue(new MailQueue(payment.PaymentID, payment.Email));
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}

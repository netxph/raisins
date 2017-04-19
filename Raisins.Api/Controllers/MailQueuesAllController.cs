using API = Raisins.Api.Models;
using Raisins.Data.Repository;
using Raisins.Notifications.Interfaces;
using Raisins.Notifications.Models;
using Raisins.Notifications.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class MailQueuesAllController : ApiController
    {
        private readonly IMailQueueService _mailQueueService;

        protected IMailQueueService MailQueueService { get { return _mailQueueService; } }

        public MailQueuesAllController() : this(new MailQueueService(new MailQueueRepository()))
        {
        }

        public MailQueuesAllController(IMailQueueService mailQueueService)
        {
            if (mailQueueService == null)
            {
                throw new ArgumentNullException("MailQueuesController:mailQueueService");
            }
            _mailQueueService = mailQueueService;
        }

        [HttpPost]
        public HttpResponseMessage AddAllToMailQueue([FromBody]IEnumerable<API.Payment> payments)
        {
            foreach (var payment in payments)
            {
                MailQueueService.AddToQueue(new MailQueue(payment.PaymentID, payment.Email));
            }           
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public MailQueues GetMailQueues()
        {
            return MailQueueService.GetAll();
        }
    }
}

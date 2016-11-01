using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Raisins.Client.Web.Controllers.Api
{
    public class MailerController : ApiController
    {

        const int DEFAULT_PAGE_SIZE = 10;

        private IUnitOfWork _unitOfWork;

        public MailerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<MailQueue> GetMailers()
        { 

            List<MailQueue> mails = _unitOfWork.MailQueues.GetAll().Take(10).ToList();
            _unitOfWork.MailQueues.DeleteMultiple(mails);

            _unitOfWork.Complete();
            _unitOfWork.Dispose();

            return mails;
        }
    }
}

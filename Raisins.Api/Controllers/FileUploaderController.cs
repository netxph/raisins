using Raisins.Api.Models;
using Raisins.Beneficiaries.Interfaces;
using Raisins.Beneficiaries.Services;
using Raisins.Data.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Raisins.Api.Controllers
{
    public class FileUploaderController : ApiController
    {
        private readonly IBeneficiaryService _service;
        protected IBeneficiaryService Service { get { return _service; } }

        public FileUploaderController()
            : this(new BeneficiaryService(
                        new BeneficiaryRepository(),
                        new AccountRepository()))
        {
        }

        public FileUploaderController(IBeneficiaryService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("BeneficiariesController:service");
            }
            _service = service;
        }
        [HttpPost]
        public HttpResponseMessage Upload([FromBody]MarkDown file)
        {
            Debug.WriteLine("fileName: " + file.FileName);
            string path = HttpContext.Current.Server.MapPath("~/App_Data/" + file.FileName + ".md");
            File.WriteAllBytes(path, file.Content);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        //tod get specific file
        [HttpGet]
        public HttpResponseMessage GetTestFile(int beneficiaryID)
        {
            string fileName = Service.Get(beneficiaryID).Name;
            HttpResponseMessage result = null;
            var localFilePath = HttpContext.Current.Server.MapPath("~/App_Data/" + fileName +".md");

            if (!File.Exists(localFilePath))
            {
                result = Request.CreateResponse(HttpStatusCode.Gone);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.OK);
                result.Content = new StreamContent(new FileStream(localFilePath, FileMode.Open, FileAccess.Read));
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = "README-Template.md";
            }
            return result;
        }
    }
}

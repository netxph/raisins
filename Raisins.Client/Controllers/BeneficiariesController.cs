using Markdig;
using Raisins.Client.ActionFilters;
using Raisins.Client.Models;
using Raisins.Client.ViewModels;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Controllers
{
    public class BeneficiariesController : Controller
    {
        // GET: Beneficiaries
        public ActionResult Index()
        {
            return View();
        }

        [BasicPermissions("beneficiaries_create")]
        [HttpGet]
        public ActionResult NewBeneficiary()
        {
            return View();
        }

        [BasicPermissions("beneficiaries_create")]
        [HttpPost]
        public ActionResult NewBeneficiary(Beneficiary beneficiary)
        {
            var client = new RestClient("http://localhost:4000/api/beneficiaries");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(beneficiary);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NewBeneficiary", "Beneficiaries");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return RedirectToAction("Index", "Home");
        }

        [BasicPermissions("beneficiaries_view")]
        [HttpGet]
        public ActionResult ViewBeneficiaryList()
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/beneficiariesall");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Beneficiary>>(request);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(response);
            BeneficiaryListViewModel model = new BeneficiaryListViewModel(beneficiaries);
            return View(model);
        }

        [BasicPermissions("beneficiaries_update")]
        [HttpGet]
        public ActionResult EditBeneficiary(int beneficiaryID)
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/beneficiaries");
            var request = new RestRequest(Method.GET);
            request.AddParameter("beneficiaryID", beneficiaryID);
            var response = client.Execute<Payment>(request);
            Beneficiary beneficiary = deserialize.Deserialize<Beneficiary>(response);
            BeneficiaryEditViewModel model = new BeneficiaryEditViewModel(beneficiary);
            return View(model);
        }

        [BasicPermissions("beneficiaries_update")]
        [HttpPost]
        public ActionResult EditBeneficiary(BeneficiaryEditViewModel model)
        {
            Beneficiary beneficiary = new Beneficiary(model.BeneficiaryID, model.Name, model.Description);
            var client = new RestClient("http://localhost:4000/api/Beneficiaries");
            var request = new RestRequest(Method.PUT);
            request.AddJsonBody(beneficiary);
            var response = client.Execute(request);
            return RedirectToAction("ViewBeneficiaryList", "Beneficiaries");
        }

        [BasicPermissions("beneficiaries_update")]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload, string Name)
        {
            var client = new RestClient("http://localhost:4000/api/FileUploader");
            var request = new RestRequest(Method.POST);
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(upload.InputStream))
            {
                fileData = binaryReader.ReadBytes(upload.ContentLength);
            }
            MarkDown markdown = new MarkDown(Name, fileData);
            request.AddJsonBody(markdown);
            var response = client.Execute(request);
            
            return RedirectToAction("ViewBeneficiaryList", "Beneficiaries");
        }

        [HttpGet]
        public ActionResult ViewBeneficiary(int beneficiaryID)
        {
            var client = new RestClient("http://localhost:4000/api/FileUploader");
            var request = new RestRequest(Method.GET);
            request.AddParameter("beneficiaryID", beneficiaryID);
            var response = client.Execute(request);
           
            return View((object)Markdown.ToHtml(response.Content));
        }
    }
}
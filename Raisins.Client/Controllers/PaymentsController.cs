using Excel;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Raisins.Client.ActionFilters;
using Raisins.Client.Models;
using Raisins.Client.Services;
using Raisins.Client.ViewModels;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Raisins.Client.Controllers
{
    public class PaymentsController : Controller
    {
        // GET: Payments
        public ActionResult Index()
        {
            return View();
        }
        [BasicPermissions("payments_view_summary")]
        public ActionResult PaymentSummary()
        {
            List<PaymentSummary> model = (List<PaymentSummary>)TempData["model"];
            return View(model);
        }


        [BasicPermissions("payments_view_summary")]
        [HttpGet]
        public ActionResult GetPaymentSummary()
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/paymentsummaries");
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute<List<PaymentSummary>>(request);
            List<PaymentSummary> list = deserialize.Deserialize<List<PaymentSummary>>(response);
            TempData["model"] = list;
            return RedirectToAction("PaymentSummary", "Payments");
        }

        [BasicPermissions("payments_create_new")]
        [HttpGet]
        public ActionResult NewPayment()
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            PaymentViewModel model = new PaymentViewModel();
            var clientB = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);
            var client = new RestClient("http://localhost:4000/api/currencies");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Currency>>(request);
            List<Currency> currencies = deserialize.Deserialize<List<Currency>>(response);

            client = new RestClient("http://localhost:4000/api/sourceslist");
            request = new RestRequest(Method.GET);
            response = client.Execute<List<Currency>>(request);
            List<PaymentSource> sources = deserialize.Deserialize<List<PaymentSource>>(response);

            client = new RestClient("http://localhost:4000/api/typeslist");
            request = new RestRequest(Method.GET);
            response = client.Execute<List<Currency>>(request);
            List<PaymentType> types = deserialize.Deserialize<List<PaymentType>>(response);
            

            model.InitResources(beneficiaries, currencies, sources, types, DateTime.Today);
            return View(model);
        }

        [PaymentPermission("payments_create_new")]
        [HttpPost]
        public ActionResult NewPayment(PaymentViewModel model)
        {
            Beneficiary beneficiary = new Beneficiary();
            Currency currency = new Currency();
            beneficiary.Name = model.Beneficiary;
            currency.CurrencyCode = model.Currency;
            Payment payment = new Payment(model.Name, model.Amount, currency, beneficiary,
                model.Email, model.CreatedDate, model.PaymentDate, model.CreatedBy, new PaymentSource(model.Source), new PaymentType(model.Type), model.OptOut);

            //TODO: get current user
            var client = new RestClient("http://localhost:4000/api/payments/NewPayment");
            var request = new RestRequest(Method.POST);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(payment, settings);
            request.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("NewPayment", "Payments");
            }
            else
            {
                ModelState.AddModelError("", "Something went wrong");
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult ExportToExcel()
        {            
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();

            //Create a header row
            var headerRow = sheet.CreateRow(0);
            headerRow.CreateCell(0).SetCellValue("Name");
            headerRow.CreateCell(1).SetCellValue("Email");
            headerRow.CreateCell(2).SetCellValue("Amount");
            headerRow.CreateCell(3).SetCellValue("Beneficiary");
            headerRow.CreateCell(4).SetCellValue("Currency");
            headerRow.CreateCell(5).SetCellValue("Type");
            headerRow.CreateCell(6).SetCellValue("Source");
            headerRow.CreateCell(7).SetCellValue("Date");
            headerRow.CreateCell(8).SetCellValue("Opt Out");

            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return File(output.ToArray(),   //The binary data of the XLS file
             "application/vnd.ms-excel",//MIME type of Excel files
             "payments-template.xls");
        }

        [HttpGet]
        public ActionResult ExportToExcelByBeneficiaryAssigned(Account user)
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();
            
            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();

            // get data from user na nakalogin


            // find beneficiary under user

            // loop payments list under the beneficiary of the user


            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/Payments");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<Payment>(request);
            Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content);

            PaymentViewModel model = new PaymentViewModel();
            //Get beneficiaries for list
            var clientB = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);
            //Get currencies for list
            var clientC = new RestClient("http://localhost:4000/api/currencies");
            var requestC = new RestRequest(Method.GET);
            var responseC = clientC.Execute<List<Currency>>(requestC);
            List<Currency> currencies = deserialize.Deserialize<List<Currency>>(responseC);

            var clientP = new RestClient("http://localhost:4000/api/sourceslist");
            var requestP = new RestRequest(Method.GET);
            var responseP = clientP.Execute<List<Currency>>(requestP);
            List<PaymentSource> sources = deserialize.Deserialize<List<PaymentSource>>(responseP);

            var clientT = new RestClient("http://localhost:4000/api/typeslist");
            var requestT = new RestRequest(Method.GET);
            var responseT = clientT.Execute<List<Currency>>(requestT);
            List<PaymentType> types = deserialize.Deserialize<List<PaymentType>>(responseT);


            var a = payment.Beneficiary.Name;

            var b = user.UserName;

            model.InitResources(beneficiaries, currencies, sources, types, payment);

            return View(model);


            ////Create a header row
            //var headerRow = sheet.CreateRow(0);
            //headerRow.CreateCell(0).SetCellValue("Name");
            //headerRow.CreateCell(1).SetCellValue("Email");
            //headerRow.CreateCell(2).SetCellValue("Amount");
            //headerRow.CreateCell(3).SetCellValue("Beneficiary");
            //headerRow.CreateCell(4).SetCellValue("Currency");
            //headerRow.CreateCell(5).SetCellValue("Type");
            //headerRow.CreateCell(6).SetCellValue("Source");
            //headerRow.CreateCell(7).SetCellValue("Date");
            //headerRow.CreateCell(8).SetCellValue("Opt Out");

            ////Write the Workbook to a memory stream
            //MemoryStream output = new MemoryStream();
            //workbook.Write(output);

            //return File(output.ToArray(),   //The binary data of the XLS file
            // "application/vnd.ms-excel",//MIME type of Excel files
            // "payments-template.xls");
        }


        [BasicPermissions("payments_create_new")]
        [HttpGet]
        public ActionResult ImportPayments()
        {
            return View();
        }


        //HttpPostedFileBase 0 content on controller when action filter calling the file is used (happens when excel reads the file)
        //[PaymentUploadPermission("payments_create_new")]
        [BasicPermissions("payments_create_new")]
        [HttpPost]
        public ActionResult ImportPayments(HttpPostedFileBase upload, UploadPaymentViewModel model)
        {
            if (upload != null)
            {
                FileUploader uploader = new FileUploader();
                var payments = uploader.ExcelUpload(upload);
                foreach (var payment in payments)
                {
                    payment.CreatedBy = model.CreatedBy;
                    payment.CreatedDate = model.CreatedDate;
                }


                //send to api
                var client = new RestClient("http://localhost:4000/api/paymentsimport");
                var request = new RestRequest(Method.POST);
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                string body = JsonConvert.SerializeObject(payments, settings);
                request.AddParameter("Application/Json", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("ImportPayments", "Payments");
                }
            }
            else
            {
                TempData["message"] = "Sorry, but you did not upload a file.";
            }
            return RedirectToAction("Index", "Home");
        }

        [PaymentsViewPermission()]
        [HttpGet]
        public ActionResult ViewPaymentList()
        {
            string token = HttpContext.Session["token"].ToString();
            var clientT = new RestClient("http://localhost:4000/api/accounts/Validate");
            var requestT = new RestRequest(Method.GET);
            requestT.AddParameter("encrypted", token);
            var responseT = clientT.Execute<Token>(requestT);
            JsonDeserializer deserialize = new JsonDeserializer();
            Token deserialized = deserialize.Deserialize<Token>(responseT);

            var client = new RestClient("http://localhost:4000/api/paymentslist");
            var request = new RestRequest(Method.GET);
            request.AddParameter("userName", deserialized.User);
            var response = client.Execute<List<Payment>>(request);
            List<Payment> payments = deserialize.Deserialize<List<Payment>>(response);
            PublishAllViewModel model = new PublishAllViewModel(payments);
            return View(model);



        }

        [BasicPermissions("payments_view_list_all")]
        [HttpGet]
        public ActionResult ViewPaymentListAll()
        {
            string token = HttpContext.Session["token"].ToString();
            var clientT = new RestClient("http://localhost:4000/api/accounts/Validate");
            var requestT = new RestRequest(Method.GET);
            requestT.AddParameter("encrypted", token);
            var responseT = clientT.Execute<Token>(requestT);
            JsonDeserializer deserialize = new JsonDeserializer();
            Token deserialized = deserialize.Deserialize<Token>(responseT);

            var client = new RestClient("http://localhost:4000/api/paymentslistall");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Payment>>(request);
            List<Payment> payments = deserialize.Deserialize<List<Payment>>(response);
            PublishAllViewModel model = new PublishAllViewModel(payments);
            return View("ViewPaymentList", model);

        }

        [PaymentPublishPermission("payments_publish")]
        [HttpPost]
        public ActionResult PublishPayment(int paymentID)
        {

            var client = new RestClient("http://localhost:4000/api/payments");
            var request = new RestRequest(Method.GET);
            request.AddParameter("paymentID", paymentID);
            var response = client.Execute<List<Payment>>(request);
            JsonDeserializer deserialize = new JsonDeserializer();
            Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content);
            payment.PublishDate = DateTime.Today;

            var clientP = new RestClient("http://localhost:4000/api/PaymentsPublish");
            var requestP = new RestRequest(Method.PUT);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(payment, settings);
            requestP.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var responseP = clientP.Execute(requestP);

            var clientT = new RestClient("http://localhost:4000/api/Tickets");
            var requestT = new RestRequest(Method.POST);
            requestT.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var responseT = clientT.Execute(requestT);

            var clientM = new RestClient("http://localhost:4000/api/MailQueues");
            var requestM = new RestRequest(Method.POST);
            requestM.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var responseM = clientM.Execute(requestM);

            return Json(Url.Action("ViewPaymentList", "Payments"));

        }

        [PaymentMultiplePermission("payments_publish")]
        [HttpPost]
        public ActionResult PublishAllPayment(List<Payment> payments)
        {
            List<Payment> paymentsPublish = new List<Payment>();
            foreach (var payment in payments)
            {
                if (!payment.Locked)
                {
                    payment.PublishDate = DateTime.Today;
                    paymentsPublish.Add(payment);
                }
            }

            var clientP = new RestClient("http://localhost:4000/api/PaymentsPublishAll");
            var requestP = new RestRequest(Method.PUT);
            requestP.AddJsonBody(paymentsPublish);
            var responseP = clientP.Execute(requestP);

            var clientT = new RestClient("http://localhost:4000/api/TicketsAll");
            var requestT = new RestRequest(Method.POST);
            requestT.AddJsonBody(paymentsPublish);
            var responseT = clientT.Execute(requestT);

            var clientM = new RestClient("http://localhost:4000/api/MailQueuesAll");
            var requestM = new RestRequest(Method.POST);
            requestM.AddJsonBody(paymentsPublish);
            var responseM = clientM.Execute(requestM);

            return Json(Url.Action("ViewPaymentList", "Payments"));
        }

        [HttpGet]
        public ActionResult EditPayment(int paymentID)
        {
            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/Payments");
            var request = new RestRequest(Method.GET);
            request.AddParameter("paymentID", paymentID);
            var response = client.Execute<Payment>(request);
            Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content);

            PaymentViewModel model = new PaymentViewModel();
            //Get beneficiaries for list
            var clientB = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);
            //Get currencies for list
            var clientC = new RestClient("http://localhost:4000/api/currencies");
            var requestC = new RestRequest(Method.GET);
            var responseC = clientC.Execute<List<Currency>>(requestC);
            List<Currency> currencies = deserialize.Deserialize<List<Currency>>(responseC);

            var clientP = new RestClient("http://localhost:4000/api/sourceslist");
            var requestP = new RestRequest(Method.GET);
            var responseP = clientP.Execute<List<Currency>>(requestP);
            List<PaymentSource> sources = deserialize.Deserialize<List<PaymentSource>>(responseP);

            var clientT = new RestClient("http://localhost:4000/api/typeslist");
            var requestT = new RestRequest(Method.GET);
            var responseT = clientT.Execute<List<Currency>>(requestT);
            List<PaymentType> types = deserialize.Deserialize<List<PaymentType>>(responseT);

            model.InitResources(beneficiaries, currencies, sources, types, payment);

            return View(model);
        }

        [EditPermission("payments_create_new")]
        [HttpPost]
        public ActionResult EditPayment(PaymentViewModel model)
        {
            Beneficiary beneficiary = new Beneficiary();
            Currency currency = new Currency();
            beneficiary.Name = model.Beneficiary;
            currency.CurrencyCode = model.Currency;
            Payment payment = new Payment(model.PaymentID, model.Name, model.Amount, currency, beneficiary, model.Email, model.CreatedDate,
                model.ModifiedDate, model.PaymentDate, model.CreatedBy, model.ModifiedBy, new PaymentSource(model.Source), new PaymentType(model.Type), model.OptOut);

            var client = new RestClient("http://localhost:4000/api/Payments");
            var request = new RestRequest(Method.PUT);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(payment, settings);
            request.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return RedirectToAction("ViewPaymentList", "Payments");
        }
    }
}
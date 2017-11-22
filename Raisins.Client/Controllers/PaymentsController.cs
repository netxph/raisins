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
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace Raisins.Client.Controllers
{
    public class PaymentsController : Controller
    {
        const string EMAIL_PATTERN = @"\w+@[a-zA-Z\d]+.[a-zA-Z]+";

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


            model.InitResources(beneficiaries, currencies, sources, types, DateTime.Now);
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

            if (EmailIsValid(model.Email))
            {
                Payment payment = new Payment(model.Name, model.Amount, currency, beneficiary,
                model.Email, model.CreatedDate, model.PaymentDate, model.CreatedBy, new PaymentSource(model.Source), new PaymentType(model.Type), model.OptOut);
                payment.ModifiedBy = model.CreatedBy;

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
            }
            else
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index", "Home");
        }

        protected bool EmailIsValid(string email)
        {
            return Regex.IsMatch(email, EMAIL_PATTERN);
        }

        [HttpGet]
        public ActionResult ExportTemplate()
        {
            //Create new Excel Workbook
            var workbook = new HSSFWorkbook();

            //Create new Excel Sheet
            var sheet = workbook.CreateSheet();

            //Create a header row
            //TODO

            var headerRow = sheet.CreateRow(0);

            SetHeader(headerRow, new string[] { "Donor Name", "Email", "Amount", "Beneficiary", "Currency", "Type", "Source", "Payment Date", "Opt Out" });
            
            //Write the Workbook to a memory stream
            MemoryStream output = new MemoryStream();
            workbook.Write(output);

            return File(output.ToArray(),   //The binary data of the XLS file
             "application/vnd.ms-excel",//MIME type of Excel files
             "payments-template.xls");
        }
        
        //for beneficiary string url
        [HttpGet]
        public ActionResult ExportListByBeneficiary(string beneficiary)
        {
            var response = CallAPI<Payment>("http://localhost:4000/api/paymentslistall/GetPaymentsList");
            var payments = Deserialize<List<Payment>>(response);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                GridView grid = GenerateExcel(beneficiary, payments);

                WriteExcel(beneficiary, grid);

                return RedirectToAction("NewPayment", "Payments");
            }

            return HttpNotFound();
        }

        private GridView GenerateExcel(string beneficiary, List<Payment> payments)
        {
            var grid = new GridView();
            string donorName, beneficiaryName, optOutStatus;

            var source = from data in payments
                         where data.Beneficiary.Name == beneficiary
                         select new
                         {
                             data.PaymentID,
                             donorName = data.Name,
                             data.Email,
                             data.Amount,
                             beneficiaryName = data.Beneficiary.Name,
                             data.Currency.CurrencyCode,
                             data.Type.Type,
                             data.Source.Source,
                             data.PaymentDate,
                             optOutStatus = data.OptOut.ToString()
                         };

            grid.DataSource = source;
            grid.DataBind();

            SetHeader(grid.HeaderRow.Cells, new string[] { "ID", "Donor Name", "Email", "Amount", "Beneficiary", "Currency", "Type", "Source", "Payment Date", "Opt Out" });
            return grid;
        }

        private void WriteExcel(string beneficiary, GridView grid)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

            Response.ClearContent();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + GetFileName(beneficiary));
            Response.Charset = "";

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            grid.RenderControl(htmlTextWriter);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        [HttpGet]
        public ActionResult Export(PublishAllViewModel model)
        {
            var response = CallAPI<Payment>("http://localhost:4000/api/paymentslistall/GetPaymentsList");
            var payments = Deserialize<List<Payment>>(response);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return GenerateExcel(model, payments);
            }

            return HttpNotFound();
        }
        
        protected virtual T Deserialize<T>(IRestResponse response)
        {
            JsonDeserializer deserializer = new JsonDeserializer();
            return Deserialize<T>(response, deserializer);
        }

        protected virtual T Deserialize<T>(IRestResponse response, JsonDeserializer deserializer)
        {
            return deserializer.Deserialize<T>(response);
        }

        protected ActionResult GenerateExcel(PublishAllViewModel model, List<Payment> payments)
        {
            var grid = new GridView();
            string donorName, beneficiaryName, optOutStatus;

            var source = from data in payments
                         where data.Beneficiary.Name == model.SelectedBeneficiary
                         select new
                         {
                             data.PaymentID,
                             donorName = data.Name,
                             data.Email,
                             data.Amount,
                             beneficiaryName = data.Beneficiary.Name,
                             data.Currency.CurrencyCode,
                             data.Type.Type,
                             data.Source.Source,
                             data.PaymentDate,
                             optOutStatus = data.OptOut.ToString()
                         };

            grid.DataSource = source;
            grid.DataBind();

            SetHeader(grid.HeaderRow.Cells, new string[] { "ID", "Donor Name", "Email", "Amount", "Beneficiary", "Currency", "Type", "Source", "Date", "Opt Out"});

            WriteExcel(model, grid);

            return RedirectToAction("NewPayment", "Payments");
        }

        protected virtual void SetHeader(TableCellCollection cells, string[] columnNames)
        {
            for (int i = 0; i < cells.Count; i++)
            {
                cells[i].Text = columnNames[i];
            }
        }

        protected virtual void SetHeader(IRow cells, string[] columnNames)
        {
            for (int i = 0; i < columnNames.Length; i++)
            {
                cells.CreateCell(i).SetCellValue(columnNames[i]);
            }
        }
            protected virtual void WriteExcel(PublishAllViewModel model, GridView grid)
        {
            // error: the file format and extension of don't match excel
            StringWriter sw = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(sw);

            Response.ClearContent();
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + GetFileName(model.SelectedBeneficiary));
            Response.Charset = "";

            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            grid.RenderControl(htmlTextWriter);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        protected virtual string GetFileName(string beneficiary)
        {
            return beneficiary + "_BeneficiaryFile.xls";
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
                    payment.ModifiedBy = model.ModifiedBy;
                    payment.ModifiedDate = model.CreatedDate;
                }

                var client = new RestClient("http://localhost:4000/api/paymentsimport");
                var request = new RestRequest(Method.POST);
                var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                string body = JsonConvert.SerializeObject(payments, settings);
                request.AddParameter("Application/Json", body, ParameterType.RequestBody);
                var response = client.Execute(request);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    TempData["message"] = "Successfully uploaded!";
                    return RedirectToAction("ImportPayments", "Payments");
                    //return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["message"] = "Error! Cannot upload file!";
                    //return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                TempData["message"] = "Sorry, but you did not upload a file.";
            }
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("ImportPayments", "Payments");
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
        public ActionResult ViewPaymentListByBeneficiary(string beneficiarySelected)
        {
            string token = HttpContext.Session["token"].ToString();
            var clientT = new RestClient("http://localhost:4000/api/accounts/Validate");
            var requestT = new RestRequest(Method.GET);
            requestT.AddParameter("encrypted", token);
            var responseT = clientT.Execute<Token>(requestT);
            JsonDeserializer deserialize = new JsonDeserializer();
            Token deserialized = deserialize.Deserialize<Token>(responseT);

            var clientp = new RestClient("http://localhost:4000/api/paymentslistall");
            var requestp = new RestRequest(Method.GET);
            var responsep = clientp.Execute<List<Payment>>(requestp);
            List<Payment> paymentsp = deserialize.Deserialize<List<Payment>>(responsep);
            PublishAllViewModel modelp = new PublishAllViewModel(paymentsp);

            var clientb = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestb = new RestRequest(Method.GET);
            var responseb = clientb.Execute<List<Beneficiary>>(requestb);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseb);
            BeneficiaryListViewModel modelb = new BeneficiaryListViewModel(beneficiaries);

            //var client = new RestClient("http://localhost:4000/api/PaymentsListByBeneficiary");
            var request = new RestRequest(Method.GET);
            if (beneficiarySelected != null)
            {
                request.AddParameter("beneficiarySelected", beneficiarySelected);
            }
            else
            {
                beneficiarySelected = modelp.Payments.DefaultIfEmpty().Select(a => a.Beneficiary.Name).FirstOrDefault();
                request.AddParameter("beneficiarySelected", beneficiarySelected);
            }
            //var response = client.Execute<List<Payment>>(request);
            //List<Payment> payments = deserialize.Deserialize<List<Payment>>(response);

            //PublishAllViewModel model = new PublishAllViewModel(/*payments,*/ beneficiarySelected, paymentsp);
            PublishAllViewModel model = new PublishAllViewModel(paymentsp);
            return View("ViewPaymentList", model);
        }

        public ActionResult ViewPaymentListByBeneficiary3(string beneficiarySelected)
        {
            JsonDeserializer deserialize = new JsonDeserializer();

            var clientp = new RestClient("http://localhost:4000/api/paymentslistall");
            var requestp = new RestRequest(Method.GET);
            var responsep = clientp.Execute<List<Payment>>(requestp);
            List<Payment> paymentsp = deserialize.Deserialize<List<Payment>>(responsep);
            PublishAllViewModel modelp = new PublishAllViewModel(paymentsp);

            var clientb = new RestClient("http://localhost:4000/api/beneficiariesall");
            var requestb = new RestRequest(Method.GET);
            var responseb = clientb.Execute<List<Beneficiary>>(requestb);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseb);
            BeneficiaryListViewModel modelb = new BeneficiaryListViewModel(beneficiaries);

            //var client = new RestClient("http://localhost:4000/api/PaymentsListByBeneficiary");
            var request = new RestRequest(Method.GET);
            PublishAllViewModel model;

            if (beneficiarySelected == "0")
            {
                model = new PublishAllViewModel(paymentsp);
            }
            else
            {
                //beneficiarySelected = modelp.Payments.Select(a => a.Beneficiary.Name).FirstOrDefault();
                model = new PublishAllViewModel(/*payments,*/ beneficiarySelected, paymentsp);
                //request.AddParameter("beneficiarySelected", beneficiarySelected);
            }
            //var response = client.Execute<List<Payment>>(request);
            //List<Payment> payments = deserialize.Deserialize<List<Payment>>(response);
            model.SelectedBeneficiary = beneficiarySelected;
            //ViewBag.PaymentList = model;

            return PartialView("ViewPaymentList", model);
        }

        [PaymentPublishPermission("payments_publish")]
        [HttpGet]
        public ActionResult PublishPayment(int paymentID, string modifiedBy)
        {
            var client = new RestClient("http://localhost:4000/api/payments");
            var request = new RestRequest(Method.GET);
            request.AddParameter("paymentID", paymentID);
            var response = client.Execute<List<Payment>>(request);

            Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content);
            payment.PublishDate = DateTime.Now;
            payment.ModifiedDate = DateTime.Now;
            payment.ModifiedBy = modifiedBy;

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
            
            return RedirectToAction("ViewPaymentList", "Payments");
        }

        [PaymentMultiplePermission("payments_publish")]
        [HttpPost]
        public ActionResult PublishAllPayment(List<Payment> payments, string modifiedBy)
        {
            var clientPA = new RestClient("http://localhost:4000/api/paymentslistall");
            var requestPA = new RestRequest(Method.GET);
            var responsePA = clientPA.Execute<List<Payment>>(requestPA);

            List<Payment> payment = JsonConvert.DeserializeObject<List<Payment>>(responsePA.Content);

            JsonDeserializer deserialize = new JsonDeserializer();
            List<Payment> paymentsList = deserialize.Deserialize<List<Payment>>(responsePA);

            List<Payment> paymentsPublish = BuildPaymentsToPublish(modifiedBy, payments);

            var clientP = new RestClient("http://localhost:4000/api/PaymentsPublishAll");
            var requestP = new RestRequest(Method.PUT);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(paymentsPublish, settings);
            requestP.AddJsonBody(paymentsPublish);
            var responseP = clientP.Execute(requestP);

            var clientT = new RestClient("http://localhost:4000/api/TicketsAll");
            var requestT = new RestRequest(Method.POST);
            requestT.AddParameter("Application/Json", body, ParameterType.RequestBody);
            requestT.AddJsonBody(paymentsPublish);
            var responseT = clientT.Execute<List<Payment>>(requestT);
            //var responseT = clientT.Execute(requestT);

            var clientM = new RestClient("http://localhost:4000/api/MailQueuesAll");
            var requestM = new RestRequest(Method.POST);
            requestM.AddJsonBody(paymentsPublish);
            var responseM = clientM.Execute(requestM);

            return Json(Url.Action("ViewPaymentList", "Payments"));
        }

        private static List<Payment> BuildPaymentsToPublish(string modifiedBy, List<Payment> paymentsList)
        {
            List<Payment> paymentsPublish = new List<Payment>();

            foreach (var payment in paymentsList)
            {
                if (!payment.Locked)
                {
                    payment.PublishDate = DateTime.Now;
                    payment.ModifiedDate = DateTime.Now;
                    payment.ModifiedBy = modifiedBy;

                    paymentsPublish.Add(payment);
                }
            }
            return paymentsPublish;
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

            //var types2 = CallAPI<List<Currency>,List<PaymentType>>(deserialize,"http://localhost:4000/api/typeslist", Method.POST); alternative way

            model.InitResources(beneficiaries, currencies, sources, types, payment);

            return View(model);
        }

        public O CallAPI<I, O>(JsonDeserializer deserializer, string url)
            where I : new()
        {
            return CallAPI<I, O>(deserializer, url, Method.GET);
        }

        public O CallAPI<I, O>(JsonDeserializer deserializer, string url, Method method)
            where I : new()
        {
            var response = CallAPI<I>(url, method);

            return deserializer.Deserialize<O>(response);
        }

        public IRestResponse CallAPI<I>(string url)
            where I : new()
        {
            return CallAPI<I>(url, Method.GET);
        }

        public IRestResponse CallAPI<I>(string url, Method method)
            where I : new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(method);
            return client.Execute<I>(request);
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
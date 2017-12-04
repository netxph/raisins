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
    public class PaymentsController : BaseClientController
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
            var client = new RestClient(AppConfig.GetUrl("paymentsummaries"));
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

            var clientB = new RestClient(AppConfig.GetUrl("beneficiariesall"));
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);

            var client = new RestClient(AppConfig.GetUrl("currencies"));
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Currency>>(request);
            List<Currency> currencies = deserialize.Deserialize<List<Currency>>(response);

            client = new RestClient(AppConfig.GetUrl("sourceslist"));
            request = new RestRequest(Method.GET);
            response = client.Execute<List<Currency>>(request);
            List<PaymentSource> sources = deserialize.Deserialize<List<PaymentSource>>(response);

            client = new RestClient(AppConfig.GetUrl("typeslist"));
            request = new RestRequest(Method.GET);
            response = client.Execute<List<Currency>>(request);
            List<PaymentType> types = deserialize.Deserialize<List<PaymentType>>(response);

            PaymentViewModel model = new PaymentViewModel();

            model.InitResources(beneficiaries, currencies, sources, types, DateTime.Now);
            return View(model);
        }

        [PaymentPermission("payments_create_new")]
        [HttpPost]
        public ActionResult NewPayment(PaymentViewModel model)
        {
            Beneficiary beneficiary = new Beneficiary()
            {
                Name = model.Beneficiary
            };

            Currency currency = new Currency()
            {
                CurrencyCode = model.Currency
            };

            if (EmailIsValid(model.Email))
            {
                Payment payment = new Payment(model.Name, model.Amount, currency, beneficiary,
                model.Email, model.CreatedDate, model.PaymentDate, model.CreatedBy, new PaymentSource(model.Source), new PaymentType(model.Type), model.OptOut);
                payment.Remarks = model.Remarks;
                payment.ModifiedBy = model.CreatedBy;

                //TODO: get current user
                var client = new RestClient(AppConfig.GetUrl("payments/NewPayment"));
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

            SetHeader(headerRow, new string[] { "Donor Name", "Email", "Amount", "Beneficiary", "Currency", "Type", "Source", "Date", "Opt Out", "Remarks" });
            
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
            var payments = CallAPI<Payment, List<Payment>>(AppConfig.GetUrl("paymentslistall/GetPaymentsList"));

            if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
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
                             optOutStatus = data.OptOut.ToString(),
                             data.Remarks
                         };

            grid.DataSource = source;
            grid.DataBind();

            SetHeader(grid.HeaderRow.Cells, new string[] { "ID", "Donor Name", "Email", "Amount", "Beneficiary", "Currency", "Type", "Source", "Payment Date", "Opt Out", "Remarks" });
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
            var payments = CallAPI<Payment, List<Payment>>(AppConfig.GetUrl("paymentslistall/GetPaymentsList"));

            if (RestResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return GenerateExcel(model, payments);
            }

            return HttpNotFound();
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
                             optOutStatus = data.OptOut.ToString(),
                             data.Remarks
                         };

            grid.DataSource = source;
            grid.DataBind();

            SetHeader(grid.HeaderRow.Cells, new string[] { "ID", "Donor Name", "Email", "Amount", "Beneficiary", "Currency", "Type", "Source", "Date", "Opt Out", "Remarks"});

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
            ActionResult result = null;

            try
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

                    var client = new RestClient(AppConfig.GetUrl("paymentsimport"));
                    var request = new RestRequest(Method.POST);
                    var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                    string body = JsonConvert.SerializeObject(payments, settings);
                    request.AddParameter("Application/Json", body, ParameterType.RequestBody);
                    var response = client.Execute(request);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["message"] = "Successfully uploaded!";
                        result = RedirectToAction("ImportPayments", "Payments");
                    }
                    else
                    {
                        TempData["message"] = "Error! Cannot upload file!";
                    }
                }
                else
                {
                    TempData["message"] = "Sorry, but you did not upload a file.";
                }
            }
            catch (Exception e)
            {

            }

            result = RedirectToAction("ImportPayments", "Payments");

            return result;
        }

        [PaymentsViewPermission()]
        [HttpGet]
        public ActionResult ViewPaymentList()
        {
            var clientT = new RestClient(AppConfig.GetUrl("accounts/Validate"));
            var requestT = new RestRequest(Method.GET);
            requestT.AddParameter("encrypted", HttpContext.Session["token"].ToString());
            var responseT = clientT.Execute<Token>(requestT);
            JsonDeserializer deserialize = new JsonDeserializer();
            Token deserialized = deserialize.Deserialize<Token>(responseT);

            var client = new RestClient(AppConfig.GetUrl("paymentslist"));
            var request = new RestRequest(Method.GET);
            request.AddParameter("userName", deserialized.User);
            var response = client.Execute<List<Payment>>(request);
            List<Payment> payments = deserialize.Deserialize<List<Payment>>(response);

            return View(new PublishAllViewModel(payments));
        }

        [BasicPermissions("payments_view_list_all")]
        [HttpGet]
        public ActionResult ViewPaymentListByBeneficiary(string beneficiarySelected)
        {
            JsonDeserializer deserialize = new JsonDeserializer();

            var clientT = new RestClient(AppConfig.GetUrl("accounts/Validate"));
            var requestT = new RestRequest(Method.GET);
            requestT.AddParameter("encrypted", HttpContext.Session["token"].ToString());
            var responseT = clientT.Execute<Token>(requestT);
            Token deserialized = deserialize.Deserialize<Token>(responseT);

            var clientp = new RestClient(AppConfig.GetUrl("paymentslistall"));
            var requestp = new RestRequest(Method.GET);
            var responsep = clientp.Execute<List<Payment>>(requestp);
            List<Payment> payments = deserialize.Deserialize<List<Payment>>(responsep);

            //TODO Decorate this
            if (!deserialized.User.Equals("super", StringComparison.InvariantCultureIgnoreCase))
            {
                FilterPayments(payments, deserialized.User);
            }

            PublishAllViewModel model = new PublishAllViewModel(payments);
            return View("ViewPaymentList", model);
        }

        protected virtual List<Payment> FilterPayments(List<Payment> payments, string username)
        {
            var clientR = new RestClient(AppConfig.GetUrl("accountscreate"));
            var requestR = new RestRequest(Method.GET);
            requestR.AddParameter("username", username);

            var responseR = clientR.Execute<Account>(requestR);
            var account = new JsonDeserializer().Deserialize<Account>(responseR);

            var assignedBeneficiaryNames = account.Profile.Beneficiaries.Select(b => b.Name);

            payments.RemoveAll(payment => !assignedBeneficiaryNames.Any(name => name.Trim().Equals(payment.Beneficiary.Name.Trim(), StringComparison.InvariantCultureIgnoreCase)));

            return payments;
        }

        public ActionResult ViewPaymentListByBeneficiary3(string beneficiarySelected)
        {
            JsonDeserializer deserialize = new JsonDeserializer();

            var clientp = new RestClient(AppConfig.GetUrl("paymentslistall"));
            var requestp = new RestRequest(Method.GET);
            var responsep = clientp.Execute<List<Payment>>(requestp);
            List<Payment> paymentsp = deserialize.Deserialize<List<Payment>>(responsep);
            PublishAllViewModel modelp = new PublishAllViewModel(paymentsp);

            var clientb = new RestClient(AppConfig.GetUrl("beneficiariesall"));
            var requestb = new RestRequest(Method.GET);
            var responseb = clientb.Execute<List<Beneficiary>>(requestb);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseb);
            BeneficiaryListViewModel modelb = new BeneficiaryListViewModel(beneficiaries);

            //var client = new RestClient(AppConfig.GetUrl("PaymentsListByBeneficiary");
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
            var client = new RestClient(AppConfig.GetUrl("payments"));
            var request = new RestRequest(Method.GET);
            request.AddParameter("paymentID", paymentID);
            var response = client.Execute<List<Payment>>(request);

            Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content);
            payment.PublishDate = DateTime.Now;
            payment.ModifiedDate = DateTime.Now;
            payment.ModifiedBy = modifiedBy;

            var clientP = new RestClient(AppConfig.GetUrl("PaymentsPublish"));
            var requestP = new RestRequest(Method.PUT);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(payment, settings);
            requestP.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var responseP = clientP.Execute(requestP);

            var clientT = new RestClient(AppConfig.GetUrl("Tickets"));
            var requestT = new RestRequest(Method.POST);
            requestT.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var responseT = clientT.Execute(requestT);

            var clientM = new RestClient(AppConfig.GetUrl("MailQueues"));
            var requestM = new RestRequest(Method.POST);
            requestM.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var responseM = clientM.Execute(requestM);
            
            return RedirectToAction("ViewPaymentList", "Payments");
        }

        [HttpGet]
        public ActionResult EditPayment(int paymentID)
        {
            JsonDeserializer deserialize = new JsonDeserializer();

            var client = new RestClient(AppConfig.GetUrl("Payments"));
            var request = new RestRequest(Method.GET);
            request.AddParameter("paymentID", paymentID);
            var response = client.Execute<Payment>(request);
            Payment payment = JsonConvert.DeserializeObject<Payment>(response.Content);

            PaymentViewModel model = new PaymentViewModel();
            //Get beneficiaries for list
            var clientB = new RestClient(AppConfig.GetUrl("beneficiariesall"));
            var requestB = new RestRequest(Method.GET);
            var responseB = clientB.Execute<List<Beneficiary>>(requestB);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(responseB);
            //Get currencies for list
            var clientC = new RestClient(AppConfig.GetUrl("currencies"));
            var requestC = new RestRequest(Method.GET);
            var responseC = clientC.Execute<List<Currency>>(requestC);
            List<Currency> currencies = deserialize.Deserialize<List<Currency>>(responseC);

            var clientP = new RestClient(AppConfig.GetUrl("sourceslist"));
            var requestP = new RestRequest(Method.GET);
            var responseP = clientP.Execute<List<Currency>>(requestP);
            List<PaymentSource> sources = deserialize.Deserialize<List<PaymentSource>>(responseP);

            var clientT = new RestClient(AppConfig.GetUrl("typeslist"));
            var requestT = new RestRequest(Method.GET);
            var responseT = clientT.Execute<List<Currency>>(requestT);
            List<PaymentType> types = deserialize.Deserialize<List<PaymentType>>(responseT);

            //var types2 = CallAPI<List<Currency>,List<PaymentType>>(deserialize,AppConfig.GetUrl("typeslist", Method.POST); alternative way

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
            payment.Remarks = model.Remarks;

            var client = new RestClient(AppConfig.GetUrl("Payments"));
            var request = new RestRequest(Method.PUT);
            var settings = new JsonSerializerSettings() { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
            string body = JsonConvert.SerializeObject(payment, settings);
            request.AddParameter("Application/Json", body, ParameterType.RequestBody);
            var response = client.Execute(request);
            return RedirectToAction("ViewPaymentList", "Payments");
        }
    }
}
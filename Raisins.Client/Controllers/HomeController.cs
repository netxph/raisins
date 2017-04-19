using Raisins.Client.Models;
using Raisins.Client.ViewModels;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Raisins.Client.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.MyString = (string)TempData["message"];

            JsonDeserializer deserialize = new JsonDeserializer();
            var client = new RestClient("http://localhost:4000/api/beneficiariesall");
            var request = new RestRequest(Method.GET);
            var response = client.Execute<List<Beneficiary>>(request);
            List<Beneficiary> beneficiaries = deserialize.Deserialize<List<Beneficiary>>(response);
           

            
            var clientG = new RestClient("http://localhost:4000/api/goal");
            var requestG = new RestRequest(Method.GET);
            var responseG = clientG.Execute<decimal>(requestG);
            decimal total = deserialize.Deserialize<decimal>(responseG);
            HomeViewModel model = new HomeViewModel(beneficiaries, total);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
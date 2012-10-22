using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raisins.Client.Web.Models;

namespace Raisins.Client.Web.Controllers
{
    public class CurrenciesController : Controller
    {
        public CurrenciesController()
        {
            Service = new CurrencyService();
        }

        protected CurrencyService Service { get; set; }

        //
        // GET: /Currencies/

        public ActionResult Index()
        {
            return View(Service.GetAll());
        }

        //
        // GET: /Currencies/Details/5

        public ActionResult Details(int id = 0)
        {
            Currency currency = Service.Find(id);

            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        //
        // GET: /Currencies/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Currencies/Create

        [HttpPost]
        public ActionResult Create(Currency currency)
        {
            if (ModelState.IsValid)
            {
                Service.Add(currency);
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        //
        // GET: /Currencies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Currency currency = Service.Find(id);

            if (currency == null)
            {
                return HttpNotFound();
            }

            return View(currency);
        }

        //
        // POST: /Currencies/Edit/5

        [HttpPost]
        public ActionResult Edit(Currency currency)
        {
            if (ModelState.IsValid)
            {
                Service.Edit(currency);

                return RedirectToAction("Index");
            }
            return View(currency);
        }

        //
        // GET: /Currencies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Currency currency = Service.Find(id);
            if (currency == null)
            {
                return HttpNotFound();
            }
            return View(currency);
        }

        //
        // POST: /Currencies/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Service.Delete(id);

            return RedirectToAction("Index");
        }
        
    }
}
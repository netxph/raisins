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
    public class BeneficiariesController : Controller
    {

        public BeneficiariesController()
        {
            Service = new BeneficiaryService();
        }

        protected BeneficiaryService Service { get; set; }

        //
        // GET: /Beneficiaries/

        public ActionResult Index()
        {
            return View(Service.GetAll());
        }

        //
        // GET: /Beneficiaries/Details/5

        public ActionResult Details(int id = 0)
        {
            Beneficiary beneficiary = Service.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        //
        // GET: /Beneficiaries/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Beneficiaries/Create

        [HttpPost]
        public ActionResult Create(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                Service.Add(beneficiary);
                return RedirectToAction("Index");
            }

            return View(beneficiary);
        }

        //
        // GET: /Beneficiaries/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Beneficiary beneficiary = Service.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        //
        // POST: /Beneficiaries/Edit/5

        [HttpPost]
        public ActionResult Edit(Beneficiary beneficiary)
        {
            if (ModelState.IsValid)
            {
                Service.Edit(beneficiary);
                return RedirectToAction("Index");
            }
            return View(beneficiary);
        }

        //
        // GET: /Beneficiaries/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Beneficiary beneficiary = Service.Find(id);
            if (beneficiary == null)
            {
                return HttpNotFound();
            }
            return View(beneficiary);
        }

        //
        // POST: /Beneficiaries/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Service.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
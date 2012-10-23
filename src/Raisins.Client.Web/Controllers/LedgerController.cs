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
    public class LedgerController : Controller
    {
        private RaisinsDB db = new RaisinsDB();

        //
        // GET: /Ledger/

        public ActionResult Index()
        {
            return View(db.Ledger.ToList());
        }

        //
        // GET: /Ledger/Details/5

        public ActionResult Details(int id = 0)
        {
            Ledger ledger = db.Ledger.Find(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        //
        // GET: /Ledger/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ledger/Create

        [HttpPost]
        public ActionResult Create(Ledger ledger)
        {
            if (ModelState.IsValid)
            {
                db.Ledger.Add(ledger);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ledger);
        }

        //
        // GET: /Ledger/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ledger ledger = db.Ledger.Find(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        //
        // POST: /Ledger/Edit/5

        [HttpPost]
        public ActionResult Edit(Ledger ledger)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ledger).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ledger);
        }

        //
        // GET: /Ledger/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ledger ledger = db.Ledger.Find(id);
            if (ledger == null)
            {
                return HttpNotFound();
            }
            return View(ledger);
        }

        //
        // POST: /Ledger/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ledger ledger = db.Ledger.Find(id);
            db.Ledger.Remove(ledger);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
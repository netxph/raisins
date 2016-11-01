using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using System.Web.Mvc;

namespace Raisins.Client.Web.Controllers
{
    public class CurrenciesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CurrenciesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Currencies/

        public ActionResult Index()
        {
            return View(_unitOfWork.Currencies.GetAll());
        }

        //
        // GET: /Currencies/Details/5

        public ActionResult Details(int id = 0)
        {
            Currency currency = _unitOfWork.Currencies.Find(id);

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
                _unitOfWork.Currencies.Add(currency);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(currency);
        }

        //
        // GET: /Currencies/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Currency currency = _unitOfWork.Currencies.Find(id);

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
                _unitOfWork.Currencies.Edit(currency);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(currency);
        }

        //
        // GET: /Currencies/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Currency currency = _unitOfWork.Currencies.Find(id);
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
            Currency currency = _unitOfWork.Currencies.Find(id);
            _unitOfWork.Currencies.Delete(currency);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
        
    }
}
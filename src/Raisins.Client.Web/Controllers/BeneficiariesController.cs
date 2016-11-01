using Raisins.Client.Web.Core;
using Raisins.Client.Web.Models;
using System.Web.Mvc;

namespace Raisins.Client.Web.Controllers
{
    [Authorize]
    public class BeneficiariesController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public BeneficiariesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //
        // GET: /Beneficiaries/

        public ActionResult Index()
        {
            return View(_unitOfWork.Beneficiaries.GetAll());
        }

        //
        // GET: /Beneficiaries/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id = 0)
        {
            Beneficiary beneficiary = _unitOfWork.Beneficiaries.Find(id);
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
                _unitOfWork.Beneficiaries.Add(beneficiary);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }

            return View(beneficiary);
        }

        //
        // GET: /Beneficiaries/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Beneficiary beneficiary = _unitOfWork.Beneficiaries.Find(id);
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
                _unitOfWork.Beneficiaries.Edit(beneficiary);
                _unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            return View(beneficiary);
        }

        //
        // GET: /Beneficiaries/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Beneficiary beneficiary = _unitOfWork.Beneficiaries.Find(id);
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
            _unitOfWork.Beneficiaries.Delete(id);
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }

    }
}
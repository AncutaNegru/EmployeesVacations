using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Repositories;
using EmployeesVacations.Models;

namespace EmployeesVacations.Controllers
{
    public class BusinessUnitController : Controller
    {
        private BusinessUnitRepository businessUnitRepository = new BusinessUnitRepository();
        // GET: BusinessUnit
        public ActionResult Index()
        {
            List<BusinessUnitModel> allBusinessUnits = businessUnitRepository.GetAllBusinessUnits();
            return View("Index", allBusinessUnits);
        }

        // GET: BusinessUnit/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BusinessUnit/Create
        public ActionResult Create()
        {
            return View("CreateBusinessUnit");
        }

        // POST: BusinessUnit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                BusinessUnitModel businessUnitModel = new BusinessUnitModel();
                UpdateModel(businessUnitModel);
                businessUnitRepository.InsertBusinessUnit(businessUnitModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateBusinessUnit");
            }
        }

        // GET: BusinessUnit/Edit/5
        public ActionResult Edit(Guid id)
        {
            BusinessUnitModel businessUnitModel = businessUnitRepository.GetBusinessUnitByID(id);
            return View("EditBusinessUnit", businessUnitModel);
        }

        // POST: BusinessUnit/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                BusinessUnitModel businessUnitModel = new BusinessUnitModel();
                UpdateModel(businessUnitModel);
                businessUnitRepository.UpdateBusinessUnit(businessUnitModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditBusinessUnit");
            }
        }

        // GET: BusinessUnit/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BusinessUnit/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

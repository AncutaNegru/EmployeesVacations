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
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        // GET: BusinessUnit
        public ActionResult Index()
        {
            List<BusinessUnitModel> allBusinessUnits = businessUnitRepository.GetAllBusinessUnits();
            return View("Index", allBusinessUnits);
        }

        // GET: BusinessUnit/Details/5
        public ActionResult Details(Guid id)
        {
            BusinessUnitModel businessUnitDetails = new BusinessUnitModel();
            businessUnitDetails = businessUnitRepository.GetBusinessUnitByID(id);
            return View("DetailsBusinessUnit", businessUnitDetails);
        }

        // GET: BusinessUnit/Create
        public ActionResult Create()
        {
            var managers = employeeRepository.GetAllEmployeesWherePositionIsBusinessUnitManager();
            SelectList listmanagers = new SelectList(managers, "IDEmployee", "FullName");
            ViewData["manager"] = listmanagers;
            return View("CreateBusinessUnit");
        }

        // POST: BusinessUnit/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var managers = employeeRepository.GetAllEmployeesWherePositionIsBusinessUnitManager();
            SelectList listmanagers = new SelectList(managers, "IDEmployee", "FullName");
            ViewData["manager"] = listmanagers;
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
        public ActionResult Delete(Guid id)
        {
            BusinessUnitModel businessUnitToDelete = businessUnitRepository.GetBusinessUnitByID(id);
            return View("DeleteBusinessUnit", businessUnitToDelete);
        }

        // POST: BusinessUnit/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                businessUnitRepository.DeleteBusinessUnit(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteBusinessUnit");
            }
        }
    }
}

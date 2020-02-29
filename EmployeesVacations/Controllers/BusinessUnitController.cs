using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Repositories;
using EmployeesVacations.Models;

namespace EmployeesVacations.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BusinessUnitController : Controller
    {
        private BusinessUnitRepository businessUnitRepository = new BusinessUnitRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private TeamRepository teamRepository = new TeamRepository();

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
            var managers = employeeRepository.GetAllEmployeesWherePositionIsBusinessUnitManager();
            SelectList listmanagers = new SelectList(managers, "IDEmployee", "FullName");
            ViewData["manager"] = listmanagers;
            return View("EditBusinessUnit", businessUnitModel);
        }

        // POST: BusinessUnit/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            var managers = employeeRepository.GetAllEmployeesWherePositionIsBusinessUnitManager();
            SelectList listmanagers = new SelectList(managers, "IDEmployee", "FullName");
            ViewData["manager"] = listmanagers;
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
                List<TeamModel> teamsInBusinessUnit = teamRepository.GetAllTeamsByBusinessUnitID(id);
                List<EmployeeModel> employeesInBusinessUnit = employeeRepository.GetAllEmployeesByBusinessUnitId(id);
                bool checkTeams = !teamsInBusinessUnit.Any();
                bool checkEmployees = !employeesInBusinessUnit.Any();
                if (checkTeams && checkEmployees)
                {
                    businessUnitRepository.DeleteBusinessUnit(id);
                    return RedirectToAction("Index");
                } 
                return RedirectToAction("AlertTeamsAndEmployeesExist");
            }
            catch
            {
                return View("DeleteBusinessUnit");
            }
        }

        public ActionResult AlertTeamsAndEmployeesExist()
        {
            TempData["alertMessage"] = "Please delete all teams and employees in the business unit or reassign them to a different business unit (both teams and employees) before deleting the entire business unit!";
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Models;
using EmployeesVacations.Repositories;

namespace EmployeesVacations.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private BusinessUnitRepository businessUnitRepository = new BusinessUnitRepository();
        private TeamRepository teamRepository = new TeamRepository();
        // GET: Employee
        public ActionResult Index()
        {
            List<EmployeeModel> allEmployees = employeeRepository.GetAllEmployees();
            return View("Index", allEmployees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(Guid id)
        {
            EmployeeModel employeeModel = employeeRepository.GetEmployeeByID(id);
            return View("DetailsEmployee", employeeModel);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            var businessUnits = businessUnitRepository.GetAllBusinessUnits();
            SelectList listBusinessUnits = new SelectList(businessUnits, "IDBusinessUnit", "Name");
            ViewData["businessUnit"] = listBusinessUnits;
            var teams = new List<TeamModel>();
            SelectList listTeams = new SelectList(teams, "IDTeam", "Name");
            ViewData["team"] = listTeams;
            return View("CreateEmployee");
        }

        // GET: Employee/FetchTeamsByBusinessUnitSelected
        [HttpGet]
        public JsonResult FetchTeamsByBusinessUnitSelected(Guid id)
        {
            var teams = teamRepository.GetAllTeamsByBusinessUnitID(id);
            return Json(teams, JsonRequestBehavior.AllowGet);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var businessUnits = businessUnitRepository.GetAllBusinessUnits();
            SelectList listBusinessUnits = new SelectList(businessUnits, "IDBusinessUnit", "Name");
            ViewData["businessUnit"] = listBusinessUnits;
            var teams = new List<TeamModel>();
            SelectList listTeams = new SelectList(teams, "IDTeam", "Name");
            ViewData["team"] = listTeams;
            try
            {
                EmployeeModel employeeModel = new EmployeeModel();
                UpdateModel(employeeModel);
                employeeRepository.InsertEmployee(employeeModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateEmployee");
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(Guid id)
        {
            EmployeeModel employeeModel = employeeRepository.GetEmployeeByID(id);
            var businessUnits = businessUnitRepository.GetAllBusinessUnits();
            SelectList listBusinessUnits = new SelectList(businessUnits, "IDBusinessUnit", "Name");
            ViewData["businessUnit"] = listBusinessUnits;
            var teams = teamRepository.GetAllTeamsByBusinessUnitID(employeeModel.IDBusinessUnit);
            SelectList listTeams = new SelectList(teams, "IDTeam", "Name");
            ViewData["team"] = listTeams;
            return View("EditEmployee", employeeModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                var businessUnits = businessUnitRepository.GetAllBusinessUnits();
                SelectList listBusinessUnits = new SelectList(businessUnits, "IDBusinessUnit", "Name");
                ViewData["businessUnit"] = listBusinessUnits;
                var teams = new List<TeamModel>();
                SelectList listTeams = new SelectList(teams, "IDTeam", "Name");
                ViewData["team"] = listTeams;
                EmployeeModel employeeModel = new EmployeeModel();
                UpdateModel(employeeModel);
                employeeRepository.UpdateEmployee(employeeModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditEmployee");
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(Guid id)
        {
            EmployeeModel employeeModel = employeeRepository.GetEmployeeByID(id);
            return View("DeleteEmployee", employeeModel);
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                employeeRepository.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteEmployee");
            }
        }
    }
}

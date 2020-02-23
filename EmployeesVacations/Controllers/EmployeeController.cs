using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Models;
using EmployeesVacations.Repositories;
using Microsoft.AspNet.Identity.Owin;

namespace EmployeesVacations.Controllers
{
    public class EmployeeController : Controller
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private BusinessUnitRepository businessUnitRepository = new BusinessUnitRepository();
        private TeamRepository teamRepository = new TeamRepository();
        private VacationRequestRepository vacationRequestRepository = new VacationRequestRepository();

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
            var x = Json(teams, JsonRequestBehavior.AllowGet);
            return Json(teams, JsonRequestBehavior.AllowGet);
        }

        // POST: Employee/Create
        [HttpPost]
        public async System.Threading.Tasks.Task<ActionResult> Create(FormCollection collection)
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

                var user = new ApplicationUser { UserName = employeeModel.FirstName + "." + employeeModel.LastName + "@company.com", Email = employeeModel.FirstName + "." + employeeModel.LastName + "@company.com" };
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var result = await userManager.CreateAsync(user, "Pa$5word");
                employeeModel.IDUser = user.Id;
                if(result.Succeeded)
                {
                    if(employeeModel.Position == PositionEnum.TeamLead)
                    {
                        await userManager.AddToRoleAsync(user.Id, "Lead");
                    }
                    else if(employeeModel.Position == PositionEnum.BusinessUnitManager)
                    {
                        await userManager.AddToRoleAsync(user.Id, "Manager");
                    }
                    else await userManager.AddToRoleAsync(user.Id, "Employee");
                }
                
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
                List<VacationRequestModel> allVacationsByEmployeeId = vacationRequestRepository.GetAllVacationRequestsByEmployeeId(id);
                foreach(VacationRequestModel request in allVacationsByEmployeeId)
                {
                    vacationRequestRepository.DeleteVacationRequest(request.IDVacationRequest);
                }
                if(employeeRepository.IsTeamLead(id) == true)
                {
                    teamRepository.UpdateTeamsIfTeamLeadDeleted(id);
                }
                if (employeeRepository.IsBusinessUnitManager(id) == true)
                {
                    businessUnitRepository.UpdateBusinessUnitsIfManagerIsDeleted(id);
                }
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

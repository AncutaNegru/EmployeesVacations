using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Models;
using EmployeesVacations.Repositories;
using Microsoft.AspNet.Identity;

namespace EmployeesVacations.Controllers
{
    public class VacationRequestController : Controller
    {
        private VacationRequestRepository vacationRequestRepository = new VacationRequestRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private TeamRepository teamRepository = new TeamRepository();
        private BusinessUnitRepository businessUnitRepository = new BusinessUnitRepository();

        // GET: VacationRequest
        public ActionResult Index()
        {
            string idLoggedInUser = User.Identity.GetUserId();
            EmployeeModel employee = employeeRepository.GetEmployeeByUserId(idLoggedInUser);
            List<VacationRequestModel> allVacationRequests;
            if (employee == null)
            {
                allVacationRequests = vacationRequestRepository.GetAllVacationRequests();
                return View("Index", allVacationRequests);
            }
            if (employee.Position == PositionEnum.TeamLead)
            {
                allVacationRequests = new List<VacationRequestModel>();
                List<TeamModel> teamsLeadByCurrentEmployee = teamRepository.GetAllTeamsByTeamLeadID(employee.IDEmployee);
                foreach (TeamModel team in teamsLeadByCurrentEmployee)
                {
                    List<VacationRequestModel> vacationsInTeam = vacationRequestRepository.GetAllVacationRequestsByTeamId(team.IDTeam);
                    foreach (VacationRequestModel vacation in vacationsInTeam)
                    {
                        allVacationRequests.Add(vacation);
                    }
                }
                return View("Index", allVacationRequests);
            }
            if(employee.Position == PositionEnum.BusinessUnitManager)
            {
                allVacationRequests = new List<VacationRequestModel>();
                List<BusinessUnitModel> businessManagedByCurrentEmployee = businessUnitRepository.GetBusinessUnitsByManagerId(employee.IDEmployee);
                foreach(BusinessUnitModel business in businessManagedByCurrentEmployee)
                {
                    List<VacationRequestModel> vacationsInBusinessUnit = vacationRequestRepository.GetAllVacationRequestsByBusinessUnitId(business.IDBusinessUnit);
                    foreach(VacationRequestModel vacation in vacationsInBusinessUnit)
                    {
                        allVacationRequests.Add(vacation);
                    }
                }
                return View("Index", allVacationRequests);
            }
            allVacationRequests = vacationRequestRepository.GetAllVacationRequestsByEmployeeId(employee.IDEmployee);
            return View("Index", allVacationRequests);
        }

        // GET: VacationRequest/Details/5
        public ActionResult Details(Guid id)
        {
            VacationRequestModel vacationRequestModel = vacationRequestRepository.GetVacationRequestById(id);
            return View("DetailsVacationRequest", vacationRequestModel);
        }

        [Authorize(Roles = "Manager, Lead, Employee")]
        // GET: VacationRequest/Create
        public ActionResult Create()
        {
            EmployeeModel current = employeeRepository.GetEmployeeByUserId(User.Identity.GetUserId());
            ViewData["idEmployee"] = current.IDEmployee;
            return View("CreateVacationRequest");
        }

        [Authorize(Roles = "Manager, Lead, Employee")]
        // POST: VacationRequest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            EmployeeModel current = employeeRepository.GetEmployeeByUserId(User.Identity.GetUserId());
            ViewData["idEmployee"] = current.IDEmployee;
            try
            {
                VacationRequestModel vacationRequestModel = new VacationRequestModel();
                UpdateModel(vacationRequestModel);
                vacationRequestRepository.InsertVacationRequest(vacationRequestModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("CreateVacationRequest");
            }
        }

        [Authorize(Roles = "Manager, Lead")]
        // GET: VacationRequest/Edit/5
        public ActionResult Edit(Guid id)
        {
            VacationRequestModel vacationRequestModel = vacationRequestRepository.GetVacationRequestById(id);
            return View("EditVacationRequest", vacationRequestModel);
        }

        [Authorize(Roles = "Manager, Lead")]
        // POST: VacationRequest/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
                VacationRequestModel vacationRequestModel = new VacationRequestModel();
                UpdateModel(vacationRequestModel);
                vacationRequestRepository.UpdateVacationRequest(vacationRequestModel);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("EditVacationRequest");
            }
        }

        [Authorize(Roles = "Admin, Manager, Lead")]
        // GET: VacationRequest/Delete/5
        public ActionResult Delete(Guid id)
        {
            VacationRequestModel vacationRequestModel = vacationRequestRepository.GetVacationRequestById(id);
            return View("DeleteVacationRequest", vacationRequestModel);
        }

        [Authorize(Roles = "Admin, Manager, Lead")]
        // POST: VacationRequest/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                vacationRequestRepository.DeleteVacationRequest(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("DeleteVacationrequest");
            }
        }
    }
}

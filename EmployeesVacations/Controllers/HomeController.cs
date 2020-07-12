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
    public class HomeController : Controller
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private VacationRequestRepository vacationRequestRepository = new VacationRequestRepository();
        private TeamRepository teamRepository = new TeamRepository();
        private BusinessUnitRepository businessUnitRepository = new BusinessUnitRepository();
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    ViewData["showRequests"] = false;
                    return View();
                }
                else
                {
                    string idLoggedInUser = User.Identity.GetUserId();
                    EmployeeModel employee = employeeRepository.GetEmployeeByUserId(idLoggedInUser);
                    List<VacationRequestModel> allVacationRequests;
                    allVacationRequests = new List<VacationRequestModel>();

                    if (employee.Position == PositionEnum.TeamLead)
                    {
                        List<TeamModel> teamsLeadByCurrentEmployee = teamRepository.GetAllTeamsByTeamLeadID(employee.IDEmployee);
                        foreach (TeamModel team in teamsLeadByCurrentEmployee)
                        {
                            List<VacationRequestModel> vacationsInTeam = vacationRequestRepository.GetAllVacationRequestsByTeamIdFirstStatus(team.IDTeam);
                            foreach (VacationRequestModel vacation in vacationsInTeam)
                            {
                                allVacationRequests.Add(vacation);
                            }
                        }
                    }

                    if (employee.Position == PositionEnum.BusinessUnitManager)
                    {
                        List<BusinessUnitModel> businessManagedByCurrentEmployee = businessUnitRepository.GetBusinessUnitsByManagerId(employee.IDEmployee);
                        foreach (BusinessUnitModel business in businessManagedByCurrentEmployee)
                        {
                            List<VacationRequestModel> vacationsInBusinessUnit = vacationRequestRepository.GetAllVacationRequestsByBusinessUnitIdSecondStatus(business.IDBusinessUnit);
                            foreach (VacationRequestModel vacation in vacationsInBusinessUnit)
                            {
                                allVacationRequests.Add(vacation);
                            }
                        }
                    }

                    if (allVacationRequests.Count == 0)
                    {
                        ViewData["showRequests"] = false;
                    }
                    else
                    {
                        ViewData["showRequests"] = true;
                    }
                    return View();
                }
            }
            ViewData["showRequests"] = false;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
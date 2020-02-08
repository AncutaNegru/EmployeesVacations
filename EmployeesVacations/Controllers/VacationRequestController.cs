using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeesVacations.Models;
using EmployeesVacations.Repositories;

namespace EmployeesVacations.Controllers
{
    public class VacationRequestController : Controller
    {
        private VacationRequestRepository vacationRequestRepository = new VacationRequestRepository();
        // GET: VacationRequest
        public ActionResult Index()
        {
            List<VacationRequestModel> allVacationRequests = vacationRequestRepository.GetAllVacationRequests();
            return View("Index", allVacationRequests);
        }

        // GET: VacationRequest/Details/5
        public ActionResult Details(Guid id)
        {
            VacationRequestModel vacationRequestModel = vacationRequestRepository.GetVacationRequestById(id);
            return View("DetailsVacationRequest", vacationRequestModel);
        }

        // GET: VacationRequest/Create
        public ActionResult Create()
        {
            return View("CreateVacationRequest");
        }

        // POST: VacationRequest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
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

        // GET: VacationRequest/Edit/5
        public ActionResult Edit(Guid id)
        {
            VacationRequestModel vacationRequestModel = vacationRequestRepository.GetVacationRequestById(id);
            return View("EditVacationRequest", vacationRequestModel);
        }

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

        // GET: VacationRequest/Delete/5
        public ActionResult Delete(Guid id)
        {
            VacationRequestModel vacationRequestModel = vacationRequestRepository.GetVacationRequestById(id);
            return View("DeleteVacationRequest", vacationRequestModel);
        }

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

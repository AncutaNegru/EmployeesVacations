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
            return View("CreateEmployee");
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
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
            return View("EditEmployee", employeeModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, FormCollection collection)
        {
            try
            {
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

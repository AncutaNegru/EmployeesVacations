﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesVacations.Models.DBObjects;
using EmployeesVacations.Models;

namespace EmployeesVacations.Repositories
{
    public class EmployeeRepository
    {
        private EmployeesAndVacationsModelsDataContext dbContext;
        public EmployeeRepository()
        {
            this.dbContext = new EmployeesAndVacationsModelsDataContext();
        }
        public EmployeeRepository(EmployeesAndVacationsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private EmployeeModel MapDbObjectToModel(Employee dbEmployee)
        {
            EmployeeModel employeeModel = new EmployeeModel();
            if (dbEmployee != null)
            {
                employeeModel.IDEmployee = dbEmployee.IDEmployee;
                employeeModel.FirstName = dbEmployee.FirstName;
                employeeModel.LastName = dbEmployee.LastName;
                employeeModel.Position = (PositionEnum)dbEmployee.Position;
                employeeModel.IDTeam = dbEmployee.IDTeam;
                employeeModel.IDBusinessUnit = dbEmployee.IDBusinessUnit;
                employeeModel.HiringDate = dbEmployee.HiringDate;
                employeeModel.TotalDaysOff = dbEmployee.TotalDaysOff;
                employeeModel.DaysOffLeft = dbEmployee.DaysOffLeft;
                return employeeModel;
            }
            return null;
        }
        private Employee MapModelToDbObject(EmployeeModel employeeModel)
        {
            Employee dbEmployee = new Employee();
            if (employeeModel != null)
            {
                dbEmployee.IDEmployee = employeeModel.IDEmployee;
                dbEmployee.FirstName = employeeModel.FirstName;
                dbEmployee.LastName = employeeModel.LastName;
                dbEmployee.Position = (int)employeeModel.Position;
                dbEmployee.IDTeam = employeeModel.IDTeam;
                dbEmployee.IDBusinessUnit = employeeModel.IDBusinessUnit;
                dbEmployee.HiringDate = employeeModel.HiringDate;
                dbEmployee.TotalDaysOff = employeeModel.TotalDaysOff;
                dbEmployee.DaysOffLeft = employeeModel.DaysOffLeft;
                return dbEmployee;
            }
            return null;
        }
        public List<EmployeeModel> GetAllEmployees()
        {
            List<EmployeeModel> allEmployeesList = new List<EmployeeModel>();
            foreach (Employee dbEmployee in dbContext.Employees)
            {
                allEmployeesList.Add(MapDbObjectToModel(dbEmployee));
            }
            return allEmployeesList;
        }
        public EmployeeModel GetEmployeeByID(Guid id)
        {
            EmployeeModel employeeModel = MapDbObjectToModel(dbContext.Employees.FirstOrDefault(x => x.IDEmployee == id));
            return employeeModel;
        }
        public void InsertEmployee(EmployeeModel employeeModel)
        {
            employeeModel.IDEmployee = Guid.NewGuid();
            dbContext.Employees.InsertOnSubmit(MapModelToDbObject(employeeModel));
            dbContext.SubmitChanges();
        }
        public void UpdateEmployee(EmployeeModel employeeModel)
        {
            Employee dbEmployee = dbContext.Employees.FirstOrDefault(x => x.IDEmployee == employeeModel.IDEmployee);
            if (dbEmployee != null)
            {
                dbEmployee.IDEmployee = employeeModel.IDEmployee;
                dbEmployee.FirstName = employeeModel.FirstName;
                dbEmployee.LastName = employeeModel.LastName;
                dbEmployee.Position = (int)employeeModel.Position;
                dbEmployee.IDTeam = employeeModel.IDTeam;
                dbEmployee.IDBusinessUnit = employeeModel.IDBusinessUnit;
                dbEmployee.HiringDate = employeeModel.HiringDate;
                dbEmployee.TotalDaysOff = employeeModel.TotalDaysOff;
                dbEmployee.DaysOffLeft = employeeModel.DaysOffLeft;
                dbContext.SubmitChanges();
            }
        }
        public void DeleteEmployee(Guid id)
        {
            Employee dbEmployeeToDelete = dbContext.Employees.FirstOrDefault(x => x.IDEmployee == id);
            if (dbEmployeeToDelete != null)
            {
                dbContext.Employees.DeleteOnSubmit(dbEmployeeToDelete);
                dbContext.SubmitChanges();
            }
        }
    }
}
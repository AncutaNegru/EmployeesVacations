using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesVacations.Models;
using EmployeesVacations.Models.DBObjects;

namespace EmployeesVacations.Repositories
{
    public class VacationRequestRepository
    {
        private EmployeesAndVacationsModelsDataContext dbContext;
        public VacationRequestRepository()
        {
            this.dbContext = new EmployeesAndVacationsModelsDataContext();
        }
        public VacationRequestRepository(EmployeesAndVacationsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
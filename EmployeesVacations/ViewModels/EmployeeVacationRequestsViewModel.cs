using EmployeesVacations.Models;
using System;
using System.Collections.Generic;

namespace EmployeesVacations.ViewModels
{
    public class EmployeeVacationRequestsViewModel
    {
        public string FullName { get; set; }

        public PositionEnum Position { get; set; }

        public string TeamName { get; set; }

        public string BusinessUnitName { get; set; }

        public DateTime HiringDate { get; set; }

        public int TotalDaysOff { get; set; }

        public int DaysOffLeft { get; set; }

        public List<VacationRequestModel> EmployeeVacations = new List<VacationRequestModel>();

    }
}
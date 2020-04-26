using EmployeesVacations.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace EmployeesVacations.ViewModels
{
    public class EmployeeVacationRequestsViewModel
    {
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        public PositionEnum Position { get; set; }

        [DisplayName("Team")]
        public string TeamName { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }

        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Total Days off")]
        public int TotalDaysOff { get; set; }

        [DisplayName("Days Off Left")]
        public int DaysOffLeft { get; set; }

        public List<VacationRequestModel> EmployeeVacations = new List<VacationRequestModel>();

    }
}
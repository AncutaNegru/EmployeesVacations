using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class EmployeeModel
    {
        public Guid IDEmployee { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [DisplayName("Employee")]
        public string FullName { get { return FirstName + " " + LastName; } }

        public PositionEnum Position { get; set; }

        [DisplayName("Team")]
        public Guid IDTeam { get; set; }

        [DisplayName("Team")]
        public string TeamName { get; set; }


        [DisplayName("Business Unit")]
        public Guid IDBusinessUnit { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }


        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }

        [DisplayName("Total Days Off")]
        public int TotalDaysOff { get; set; }

        [DisplayName("Days Off Remaining")]
        public int DaysOffLeft { get; set; }
    }
}
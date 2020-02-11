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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public PositionEnum Position { get; set; }

        [DisplayName("Team")]
        public Guid IDTeam { get; set; }

        [DisplayName("Business Unit")]
        public Guid IDBusinessUnit { get; set; }
        public DateTime HiringDate { get; set; }

        [DisplayName("Total Days Off")]
        public int TotalDaysOff { get; set; }

        [DisplayName("Days Off Remaining")]
        public int DaysOffLeft { get; set; }

        [DisplayName("Employee")]
        public string FullName { get { return FirstName + " " + LastName; } }

    }
}
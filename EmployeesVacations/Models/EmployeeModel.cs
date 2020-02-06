using System;
using System.Collections.Generic;
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
        public Guid IDTeam { get; set; }
        public Guid IDBusinessUnit { get; set; }
        public DateTime HiringDate { get; set; }
        public int TotalDaysOff { get; set; }
        public int DaysOffLeft { get; set; }
        
    }
}
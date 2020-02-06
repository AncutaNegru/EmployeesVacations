using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class TeamModel
    {
        public Guid IDTeam { get; set; }
        public Guid IDBusinessUnit { get; set; }
        public string Name { get; set; }
        public Guid? IDTeamLead { get; set; }

    }
}
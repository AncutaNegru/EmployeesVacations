using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class TeamModel
    {
        public Guid IDTeam { get; set; }

        [DisplayName("Business Unit")]
        public Guid IDBusinessUnit { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }

        [DisplayName("Team Name")]
        public string Name { get; set; }

        [DisplayName("Team Lead")]
        public Guid? IDTeamLead { get; set; }

        [DisplayName("Team Lead")]
        public string TeamLeadName { get; set; }

    }
}
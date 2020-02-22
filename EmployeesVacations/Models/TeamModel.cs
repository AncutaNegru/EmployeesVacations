﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class TeamModel
    {
        public Guid IDTeam { get; set; }
        public Guid IDBusinessUnit { get; set; }

        [DisplayName("Business Unit")]
        public string BusinessUnitName { get; set; }

        [DisplayName("Team")]
        public string Name { get; set; }
        public Guid? IDTeamLead { get; set; }

        [DisplayName("Team Lead")]
        public string TeamLeadName { get; set; }

    }
}
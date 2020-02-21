using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public enum PositionEnum
    {
        [Display(Name = "Business Unit Manager")]
        BusinessUnitManager = 1,

        [Display(Name = "Team Lead")]
        TeamLead = 2,

        [Display(Name = "Recruitment Specialist")]
        RecruitmentSpecialist = 3,

        [Display(Name = "Business Analyst")]
        BusinessAnalyst =4,

        Developer = 5,

        [Display(Name = "Order Administrator")]
        OrderAdministrator =6,

        Buyer =7,

        [Display(Name = "Project Engineer")]
        ProjectEngineer =8,

        [Display(Name = "Proposal Engineer")]
        ProposalEngineer = 9

    }
}
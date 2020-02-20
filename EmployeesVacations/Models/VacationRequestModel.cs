using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class VacationRequestModel
    {
        public Guid IDVacationRequest { get; set; }
        public Guid IDEmployee { get; set; }

        [DisplayName("Employee")]
        public string EmployeeFullName { get; set; }

        [DisplayName("Reason For Request")]
        public string Reason { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Days Requested")]
        public int DaysRequested { get; set; }

        [DisplayName("First Approval")]
        public ApprovalStatusEnum FirstApproval { get; set; }

        [DisplayName("Second Approval")]
        public ApprovalStatusEnum SecondApproval { get; set; }

        [DisplayName("Status")]
        public ApprovalStatusEnum Status { get; set; }

    }
}
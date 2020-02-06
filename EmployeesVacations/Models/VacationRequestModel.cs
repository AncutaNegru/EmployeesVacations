using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class VacationRequestModel
    {
        public Guid IDVacationRequest { get; set; }
        public Guid IDEmployee { get; set; }
        public string Reason { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DaysRequested { get; set; }
        public ApprovalStatusEnum FirstApproval { get; set; }
        public ApprovalStatusEnum SecondApproval { get; set; }
        public ApprovalStatusEnum Status { get; set; }

    }
}
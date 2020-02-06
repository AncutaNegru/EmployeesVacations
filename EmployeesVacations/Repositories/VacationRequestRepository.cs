using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesVacations.Models;
using EmployeesVacations.Models.DBObjects;

namespace EmployeesVacations.Repositories
{
    public class VacationRequestRepository
    {
        private EmployeesAndVacationsModelsDataContext dbContext;
        public VacationRequestRepository()
        {
            this.dbContext = new EmployeesAndVacationsModelsDataContext();
        }
        public VacationRequestRepository(EmployeesAndVacationsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private VacationRequestModel MapDbObjectToModel(VacationRequest dbVacationRequest)
        {
            VacationRequestModel vacationRequestModel = new VacationRequestModel();
            if (dbVacationRequest != null)
            {
                vacationRequestModel.IDVacationRequest = dbVacationRequest.IDVacationRequest;
                vacationRequestModel.IDEmployee = dbVacationRequest.IDEmployee;
                vacationRequestModel.Reason = dbVacationRequest.Reason;
                vacationRequestModel.StartDate = dbVacationRequest.StartDate;
                vacationRequestModel.EndDate = dbVacationRequest.EndDate;
                vacationRequestModel.DaysRequested = dbVacationRequest.DaysRequested;
                vacationRequestModel.FirstApproval = (ApprovalStatusEnum)dbVacationRequest.FirstApproval;
                vacationRequestModel.SecondApproval = (ApprovalStatusEnum)dbVacationRequest.SecondApproval;
                vacationRequestModel.Status = (ApprovalStatusEnum)dbVacationRequest.Status;
                return vacationRequestModel;
            }
            return null;
        }
    }
}
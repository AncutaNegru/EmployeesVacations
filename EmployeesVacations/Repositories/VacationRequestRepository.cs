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
                vacationRequestModel.EmployeeFullName = dbVacationRequest.Employee.FirstName + " " + dbVacationRequest.Employee.LastName;
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
        private VacationRequest MapModelToDbObject(VacationRequestModel vacationRequestModel)
        {
            VacationRequest dbVacationRequest = new VacationRequest();
            if (vacationRequestModel != null)
            {
                dbVacationRequest.IDVacationRequest = vacationRequestModel.IDVacationRequest;
                dbVacationRequest.IDEmployee = vacationRequestModel.IDEmployee;
                dbVacationRequest.Reason = vacationRequestModel.Reason;
                dbVacationRequest.StartDate = vacationRequestModel.StartDate;
                dbVacationRequest.EndDate = vacationRequestModel.EndDate;
                dbVacationRequest.DaysRequested = vacationRequestModel.DaysRequested;
                dbVacationRequest.FirstApproval = (int)vacationRequestModel.FirstApproval;
                dbVacationRequest.SecondApproval = (int)vacationRequestModel.SecondApproval;
                dbVacationRequest.Status = (int)vacationRequestModel.Status;
                return dbVacationRequest;
            }
            return null;
        }
        public List<VacationRequestModel> GetAllVacationRequests()
        {
            List<VacationRequestModel> vacationRequestsList = new List<VacationRequestModel>();
            foreach (VacationRequest dbVacationRequest in dbContext.VacationRequests)
            {
                vacationRequestsList.Add(MapDbObjectToModel(dbVacationRequest));
            }
            return vacationRequestsList;
        }
        public VacationRequestModel GetVacationRequestById(Guid id)
        {
            return MapDbObjectToModel(dbContext.VacationRequests.FirstOrDefault(x => x.IDVacationRequest == id));
        }

        public List<VacationRequestModel> GetAllVacationRequestsByEmployeeId(Guid id)
        {
            List<VacationRequestModel> requestsListByEmployeeID = new List<VacationRequestModel>();
            List<VacationRequest> dbVacationRequestsByEmployeeId = dbContext.VacationRequests.Where(x => x.IDEmployee == id).ToList();
            foreach(VacationRequest dbVacationRequest in dbVacationRequestsByEmployeeId)
            {
                requestsListByEmployeeID.Add(MapDbObjectToModel(dbVacationRequest));
            }
            return requestsListByEmployeeID;
        }
        public void InsertVacationRequest(VacationRequestModel vacationRequestModel)
        {
            vacationRequestModel.IDVacationRequest = Guid.NewGuid();
            dbContext.VacationRequests.InsertOnSubmit(MapModelToDbObject(vacationRequestModel));
            dbContext.SubmitChanges();
        }
        public void UpdateVacationRequest(VacationRequestModel vacationRequestModel)
        {
            VacationRequest dbExistingVacationRequest = dbContext.VacationRequests.FirstOrDefault(x => x.IDVacationRequest == vacationRequestModel.IDVacationRequest);
            if (dbExistingVacationRequest != null)
            {
                dbExistingVacationRequest.IDVacationRequest = vacationRequestModel.IDVacationRequest;
                dbExistingVacationRequest.IDEmployee = vacationRequestModel.IDEmployee;
                dbExistingVacationRequest.Reason = vacationRequestModel.Reason;
                dbExistingVacationRequest.StartDate = vacationRequestModel.StartDate;
                dbExistingVacationRequest.EndDate = vacationRequestModel.EndDate;
                dbExistingVacationRequest.DaysRequested = vacationRequestModel.DaysRequested;
                dbExistingVacationRequest.FirstApproval = (int)vacationRequestModel.FirstApproval;
                dbExistingVacationRequest.SecondApproval = (int)vacationRequestModel.SecondApproval;
                dbExistingVacationRequest.Status = (int)vacationRequestModel.Status;
                dbContext.SubmitChanges();
            }
        }
        public void DeleteVacationRequest(Guid id)
        {
            VacationRequest vacationRequestToDeleteDb = dbContext.VacationRequests.FirstOrDefault(x => x.IDVacationRequest == id);
            if(vacationRequestToDeleteDb != null)
            {
                dbContext.VacationRequests.DeleteOnSubmit(vacationRequestToDeleteDb);
                dbContext.SubmitChanges();
            }
        }
    }
}
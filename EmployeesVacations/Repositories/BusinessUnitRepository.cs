using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeesVacations.Models;
using EmployeesVacations.Models.DBObjects;

namespace EmployeesVacations.Repositories
{
    public class BusinessUnitRepository
    {
        private EmployeesAndVacationsModelsDataContext dbContext;
        public BusinessUnitRepository()
        {
            this.dbContext = new EmployeesAndVacationsModelsDataContext();
        }
        public BusinessUnitRepository(EmployeesAndVacationsModelsDataContext dbContext)
        {
            this.dbContext = dbContext;
        }
        private BusinessUnitModel MapDbObjectToModel(BusinessUnit dbBusinessUnit)
        {
            BusinessUnitModel businessUnitModel = new BusinessUnitModel();
            if (dbBusinessUnit != null)
            {
                businessUnitModel.IDBusinessUnit = dbBusinessUnit.IDBusinessUnit;
                businessUnitModel.Name = dbBusinessUnit.Name;
                businessUnitModel.IDBusinessUnitManager = dbBusinessUnit.IDBusinessUnitManager;
                if (dbBusinessUnit.Employee != null)
                {
                    businessUnitModel.BusinessUnitManagerName = dbBusinessUnit.Employee.FirstName + " " + dbBusinessUnit.Employee.LastName;
                }
                return businessUnitModel;
            }
            return null;
        }
        private BusinessUnit MapModelToDbObject(BusinessUnitModel businessUnitModel)
        {
            BusinessUnit dbBusinessUnit = new BusinessUnit();
            if(businessUnitModel != null)
            {
                dbBusinessUnit.IDBusinessUnit = businessUnitModel.IDBusinessUnit;
                dbBusinessUnit.Name = businessUnitModel.Name;
                dbBusinessUnit.IDBusinessUnitManager = businessUnitModel.IDBusinessUnitManager;
                return dbBusinessUnit;
            }
            return null;
        }
        public List<BusinessUnitModel> GetAllBusinessUnits()
        {
            List<BusinessUnitModel> allBusinessUnitsList = new List<BusinessUnitModel>();
            foreach(BusinessUnit dbBusinessUnit in dbContext.BusinessUnits)
            {
                allBusinessUnitsList.Add(MapDbObjectToModel(dbBusinessUnit));
            }
            return allBusinessUnitsList;
        }
        public BusinessUnitModel GetBusinessUnitByID(Guid id)
        {
            BusinessUnitModel businessUnitModel = MapDbObjectToModel(dbContext.BusinessUnits.FirstOrDefault(x => x.IDBusinessUnit == id));
            return businessUnitModel;
        }
        public void InsertBusinessUnit(BusinessUnitModel businessUnitModel)
        {
            businessUnitModel.IDBusinessUnit = Guid.NewGuid();
            dbContext.BusinessUnits.InsertOnSubmit(MapModelToDbObject(businessUnitModel));
            dbContext.SubmitChanges();
        }
        public void UpdateBusinessUnit(BusinessUnitModel businessUnitModel)
        {
            BusinessUnit dbBusinessUnit = dbContext.BusinessUnits.FirstOrDefault(x => x.IDBusinessUnit == businessUnitModel.IDBusinessUnit);
            if (dbBusinessUnit != null)
            {
                dbBusinessUnit.IDBusinessUnit = businessUnitModel.IDBusinessUnit;
                dbBusinessUnit.Name = businessUnitModel.Name;
                dbBusinessUnit.IDBusinessUnitManager = businessUnitModel.IDBusinessUnitManager;
                dbContext.SubmitChanges();
            }
        }

        public void UpdateBusinessUnitsIfManagerIsDeleted(Guid id)
        {
            List<BusinessUnit> dbBusinessUnits = dbContext.BusinessUnits.Where(x => x.IDBusinessUnitManager == id).ToList();
            foreach (BusinessUnit dbBusinessUnit in dbBusinessUnits)
            {
                dbBusinessUnit.IDBusinessUnitManager = null;
                dbContext.SubmitChanges();
            }
        }
        public void DeleteBusinessUnit(Guid id)
        {
            BusinessUnit dbBusinessUnitToDelete = dbContext.BusinessUnits.FirstOrDefault(x => x.IDBusinessUnit == id);
            if(dbBusinessUnitToDelete != null)
            {
                dbContext.BusinessUnits.DeleteOnSubmit(dbBusinessUnitToDelete);
                dbContext.SubmitChanges();
            }
        }
        
    }
}
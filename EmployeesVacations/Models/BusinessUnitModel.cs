using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class BusinessUnitModel
    {
        public Guid IDBusinessUnit { get; set; }
        public string Name { get; set; }
        public Guid? IDBusinessUnitManager { get; set; }

    }
}
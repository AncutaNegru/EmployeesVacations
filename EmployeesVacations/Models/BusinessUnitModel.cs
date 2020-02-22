using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeesVacations.Models
{
    public class BusinessUnitModel
    {
        public Guid IDBusinessUnit { get; set; }

        [DisplayName("Business Unit")]
        public string Name { get; set; }
        public Guid? IDBusinessUnitManager { get; set; }

        [DisplayName("Business Unit Manager")]
        public string BusinessUnitManagerName { get; set; }
    }
}
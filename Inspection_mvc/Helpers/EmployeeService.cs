using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspection_mvc.Models.EF; 

namespace Inspection_mvc.Helpers
{
    public class EmployeeService
    {
        private Inspection dbContext; 

        public EmployeeService(Inspection _dbContext = null)
        {
            if (_dbContext != null)
            {
                dbContext = _dbContext;
                return; 
            }
            dbContext = _dbContext; 
        }

        public EmployeeNo addEmployee(Models.SourceCrud crudVars)
        {
            if (crudVars != null)
            {
                validateEmployee(crudVars); 

                EmployeeNo newEmployee = new EmployeeNo();

                newEmployee.FirstName = crudVars.FirstName;
                newEmployee.LastName = crudVars.LastName;

                newEmployee.Initials = crudVars.Initials.ToUpper();
                newEmployee.Active = true;
                newEmployee.Type = "WEAVER";
                newEmployee.CID = "000999";

                dbContext.EmployeeNoes.Add(newEmployee);

                int rows = dbContext.SaveChanges();
                return newEmployee;
            }
            return null;
        }

        private void validateEmployee(Models.SourceCrud crudvars)
        {
            if (crudvars.FirstName.Length == 0)
                throw new Exception("First Name Required");

            if (crudvars.LastName.Length == 0)
                throw new Exception("Last Name Required");

            if (crudvars.Initials.Length == 0)
                throw new Exception("Initials Required"); 
        }

    }
}
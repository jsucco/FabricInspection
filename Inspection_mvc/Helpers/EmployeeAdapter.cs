using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Inspection_mvc.Models.EF;
using System.Data.Entity;

namespace Inspection_mvc.Helpers
{
    public class EmployeeAdapter
    {

        public static async Task<List<EmployeeNo>> getEmployees()
        {
            List<EmployeeNo> data = new List<EmployeeNo>();

            try
            {

                try
                {
                    var cachedEmployees = Helpers.EmployeeAdapter.getEmployeesFromCache();
                    data = cachedEmployees;
                } catch (Exception e)
                {

                }
                

                if (data.Count == 0)
                {
                    data = await getDbEmployees();

                    Helpers.EmployeeAdapter.InsertIntoCache(data);
                }

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return data;
        }

        public static async Task<List<Models.EF.OE_Operators>> getOE_Operators()
        {
            List<Models.EF.OE_Operators> employees = new List<Models.EF.OE_Operators>();

            using (AprSTTcontext _db = new AprSTTcontext())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                var query = from x in _db.OE_Operators where x.OEOP_Active == true && x.OEOP_JobCode == "61257A" orderby x.OEOP_Operator ascending select x;

                employees = await query.ToListAsync();
            }

            return employees;
        }

        private static async Task<List<Models.EF.EmployeeNo>> getDbEmployees()
        {
            List<Models.EF.EmployeeNo> employees = new List<Models.EF.EmployeeNo>();

            using (Inspection _db = new Inspection())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                var query = from x in _db.EmployeeNoes where x.Active == true select x;

                employees = await query.ToListAsync(); 
            }

            return employees;
        }

        public static List<Models.EF.EmployeeNo> getEmployeesFromCache()
        {
            List<Models.EF.EmployeeNo> employees = new List<Models.EF.EmployeeNo>(); 

            try
            {
                var cachedEmployees = (List<Models.EF.EmployeeNo>)HttpContext.Current.Cache["Inspection.Employees"];

                if (cachedEmployees != null)
                {
                    employees = cachedEmployees; 
                }
            } catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                HttpContext.Current.Cache.Remove("Inspection.Employees"); 
            }
            return employees; 
        }

        public static void updateEmployeesAsync(Models.SourceCrud crudVars)
        {
            HttpContext context = HttpContext.Current;

            Task.Run(() => UpdateEmployees(crudVars, context)); 
        }

        public static void deactivateEmployeeAsync(Models.SourceCrud crudVars)
        {
            HttpContext context = HttpContext.Current;

            Task.Run(() => deactivateEmployee(crudVars, context)); 
        }

        public static void addEmployeeAsync(Models.SourceCrud crudVars)
        {
            try
            {
                Task.Run(() => addEmployee(crudVars));
            } catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e); 
            }
            
        }

        private static async void RefreshCache(HttpContext context)
        {
            List<Models.EF.EmployeeNo> employees = await getDbEmployees(); 

            if (employees != null && employees.Count > 0)
            {
                context.Cache.Remove("Inspection.Employees");
                context.Cache.Insert("Inspection.Employees", employees, null, DateTime.Now.AddDays(3), System.Web.Caching.Cache.NoSlidingExpiration);
            }
        }

        public static EmployeeNo addEmployee(Models.SourceCrud crudVars)
        {
            if (crudVars != null)
            {
                using (var _db = new Inspection())
                {
                    EmployeeNo newEmployee = new EmployeeNo();

                    newEmployee.FirstName = crudVars.FirstName;
                    newEmployee.LastName = crudVars.LastName;

                    int matchingInits = (from x in _db.EmployeeNoes where x.Initials == crudVars.Initials.ToUpper() select x).Count();

                    if (matchingInits > 0)
                        crudVars.Initials = crudVars.Initials.ToUpper() + "-" + (matchingInits + 1).ToString(); 

                    newEmployee.Initials = crudVars.Initials.ToUpper();
                    newEmployee.Active = true;
                    newEmployee.Type = "WEAVER";
                    newEmployee.CID = "000999"; 

                    _db.EmployeeNoes.Add(newEmployee);

                    int rows = _db.SaveChanges();
                    return newEmployee;
                }
            }
            return null;
        }

        public static void deactivateEmployee(Models.SourceCrud crudVars, HttpContext context)
        {
            if (crudVars != null)
            {
                int employeeId = getRealEmployeeId(crudVars.Id, context); 

                if (employeeId > 0)
                {
                    using (var _db = new Inspection())
                    {
                        EmployeeNo employee = (from x in _db.EmployeeNoes where x.Id == employeeId select x).First(); 

                        if (employee != null)
                        {
                            employee.Active = false;

                            _db.SaveChanges();
                            RefreshCache(context); 
                        }
                    }
                }
            }
        }
        public static void UpdateEmployees(Models.SourceCrud crudVars, HttpContext context)
        {
            if (crudVars != null)
            {
                int employeeId = getRealEmployeeId(crudVars.Id, context);

                using (var _db = new Inspection())
                {
                    EmployeeNo rowToEdit = (from x in _db.EmployeeNoes where x.Id == employeeId select x).FirstOrDefault(); 

                    if (rowToEdit != null)
                    {
                        rowToEdit.FirstName = crudVars.FirstName;
                        rowToEdit.LastName = crudVars.LastName;
                        rowToEdit.Initials = crudVars.Initials;

                        _db.SaveChanges();
                        RefreshCache(context); 
                    }
                }
            }
        }

        public static void InsertIntoCache(List<Models.EF.EmployeeNo> employees)
        {
            HttpContext context = HttpContext.Current; 
            Task.Run(() => {
                if (employees != null || employees.Count > 0)
                {
                    context.Cache.Insert("Inspection.Employees", employees, null, DateTime.Now.AddDays(3), System.Web.Caching.Cache.NoSlidingExpiration);
                }
            });
        }

        public static List<Models.EF.EmployeeNo> getCachedEmployees()
        {
            List<Models.EF.EmployeeNo> employees = new List<EmployeeNo>(); 

            try
            {
                employees = (List<Models.EF.EmployeeNo>)HttpContext.Current.Cache["Inspection.Employees"];

            } catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                HttpContext.Current.Cache.Remove("Inspection.Employees"); 
            }

            return employees; 
        }

        private static int getRealEmployeeId(int rowid, HttpContext context)
        {
            if (rowid == 0)
                throw new Exception("rowid cannot be zero");

            int EmployeeId = 0 + rowid;
            try
            {

                if (EmployeeId > 0)
                {
                    EmployeeId = getEmployeeIdQuick(rowid, context);
                }
            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            }

            return EmployeeId;
        }

        private static int getEmployeeIdQuick(int realRow, HttpContext context)
        {
            int EmployeeId = 0;

            List<Models.EF.EmployeeNo> employees = (List<Models.EF.EmployeeNo>)context.Cache["Inspection.Employees"];

            if (employees == null)
                throw new Exception("employeelist in cache was empty. cannot complete traction");

            EmployeeNo[] employeeArr = employees.ToArray();

            
            int L = 0;
            int R = employeeArr.Length - 1;
            int n = (L + R) / 2;
            int target = realRow - 1;

            while (L <= R)
            {
                n = (L + R) / 2;
                if (n == target || L==R)
                    return employeeArr[n].Id; 

                if (target > n)
                {
                    L = n + 1; 
                } else if (target < n)
                {
                    R = n - 1; 
                }
            }
            return EmployeeId;
        }
    }
}
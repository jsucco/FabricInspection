using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Inspection_mvc.Models;
using Inspection_mvc.Models.EF;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Inspection_mvc.Controllers
{
    public class ManageController : Controller
    {
        private JavaScriptSerializer jser = new JavaScriptSerializer();
        public static string cachedSessionId = "";
        //
        // GET: /Manage/Index

        public ActionResult Index()
        {

           
            var model = new IndexViewModel
            {
                
            };
            return View(model);
        }

        public async Task<ActionResult> Employees()
        {
            var model = new EmployeeViewModel();

            model.employees = await Helpers.EmployeeAdapter.getEmployees();
            cachedSessionId = ControllerContext.HttpContext.Session.SessionID.ToString();

            if (model.employees != null && model.employees.Count > 0)
            {
                Helpers.jqGrid<Models.EF.EmployeeNo> jq = new Helpers.jqGrid<Models.EF.EmployeeNo>();
                model.gridObj = new Models.jqgridData(); 
                model.gridObj.records = 12;
                model.gridObj.rows = jq.loadPageRecords(1, model.gridObj.records, model.employees);
                model.gridObj.total = (int)Math.Ceiling((decimal)model.employees.Count / model.gridObj.records);
                model.gridObj.page = 1; 
            }

            return View("Employees",model); 
        }

        public async Task<ActionResult> getEmployeesData()
        {
            List<Models.EF.EmployeeNo> employees = new List<Models.EF.EmployeeNo>();

            employees = await Helpers.EmployeeAdapter.getEmployees();

            return Json(employees, JsonRequestBehavior.AllowGet); 
        }

        [HttpGet]
        public ActionResult cachePageInfo(string page, string viewRows, string table)
        {
            HttpContext.Cache.Insert("Inspection." + table + ".viewInfo", new int[2] { Convert.ToInt16(page), Convert.ToInt16(viewRows) }, null, DateTime.Now.AddMinutes(120), System.Web.Caching.Cache.NoSlidingExpiration);

            return Json("true", JsonRequestBehavior.AllowGet); 
        }

        public ActionResult EditEmployee()
        {

            System.Collections.Specialized.NameValueCollection RequestParams = ControllerContext.RequestContext.HttpContext.Request.Params;

            bool result = false; 

            if (RequestParams.Count > 0)
            {
                Helpers.jqGrid<Models.SourceCrud> jqgrid = new Helpers.jqGrid<Models.SourceCrud>();

                Models.SourceCrud crudVars = jqgrid.getReqParamsAsObject(RequestParams);
                if (crudVars == null)
                    return Json(result, JsonRequestBehavior.AllowGet);              

                switch (crudVars.oper)
                {
                    case "edit":
                        Helpers.EmployeeAdapter.updateEmployeesAsync(crudVars); 
                        break;
                    case "add":
                        Helpers.EmployeeAdapter.addEmployeeAsync(crudVars);
                        try
                        {
                            HttpContext.Cache.Remove("Inspection.Employees");
                        } catch
                        {

                        }
                        
                        break;
                    case "del":
                        Helpers.EmployeeAdapter.deactivateEmployeeAsync(crudVars);                     
                        break; 
                }
                
                result = true; 
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RM()
        {
            Models.RMViewModel model = new Models.RMViewModel();
            Helpers.InspectionService service = new Helpers.InspectionService();

            model.records = service.getRMTable(); 

            return View(model); 
        }

        [HttpPost]
        public JsonResult getRMs()
        {
            Helpers.InspectionService service = new Helpers.InspectionService();

            return Json(jser.Serialize(service.getRMTable()), JsonRequestBehavior.AllowGet); 
        }

        [HttpPost]
        public ActionResult editRMs()
        {
            System.Collections.Specialized.NameValueCollection RequestParams = ControllerContext.RequestContext.HttpContext.Request.Params;

            bool updated = false;

            if (RequestParams.Count > 0)
            {
                Helpers.jqGrid<Models.RMCrud> jqgrid = new Helpers.jqGrid<Models.RMCrud>();

                Models.RMCrud crudVars = jqgrid.getReqParamsAsObject(RequestParams);
                if (crudVars == null)
                    return Json(updated, JsonRequestBehavior.AllowGet);

                Helpers.InspectionService service = new Helpers.InspectionService();
                try
                {
                    switch (crudVars.oper)
                    {
                        case "edit":
                            updated = service.updateRM(crudVars);                       
                            break;
                        case "add":
                            updated = service.addRM(crudVars);                      
                            break;
                        case "del":
                            updated = service.deleteRM(crudVars); 
                            break;
                    }             
                    
                    HttpContext.Cache.Remove("Inspection.RollRM_Xref");
                }
                catch (Exception ex)
                {
                    return Json(ex.Message, JsonRequestBehavior.AllowGet); 
                }
            }

            return Json(updated, JsonRequestBehavior.AllowGet);
        }

        #region Helpers

        

        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Inspection_mvc.Helpers;
using System.Web.Security;
using System.Configuration;

namespace Inspection_mvc.Controllers
{
    public class RollController : Controller
    {
        public string sessionId;
        private JavaScriptSerializer jser = new JavaScriptSerializer(); 
        
        public ActionResult InfoEntry()
        {
            Models.InfoEntryViewModel model = new Models.InfoEntryViewModel();
            InspectionService service = new InspectionService();

            model.rmtable = service.getRMTable(); 

            model.inputs = getInfoEntryPageInputs(model); 

            return View("InfoEntry",model); 
        }

        [HttpPost]
        public ActionResult InfoEntry(ICollection<Helpers.InputObject> PageInput)
        {
            if (PageInput == null || PageInput.Count == 0)
                throw new Exception("Server did not received view inputs.");

            if (PageInput.Count != 6)
                throw new Exception("Server recieved an incorect amount of view inputs.");

            Helpers.InspectionJob job = new Helpers.InspectionJob(PageInput, new Helpers.InspectionJob.Type { fabric = true });
            Models.InfoEntryViewModel model = new Models.InfoEntryViewModel();
            if (job.currentJob.RM_XrefId == null || job.currentJob.RM_XrefId == 0)
            {
                InspectionService service = new InspectionService();

                model.rmtable = service.getRMTable();
                model.inputs = getInfoEntryPageInputs(model, job.currentJob.JobNumber, null, job.currentJob.RollWidth.ToString(), job.currentJob.ThreadColor);
                model.inputs[1].errorFlag = true;
                model.inputs[1].errorMessage = "RM Number must be selected.";

                return View(model);
            }
            else if (job.currentJob.JobNumber.Length == 0)
            {
                InspectionService service = new InspectionService();

                model.rmtable = service.getRMTable();
                model.inputs = getInfoEntryPageInputs(model, null, job.currentJob.RM_XrefId.ToString(), job.currentJob.RollWidth.ToString(), job.currentJob.ThreadColor);
                model.inputs[0].errorFlag = true;
                model.inputs[0].errorMessage = "Loom Number cannot be blank.";
                return View(model);
            }
            else if (job.currentJob.RM_XrefId > 0)
            {
                InspectionService service = new InspectionService();
                Dictionary<string, Models.EF.RollRM_Xref> xrefs = service.getRMDictionary();
 
                if (xrefs.ContainsKey(job.currentJob.PRP_Code))
                {
                    if (xrefs[job.currentJob.PRP_Code].IDThread == true && job.currentJob.ThreadColor.Trim().Length == 0)
                    {
                        model.rmtable = service.getRMTable(); 
                        model.inputs = getInfoEntryPageInputs(model, job.currentJob.JobNumber, job.currentJob.RM_XrefId.ToString(), job.currentJob.RollWidth.ToString(), job.currentJob.ThreadColor);
                        model.inputs[2].input.hidden = false;
                        model.inputs[0].errorFlag = true;
                        model.inputs[0].errorMessage = "Thread Color must be selected.";
                        return View(model); 
                    } 
                }
                job.currentJob.PRP_Code = ""; 
            } else if (job.currentJob.RollWidth == null || job.currentJob.RollWidth == 0)
            {
                InspectionService service = new InspectionService();

                model.rmtable = service.getRMTable();
                model.inputs = getInfoEntryPageInputs(model, job.currentJob.JobNumber, job.currentJob.RM_XrefId.ToString(), null, job.currentJob.ThreadColor);
                model.inputs[0].errorFlag = true;
                model.inputs[0].errorMessage = "Roll Width must be defined.";
                return View(model);
            }

            TempData["InfoEntryInputs"] = PageInput;
            return RedirectToAction("WeaverEntry");

                
        }

        public ActionResult Start()
        {
            return View("Start"); 
        }

        public async  Task<ActionResult> WeaverEntry()
        {
            var RequiredInspectionInfo = TempData["InfoEntryInputs"];

            TempData["InfoEntryInputs"] = RequiredInspectionInfo; 

            var Jobid = ControllerContext.HttpContext.Request.QueryString["JobId"];

            Models.WeaverEntryViewModel model = new Models.WeaverEntryViewModel();

            if (Jobid != null)
            {
                model.JobId = Convert.ToInt32(Jobid);
                model.LastWeaverShiftId = Convert.ToInt32(ControllerContext.HttpContext.Request.QueryString["LastWeaverShiftId"]); 
            }
            else if (RequiredInspectionInfo != null)
            {
                model.RequiredInputs = (ICollection<Helpers.InputObject>)RequiredInspectionInfo;
                InputObject templateid = new InputObject();
                templateid.id = "TemplateId";
                templateid.value = getPageTemplateId().ToString();
                model.RequiredInputs.Add(templateid);
            }
            else
                return RedirectToAction("InfoEntry");

            model.inputs = await getWeaverInfoPageInputs();

            if (model.inputs == null)
                throw new Exception("Page Inputs are required"); 

            return View("WeaverEntry", model); 
        }

        [HttpPost]
        public async Task<ActionResult> WeaverEntry(ICollection<Helpers.InputObject> PageInputs)
        {
            if (PageInputs == null || PageInputs.Count == 0)
                throw new Exception("Server did not received view inputs.");

            Models.EF.InspectionJobSummary thisJobObject = null; 

            Helpers.Shift newShift = new Helpers.Shift(PageInputs);

            string errorMessage = ""; 
            try
            {
                if (newShift.model.JobId == 0)
                {
                    if (newShift.model.Weaver1.Trim().Length == 0)
                        throw new Exception("At Least one weaver must be selected.");

                    thisJobObject = CreateInspection(PageInputs);
                } 
                
                newShift.StartWeaverShift(thisJobObject); 
            } catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                errorMessage = ex.Message; 
            }
            
            if (errorMessage.Length == 0)
            { 
                return RedirectToAction("DefectEntry", new { JobId = newShift.model.JobId, shift=newShift.model.WeaverShiftId });
            } else
            {
                Models.WeaverEntryViewModel errorModel = new Models.WeaverEntryViewModel();
                errorModel.inputs = await getWeaverInfoPageInputs();
                errorModel.inputs[0].errorFlag = true;
                errorModel.inputs[0].errorMessage = "Error Creating Job - " + errorMessage;
                errorModel.JobId = newShift.model.JobId;
                errorModel.RequiredInputs = (ICollection<Helpers.InputObject>)TempData["InfoEntryInputs"];
                return View(errorModel); 
            }
        }

        public ActionResult EndShift()
        {
            Models.EndShiftViewModel model = new Models.EndShiftViewModel();

            var JobId = ControllerContext.HttpContext.Request.QueryString["JobId"];
            var WeaverShiftId = ControllerContext.HttpContext.Request.QueryString["weaverShiftId"];

            if (JobId == null || WeaverShiftId == null)
                return RedirectToAction("Start");

            model.WeaverShiftId = Convert.ToInt32(WeaverShiftId);
            model.JobId = Convert.ToInt32(JobId); 

            model.inputs = getEndShiftPageInputs();

            return View(model); 
        }

        [HttpPost]
        public ActionResult EndShift(ICollection<Helpers.InputObject> inputs)
        {
            if (inputs == null || inputs.Count == 0)
                return RedirectToAction("Start");

            bool shiftEnded = false;
            string errorMessage = ""; 

            Helpers.Shift thisShift = null;
            try
            {
                thisShift = new Helpers.Shift(inputs);
                if (thisShift != null)
                {
                    thisShift.EndShift();
                    shiftEnded = true;
                }
                
            } catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                errorMessage = e.Message;       
            }

            if (shiftEnded && thisShift != null)
                return RedirectToAction("WeaverEntry", new { JobId = thisShift.model.JobId, LastWeaverShiftId = thisShift.model.WeaverShiftId }); 
            else
            {
                Models.EndShiftViewModel model = new Models.EndShiftViewModel();

                if (thisShift == null || thisShift.model.JobId == 0 || thisShift.model.WeaverShiftId == 0)
                    return RedirectToAction("Start");

                model.JobId = thisShift.model.JobId;
                model.WeaverShiftId = thisShift.model.WeaverShiftId;
                model.inputs = getEndShiftPageInputs(); 
                if (errorMessage.Length > 0)
                {
                    model.inputs[0].errorFlag = true;
                    model.inputs[0].errorMessage = errorMessage; 
                }
                return View(model); 
            }
            
        }

        public async Task<ActionResult> DefectEntry()
        {
            var JobId = ControllerContext.HttpContext.Request.QueryString["JobId"];
            var WeaverShift = ControllerContext.HttpContext.Request.QueryString["shift"];

            if (JobId == null || WeaverShift == null)
                return RedirectToAction("Start"); 

            sessionId = ControllerContext.HttpContext.Session.SessionID.ToString();
            Helpers.Template template = new Helpers.Template(getPageTemplateId(), sessionId);
            Models.DefectEntryViewModel model = new Models.DefectEntryViewModel();
            Helpers.InspectionJob inspect = new Helpers.InspectionJob();

            model.currentJob = await inspect.getJobById(Convert.ToInt32(JobId));
            model.currentShiftId = Convert.ToInt32(WeaverShift);
            var lastShift = await inspect.getLastShift(Convert.ToInt32(JobId));

            if (lastShift == 0 || lastShift < model.currentShiftId || model.currentJob == null)
                RedirectToAction("Start");

            model.TemplateId = template.SelectedTemplateId;
            model.buttons = await template.getButtons();
            model.tabs = await template.getTabs();

            return View("DefectEntry",model);
        }

        [HttpPost]
        public JsonResult InsertDefect(string jobStr, int ButtonTemplateId, string DefectDesc, int WeaverShiftId, int TemplateId)
        {
            string returnMessage = "";
            Models.InspectionJobSummaryIns JobSummary = new Models.InspectionJobSummaryIns(); 
            try
            {
                if (ButtonTemplateId == 0)
                return Json("error: buttonTemplateId cannot be zero", JsonRequestBehavior.AllowGet);

                if (DefectDesc.Length == 0)
                    return Json("error: buttonTemplateId cannot be zero", JsonRequestBehavior.AllowGet);

                if (WeaverShiftId == 0)
                    return Json("error: WeaverShiftId cannot be zero", JsonRequestBehavior.AllowGet);

                JobSummary = jser.Deserialize<Models.InspectionJobSummaryIns>(jobStr);

                if (JobSummary == null)
                    return Json("error: server could not deserialize page inputs.  Check model object.");

                Models.EF.DefectMaster newDefect = new Models.EF.DefectMaster();

                newDefect.WeaverShiftId = WeaverShiftId;
                newDefect.DefectDesc = DefectDesc;
                newDefect.ButtonTemplateId = ButtonTemplateId;
        
                Helpers.InspectionJob currentJob = new Helpers.InspectionJob(null, null);
                JobSummary.TemplateId = TemplateId;
                currentJob.loadJobRecord(JobSummary);
                
                returnMessage = currentJob.InsertDefect(newDefect, WeaverShiftId).ToString();
            }
            catch (Exception e)
            {
                returnMessage = "error: " + e.Message;
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UndoLastDefect(int JobId, int WeaverShiftId)
        {
            Helpers.Defect defects = new Helpers.Defect(new Models.EF.InspectionJobSummary { id = JobId });

            string desc = "NA";
            try
            {
                defects.WeaverShiftId = WeaverShiftId; 
                desc = defects.UndoLast();
            }
            catch (Exception ex)
            {
                desc = "undo error: " + ex.Message; 
            }

            return Json(desc, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult RollComplete()
        {
            var QryJobId = ControllerContext.HttpContext.Request.QueryString["JobId"];
            var WeaverShiftId = ControllerContext.HttpContext.Request.QueryString["WeaverShiftId"];
            
            if (QryJobId == null)
                return RedirectToAction("Start");

            if (WeaverShiftId == null)
                return RedirectToAction("WeaverEntry", new { JobId = QryJobId }); 

            Models.RollCompleteViewModel model = new Models.RollCompleteViewModel();
            int jobid = 0;
            Int32.TryParse(QryJobId, out jobid); 
            model.inputs = getRollCompletePageInputs(jobid);
            model.JobId = Convert.ToInt32(QryJobId);
            model.WeaverShiftId = Convert.ToInt32(WeaverShiftId); 

            return View(model); 
        }

        [HttpPost]
        public ActionResult RollComplete(ICollection<Helpers.InputObject> inputs)
        {
            if (inputs == null || inputs.Count == 0)
                return RedirectToAction("Start");

            bool shiftEnded = false;
            string errorMessage = "";

            Helpers.Shift thisShift = null;
            try
            {
                thisShift = new Helpers.Shift(inputs);
                if (thisShift != null)
                {
                    thisShift.EndShift();
                    shiftEnded = true;
                }

            }
            catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
                errorMessage = e.Message;
            }

            if (shiftEnded && thisShift != null) {
                TempData["Comments"] = thisShift.model.Comments;
                TempData["RollCompleteInputs"] = inputs; 
                return RedirectToAction("End", new { JobId = thisShift.model.JobId, WeaverShiftId = thisShift.model.WeaverShiftId });
            }
            else
            {
                Models.RollCompleteViewModel model = new Models.RollCompleteViewModel();

                if (thisShift == null || thisShift.model.JobId == 0 || thisShift.model.WeaverShiftId == 0)
                    return RedirectToAction("Start");

                model.JobId = thisShift.model.JobId;
                model.WeaverShiftId = thisShift.model.WeaverShiftId;
                model.inputs = getRollCompletePageInputs(model.JobId);
                if (errorMessage.Length > 0)
                {
                    model.inputs[0].errorFlag = true;
                    model.inputs[0].errorMessage = errorMessage;
                }
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            string LoginAddress = ConfigurationManager.AppSettings["Login"];
            return Redirect(LoginAddress + "?returnUrl=" + HttpContext.Request.Url.AbsoluteUri);
        }
        public ActionResult End()
        {
            var JobId = ControllerContext.HttpContext.Request.QueryString["JobId"];
            var LastWeaverShift = ControllerContext.HttpContext.Request.QueryString["WeaverShiftId"];
            var Comments = TempData["Comments"];
            var PassedInputs = TempData["RollCompleteInputs"]; 
            if (JobId == null)
                return RedirectToAction("Start");

            if (LastWeaverShift == null)
                return RedirectToAction("WeaverEntry", new { JobId = JobId });

            if (PassedInputs == null)
                return RedirectToAction("RollComplete", new { JobId = JobId, WeaverShiftId=LastWeaverShift }); 

            if (Comments == null)
                Comments = ""; 

            Models.EndViewModel model = new Models.EndViewModel();

            model.JobId = Convert.ToInt32(JobId);
            ICollection<Helpers.InputObject> rollcompleteInputs = (ICollection<Helpers.InputObject>)PassedInputs;
            foreach (var item in rollcompleteInputs)
            {
                if (item.id == "ThreadColorConfirm")
                    model.threadconfirm = item;
            }
            if (model.threadconfirm == null)
            {
                Helpers.InputObject obj = new InputObject();
                obj.id = "ThreadColorConfirm";
                obj.name = "ThreadColorConfirm";
                obj.value = ""; 
                model.threadconfirm = obj;
            }
               

            model.Comments = Comments.ToString();
            if (LastWeaverShift != null)
                model.LastWeaverShiftId = Convert.ToInt32(LastWeaverShift);

            return View(model); 
        }

        [HttpPost]
        public ActionResult End(int JobId, string Comments = "", bool ThreadColorConfirm = true)
        {
            if (JobId == 0)
                return RedirectToAction("Start");

            Helpers.InspectionJob thisjob = new Helpers.InspectionJob();

           // Task.Run(() => thisjob.CompleteFabricJob(JobId, Comments, ThreadColorConfirm));
            thisjob.CompleteFabricJob(JobId, Comments, ThreadColorConfirm);

            return RedirectToAction("Start"); 
        }

        #region Helpers

        private int getPageTemplateId()
        {
            int TemplateId = 76;

            using (Models.EF.Inspection _db = new Models.EF.Inspection())
            {
                TemplateId = (from x in _db.TemplateNames where x.Name.ToUpper() == "FABRIC" select x.TemplateId).FirstOrDefault(); 
            }

            if (HttpContext.Request != null && HttpContext.Request.QueryString.Count > 0)
            {
                if (HttpContext.Request.QueryString["TemplateId"] != null)
                {
                    TemplateId = Convert.ToInt32(HttpContext.Request.QueryString["TemplateId"]);
                }
            }

            return TemplateId;
        }

        private async Task<Helpers.PageInput> getWeaverDropDown(string name, string placeHolder)
        {
            Helpers.PageInput newObject = new Helpers.PageInput("select", placeHolder, name);

            try
            {
                List<Models.EF.OE_Operators> weavers = await Helpers.EmployeeAdapter.getOE_Operators();

                if (weavers != null && weavers.Count > 0)
                {
                    foreach (var item in weavers)
                    {
                        newObject.input.options.Add(new Helpers.InputObject.option { text = item.OEOP_Operator + " (ADP:" + item.OEOP_ADP + " )", value = item.OEOP_ADP });
                    }
                }
            } catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e); 
            }
            
            return newObject;
        }

        private Helpers.PageInput getRMDropDown(List<Models.EF.RollRM_Xref> rmtable, string selectedVal = "")
        {
            Helpers.PageInput newObject = new Helpers.PageInput("select", "What is the RM Number?", "RM_XrefId", "RM_XrefId", ""); 

            try
            {
                newObject.input.value = selectedVal.Trim(); 
                if (rmtable != null && rmtable.Count > 0)
                {
                    foreach(var item in rmtable)
                    {
                        newObject.input.options.Add(new Helpers.InputObject.option { text = item.RMin, value = item.Id.ToString() });
                    }
                }
            } catch (Exception e)
            {
                newObject.errorFlag = true;
                newObject.errorMessage = e.Message; 
                Elmah.ErrorSignal.FromCurrentContext().Raise(e); 
            }
            return newObject;
        }

        private Helpers.PageInput getColorsDropDown(List<Models.EF.RollRM_Xref> rmtable, string selectedVal = "")
        {
            Helpers.PageInput newObject = new Helpers.PageInput("select", "What is the ID Yarn Color?", "IDThreadColor", "IDThreadColor", "");

            try
            {
                newObject.input.hidden = true;
                newObject.input.value = selectedVal; 
                newObject.input.width = "70%"; 
                if (rmtable != null && rmtable.Count > 0)
                {
                    foreach(var item in rmtable)
                    {
                        if (item.IDThread == true) 
                            newObject.input.options.Add(new Helpers.InputObject.option { text = item.IDThreadColor, value = item.IDThreadColor });
                    }
                }
            } catch (Exception e)
            {
                newObject.errorFlag = true;
                newObject.errorMessage = e.Message;
                Elmah.ErrorSignal.FromCurrentContext().Raise(e); 
            }

            newObject.errorFlag = true;
            newObject.errorMessage = ""; 

            return newObject; 
        }

        private async Task<Models.WeaverEntryViewModel> validateWeaverEntryInputs(ICollection<Helpers.InputObject> inputs)
        {
            Models.WeaverEntryViewModel Model = new Models.WeaverEntryViewModel();

            List<Helpers.PageInput> newInputs = new List<Helpers.PageInput>();
            var inputArray = inputs.ToArray();
          
            if (inputs == null || inputs.Count == 0 || inputArray[0].value.Length == 0)
            {
                
                Helpers.PageInput select1 = await getWeaverDropDown("Weaver1", "Weaver 1");
                select1.errorFlag = true;
                select1.errorMessage = "At least one Weaver is required.";

                Helpers.PageInput select2 = await getWeaverDropDown("Weaver2", "Weaver 2 (if applicable)");

                newInputs.Add(select1);
                newInputs.Add(select2);
            }
            
            return Model;
        }

        private Models.EF.InspectionJobSummary CreateInspection(ICollection<Helpers.InputObject> inputs)
        {
            Models.EF.InspectionJobSummary thisinspection = new Models.EF.InspectionJobSummary(); 
           
            Helpers.InspectionJob Inspect = new Helpers.InspectionJob(inputs, new Helpers.InspectionJob.Type { fabric = true });

            Inspect.currentJob.EmployeeNo = User.Identity.Name;
            Inspect.currentJob.Inspection_Started = DateTime.Now;
            Inspect.currentJob.ItemFailCount = 0;
            Inspect.currentJob.CID = "578";
            Inspect.currentJob.Standard = "Regular";
            Inspect.currentJob.RejectLimiter = 0;
            Inspect.currentJob.UnitDesc = Inspect.currentJob.DataNo;
            Inspect.currentJob.WOQuantity = 1;
            Inspect.currentJob.WorkOrderPieces = 1;
            Inspect.currentJob.SampleSize = 1;
            Inspect.currentJob.TotalInspectedItems = 1;
            Inspect.currentJob.PRP_Code = "";
            Inspect.currentJob.UnitCost = 0;
            Inspect.currentJob.Comments = "";
            Inspect.currentJob.MajorsCount = 0;
            Inspect.currentJob.MinorsCount = 0;
            Inspect.currentJob.RepairsCount = 0;
            Inspect.currentJob.RepairsCount = 0;
            Inspect.currentJob.CasePack = "0";
            Inspect.currentJob.WorkRoom = "SAT"; 
            thisinspection = Inspect.Create(true, new Helpers.InspectionJob.Type { fabric = true });

            return Inspect.currentJob;
        }
       
        private List<Helpers.PageInput> getEndShiftPageInputs()
        {
            List<Helpers.PageInput> newInputs = new List<Helpers.PageInput>();

            Helpers.PageInput input1 = new Helpers.PageInput("default", "Yards Inspected", "YardsInspected", "YardsInspected");

            input1.input.width = "30%";

            Helpers.PageInput input2 = new Helpers.PageInput("default", "Comments (Optional)", "Comments", "Comments");

            input2.input.width = "90%";

            newInputs.Add(input1);
            newInputs.Add(input2);

            return newInputs;
        }

        public async Task<List<Helpers.PageInput>> getWeaverInfoPageInputs()
        {
            List<Helpers.PageInput> newInputs = new List<Helpers.PageInput>();

            Helpers.PageInput select1 = await getWeaverDropDown("Weaver1", "Weaver 1");

            Helpers.PageInput select2 = await getWeaverDropDown("Weaver2", "Weaver 2 (if applicable)");

            newInputs.Add(select1);
            newInputs.Add(select2);

            return newInputs;
        }

        public List<Helpers.PageInput> getRollCompletePageInputs(int jobid)
        {
            List<Helpers.PageInput> newInputs = new List<Helpers.PageInput>();

            Helpers.PageInput input1 = new Helpers.PageInput("default", "Yards Inspected", "YardsInspected", "YardsInspected");

            input1.input.width = "30%";

            Helpers.PageInput input2 = new Helpers.PageInput("default", "Any Comments?", "Comments", "Comments");

            input2.input.width = "90%";

            newInputs.Add(input1);

            InspectionJob service = new InspectionJob();

            Models.EF.InspectionJobSummary jobObj = service.getJobByIdsync(jobid);

            if (jobObj != null && jobObj.ThreadColor != null && jobObj.ThreadColor.Length > 0)
            {
                Helpers.PageInput colorconfirm = new Helpers.PageInput("select", "Was the ID Yarn {" + jobObj.ThreadColor + "} for the entire roll?", "ThreadColorConfirm", "ThreadColorConfirm", "");

                colorconfirm.input.options.Add(new Helpers.InputObject.option { text = "Yes", value = "true" });
                colorconfirm.input.options.Add(new Helpers.InputObject.option { text = "No", value = "false" });
                newInputs.Add(colorconfirm);
            }
           
            newInputs.Add(input2);
            

            return newInputs; 
        }

        public List<Helpers.PageInput> getInfoEntryPageInputs(Models.InfoEntryViewModel model,string LoomNumber = "", string RMId = "", string RollWidth = "", string color = "")
        {
            List<Helpers.PageInput> newInputs = new List<Helpers.PageInput>();

            newInputs.Add(new Helpers.PageInput("default", "What is the Loom Number?", "LoomNumber", null, LoomNumber));

            newInputs.Add(getRMDropDown(model.rmtable, RMId));

            newInputs.Add(getColorsDropDown(model.rmtable, color));

            Helpers.PageInput rollwidth = new Helpers.PageInput("default", "What is the Roll Width?", "RollWidth", "RollWidth", RollWidth);
            rollwidth.input.inputtype = "number"; 
            newInputs.Add(rollwidth);

            Helpers.PageInput hiddenRM = new PageInput("default", "RMIN", "RMNumber", "RMNumber", "");
            hiddenRM.input.inputtype = "hidden";
            newInputs.Add(hiddenRM);

            Helpers.PageInput hiddenRMin = new PageInput("default", "RMholder", "RMholder", "RMholder", "");
            hiddenRMin.input.inputtype = "hidden";
            newInputs.Add(hiddenRMin);

            return newInputs;
        }

        #endregion

    }
}
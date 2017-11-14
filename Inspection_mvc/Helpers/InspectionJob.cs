using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Inspection_mvc.Models.EF;
using System.Reflection;
using System.Data.Entity; 

namespace Inspection_mvc.Helpers
{
    public class InspectionJob
    {
        public InspectionJob(ICollection<Helpers.InputObject> parameters = null, InspectionJob.Type type = null )
        {
            if (parameters == null || type == null)
                return;
       
            InitalizeType(type);
            mapJobParameters(parameters);
            
        }

        private void mapJobParameters(ICollection<Helpers.InputObject> parameters)
        {
            foreach (var item in parameters)
            {
                var Name = item.id;
                if (Name != null && mappedFields.ContainsKey(Name.ToUpper()))
                {
                    string JobFieldName = mappedFields[Name.ToUpper()];
                    System.Type classType = currentJob.GetType();
                    PropertyInfo JobField = classType.GetProperty(JobFieldName);
                    if (JobField != null)
                    {
                        try
                        {
                            System.Type propertyType = JobField.PropertyType; 
                            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                propertyType = propertyType.GetGenericArguments()[0];
                            }
                            JobField.SetValue(currentJob, ConvertType(item.value, propertyType.Name));
                        } catch (Exception ex)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
                        }                      
                    }
                    
                }
                
            }
        }

        public Models.EF.InspectionJobSummary currentJob = new Models.EF.InspectionJobSummary();
        private Dictionary<string, string> mappedFields = new Dictionary<string, string>();
        private InspectionJob.Type Jobtype = new InspectionJob.Type();
        private List<int> weaverid = new List<int>();

        private void InitalizeType(InspectionJob.Type thistype)
        {
            Jobtype = thistype;
            if (Jobtype == null)
                throw new Exception("Job Object cannot be null");

            mappedFields.Add("JOBID", "id");
            mappedFields.Add("JOBTYPE", "JobType");
            mappedFields.Add("JOBNUMBER", "JobNumber");
            mappedFields.Add("TEMPLATEID", "TemplateId");
            mappedFields.Add("DATANO", "DataNo");
            mappedFields.Add("CID", "CID");
            mappedFields.Add("EMPLOYEENO", "EmployeeNo");

            System.Type insJobType = Jobtype.GetType(); 
            foreach (var item in insJobType.GetProperties())
            {
                var FieldVal = (bool)item.GetValue(Jobtype); 
                if (item.Name.ToUpper() == "FABRIC" && FieldVal == true)
                {
                    mappedFields.Add("RMNUMBER", "DataNo");
                    mappedFields.Add("RMHOLDER", "PRP_Code");
                    mappedFields.Add("RM_XREFID", "RM_XrefId");
                    mappedFields.Add("IDTHREADCOLOR", "ThreadColor");
                    mappedFields.Add("ROLLWIDTH", "RollWidth");       
                    mappedFields.Add("LOOMNUMBER", "JobNumber");
                    currentJob.JobType = item.Name.ToUpper();
                   
                    break;
                }
            }
        }

        public void loadJobRecord(Models.InspectionJobSummaryIns jobrecord)
        {

            FieldInfo[] objTypes2 = jobrecord.GetType().GetFields();

            foreach (var item in objTypes2)
            {
                PropertyInfo field = currentJob.GetType().GetProperty(item.Name);
                var value = item.GetValue(jobrecord);
                field.SetValue(currentJob, value);
            }
        }
        private void validateRequiredFields()
        {
            if (Jobtype.fabric == true)
            {
                if (currentJob.JobType != "FABRIC" || currentJob.JobType.Length == 0)
                    throw new Exception("invalid job type.");

                if (currentJob.DataNo.Length == 0)
                    throw new Exception("job requires a dataNo.");

                if (currentJob.JobNumber.Length == 0)
                    throw new Exception("job requires a jobnumber");

                if (currentJob.TemplateId == 0)
                    throw new Exception("job requires a templateid.");

                if (currentJob.EmployeeNo.Length == 0)
                    throw new Exception("job requires an employeeNo.");
            }
        }

        public InspectionJobSummary Create(bool getexisting = true, Type JobTypeDec = null)
        {
            if (currentJob == null && getexisting)
                throw new Exception("Job Object cannot be null");

            if (Jobtype == null)
                throw new Exception("Must declare a jobtype.");
            Jobtype = JobTypeDec;

            validateRequiredFields(); 

            using (Inspection _db = new Inspection())
            {
               
                _db.InspectionJobSummaries.Add(currentJob);

                int rowsAff = _db.SaveChanges();

                if (rowsAff > 0)
                    return currentJob;
                else
                    return null; 
            }
        } 

        public async Task<InspectionJobSummary> getJobById(int JobId)
        {
            if (JobId == 0)
                throw new Exception("JobId cannot be 0");

            using (Inspection _db = new Inspection())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                var Job = await (from x in _db.InspectionJobSummaries where x.id == JobId select x).FirstAsync();

                if (Job != null)
                    return Job;
            }
            return null;
        }

        public InspectionJobSummary getJobByIdsync(int JobId)
        {
            if (JobId == 0)
                throw new Exception("JobId cannot be 0");

            using (Inspection _db = new Inspection())
            {
                _db.Configuration.ProxyCreationEnabled = false;
                var Job = (from x in _db.InspectionJobSummaries where x.id == JobId select x).First();

                if (Job != null)
                    return Job;
            }
            return null;
        }

        public async Task<int> getLastShift(int JobId)
        {
            if (JobId == 0)
                throw new Exception("JobId cannot be 0");

            using (Inspection _db = new Inspection())
            {
                var shiftRec = await (from x in _db.WeaverProductions where x.JobSummaryId == JobId orderby x.ShiftId descending select x.ShiftId).FirstOrDefaultAsync();
                
               if (shiftRec > 0)
                    return shiftRec; 
            }
            return 0; 
        }

        public string InsertDefect(DefectMaster defect, int WeaverShiftId = 0)
        {
            if (currentJob == null)
                throw new Exception("Defect Must have a start Inpsection job");
            Defect newDefect = new Defect(currentJob);
            newDefect.WeaverShiftId = WeaverShiftId; 
            return newDefect.Insert(defect); 
        }

        public InspectionJobSummary getFabricJob(string LoomNumber, string RMNumber)
        {
            if (LoomNumber.Length == 0)
                return null;
            try
            {
                using (Inspection _db = new Inspection())
                {
                    var foundObj = (from x in _db.InspectionJobSummaries
                                          where x.JobNumber.ToUpper() == LoomNumber.ToUpper() && x.DataNo.ToUpper() == RMNumber.ToUpper()
                                          orderby x.Inspection_Started descending
                                          select x).ToList();
                    if (foundObj != null && foundObj.Count > 0)
                        return (from y in foundObj orderby y.Inspection_Started descending select y).First();
                }
            } catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
            }
            return null; 
        }

        public void CompleteFabricJob(int JobId, string comments = "", bool ThreadColorConfirm = true)
        {
            using (Inspection _db = new Models.EF.Inspection())
            {
                var RollYards = (from x in _db.WeaverProductions where x.JobSummaryId == JobId select x.Yards).Sum();

                var JobRec = (from x in _db.InspectionJobSummaries where x.id == JobId select x).FirstOrDefault(); 

                if (JobRec != null)
                {
                    JobRec.TotalInspectedItems = Convert.ToInt32(RollYards);
                    JobRec.Inspection_Finished = DateTime.Now;
                    JobRec.MajorsCount = (from x in _db.DefectMasters where x.InspectionJobSummaryId == JobId select x).Count();
                    JobRec.MinorsCount = 0;
                    JobRec.ScrapCount = 0;
                    JobRec.UserConfirm_PassFail = true;
                    JobRec.UserConfirm_PassFail_Timestamp = DateTime.Now;
                    JobRec.Technical_PassFail = true;
                    JobRec.Technical_PassFail_Timestamp = DateTime.Now;
                    JobRec.Comments = comments;
                    JobRec.ThreadColorConfirm = ThreadColorConfirm;
                    _db.SaveChanges();
                 
                    try
                    {
                        sendAlertEmail(JobRec);
                    } catch(Exception ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex); 
                    }
                        
                }
            }
        }

        private void sendAlertEmail(InspectionJobSummary job)
        {
            Helpers.EmailService emailservice = new Helpers.EmailService();
            //List<string> destinations = new List<string>();
            //destinations.Add("jsucco@standardtextile.com");
            //destinations.Add("aheiman@standardtextile.com");
            //destinations.Add("tbopp@standardtextile.com");
            //destinations.Add("njohnson@standardtextile.com"); 
            string[] destinations = emailservice.GetRollUsers(); 

            Helpers.InspectionService service = new Helpers.InspectionService();

            if (job.id == 0)
                throw new Exception("Job id cannot be zero."); 

            RollRM_Xref rmxref = service.getRMObject(job.RM_XrefId);

            if (rmxref == null || rmxref.Id == 0)
                throw new Exception("Thread color not found in RM reference table.");

            if (rmxref.IDThreadColor == job.ThreadColor)
                return; 

            foreach (var item in destinations)
            {
                Helpers.Email alert = new Helpers.Email();

                alert.toaddress = item.Trim();
                alert.fromaddress = "apr@standardtextile.com";
                alert.isBodyHtml = true;
                alert.subject = "ALERT - Roll Thread Color mismatch.";
                alert.body = "Inspector " + job.EmployeeNo + " indicated on Inspection Id: " + job.id + " had the wrong thread Color. <br/><br/>Inspector Indicated Color: " + job.ThreadColor + "<br/>RM refernced Color: " + rmxref.IDThreadColor;

                emailservice.sendAsync(alert); 
            }

        }

        private object ConvertType(object Obj, string TypeName)
        {
            object returnobj = new object();
            switch (TypeName.ToUpper())
            {
                case "STRING":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToString(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = "";
                    }
                    break;
                case "INT32":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToInt32(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = 0;
                    }
                    break;
                case "INTEGER":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToInt32(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = 0;
                    }
                    break;
                case "INT64":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToInt64(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = 0;
                    }
                    break;
                case "BOOL":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToBoolean(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = false;
                    }
                    break;
                case "BOOLEAN":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToBoolean(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = false;
                    }
                    break;
                case "DECIMAL":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToDecimal(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = 0;
                    }
                    break;
                case "DATETIME":
                    try
                    {
                        if (Obj == null)
                            throw new Exception("No Nulls");
                        returnobj = Convert.ToDateTime(Obj);
                    }
                    catch (Exception e)
                    {
                        returnobj = new DateTime(1900, 1, 1);
                    }
                    break;

            }
            return returnobj;
        }

        public class Type
        {
            public bool fabric { get; set; }
        }
    }
}
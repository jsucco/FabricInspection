using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspection_mvc.Models.EF;
using System.Threading.Tasks;

namespace Inspection_mvc.Helpers
{
    public class Defect
    {
        private InspectionJobSummary currentJob = null;
        private DefectMaster newDefect = new DefectMaster();
        public int WeaverShiftId = 0; 

        public Defect(InspectionJobSummary JobToLoad)
        {
            if (JobToLoad == null)
                throw new Exception("Job To Load cannot be null.");
            currentJob = JobToLoad;
        }

        private void SetDefectFields()
        {
            newDefect.InspectionJobSummaryId = currentJob.id;
            newDefect.DataNo = currentJob.DataNo;
            newDefect.Inspector = HttpContext.Current.User.Identity.Name;
            newDefect.InspectionState = currentJob.JobType;
            newDefect.TemplateId = currentJob.TemplateId;
            newDefect.Tablet = "Browser";
            newDefect.DefectClass = "MAJOR";
            newDefect.Location = "578";
            newDefect.DataType = "Defect";
            newDefect.EmployeeNo = HttpContext.Current.User.Identity.Name; 
        }

        public string Insert(Models.EF.DefectMaster defect)
        {

            if (defect != null)
                newDefect = defect;
            SetDefectFields();
            validateFields();
            Task.Run(() => InsertDefect(defect));
            return "true_Last Defect Chosen: " + defect.DefectDesc;
        }

        public string InsertWText(Models.EF.DefectMaster defect)
        {
            string text = "";
            if (defect != null)
                newDefect = defect;
            SetDefectFields();
            validateFields();
            using (Inspection _db = new Inspection())
            {
                defect.DefectTime = DateTime.Now;
                _db.DefectMasters.Add(defect);
                _db.SaveChanges();
                text = "Last Defect Chosen: " + defect.DefectDesc; 
            }
            return text; 
        }

        private void InsertDefect(DefectMaster defect)
        {
            using (Inspection _db = new Inspection())
            {
                defect.DefectTime = DateTime.Now; 
                _db.DefectMasters.Add(defect);
                int rows = _db.SaveChanges(); 
            }
        }
        
        public string UndoLast()
        {
            string desc = "";

            if (currentJob.id == 0)
                throw new Exception("undo error: cannot delete defect with out a job defined");

            try
            {
                using (Inspection _db = new Inspection())
                {
                    var rec = (from x in _db.DefectMasters where x.InspectionJobSummaryId == currentJob.id && x.WeaverShiftId == WeaverShiftId orderby x.DefectTime descending select x).Take(2).ToList();

                    if (rec != null && rec.Count > 0)
                    {
                        var DefArr = (from x in rec orderby x.DefectTime descending select x).ToArray();
                        _db.DefectMasters.Remove(DefArr[0]);
                        int rows = _db.SaveChanges();
                        if (rows > 0)
                        {
                            
                            if (DefArr.Length > 1)
                                desc = DefArr[0].ButtonTemplateId.ToString() + "_Last Defect Chosen: " + DefArr[1].DefectDesc;
                            else
                                desc = "NA"; 
                        }                        
                        else
                            throw new Exception("unknown error deleting defect.");
                    }
                    else
                        desc = "NA";
                }
            } catch(Exception ex)
            {
                desc = ex.Message; 
            }
            
            return desc;
        }

        private void validateFields()
        {
            if (currentJob == null)
                throw new Exception("Job Summary obj cannot be null");

            if (newDefect == null)
                throw new Exception("Defect cannot be null.");

            if (newDefect.InspectionJobSummaryId < 1)
                throw new Exception("Defect must have Job SummaryId greater than zero.");

            if (newDefect.DataNo.Length == 0)
                throw new Exception("Defect must have a DataNo.");
            
            if (currentJob.JobType == "FABRIC")
            {
                if (newDefect.WeaverShiftId == 0)
                    throw new Exception("FABRIC Defect must have weavershift Id greater than 0.");
            }

            if (newDefect.ButtonTemplateId == 0) 
                throw new Exception("Defect must have buttontemplateId greater than 0.");

            if (newDefect.DefectDesc.Length == 0)
                throw new Exception("Defect must have a defect desciption.");

            if (newDefect.Inspector.Length == 0)
                throw new Exception("Defect must have an inspector.");

            if (newDefect.TemplateId == 0)
                throw new Exception("Defect must have a template selected.");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Inspection_mvc.Models.EF;
using System.Reflection;
using System.Threading.Tasks;

namespace Inspection_mvc.Helpers
{
    public class Shift
    {
        public EndShiftModel model = new EndShiftModel(); 

        public Shift(ICollection<Helpers.InputObject> inputs)
        {
            if (inputs == null || inputs.Count == 0)
                return;

            foreach (var item in inputs)
            {
                PropertyInfo shiftField = model.GetType().GetProperty(item.id);
                if (shiftField != null)
                    shiftField.SetValue(model, ConvertType(item.value, shiftField.PropertyType.Name)); 
            }
        }

        public void StartWeaverShift(InspectionJobSummary job = null)
        {
            if (job != null)
                model.JobId = job.id;

            if (model.JobId < 1)
                throw new Exception("Shift requires an inspection job that is already started.");

            if (model.Weaver1.Trim().Length == 0)
                throw new Exception("At Least one weaver must be selected.");

            using (Inspection _db = new Inspection())
            {
                WeaverShift newShift = new WeaverShift();

                newShift.Start = DateTime.Now;
                newShift.Shift = 1;
                _db.WeaverShifts.Add(newShift);

                int rows = _db.SaveChanges();

                if (rows > 0)
                {

                    if (model.Weaver1.Trim().Length > 0)
                    {
                        WeaverProduction newProductionJob = new WeaverProduction();
                        newProductionJob.OEOP_ADP = model.Weaver1;
                        newProductionJob.JobSummaryId = model.JobId;
                        newProductionJob.Yards = 0;
                        newProductionJob.ShiftId = newShift.Id;
                        _db.WeaverProductions.Add(newProductionJob);
                    }
                    if (model.Weaver2.Trim().Length > 0)
                    {
                        WeaverProduction newProductionJob = new WeaverProduction();
                        newProductionJob.OEOP_ADP = model.Weaver2;
                        newProductionJob.JobSummaryId = model.JobId;
                        newProductionJob.Yards = 0;
                        newProductionJob.ShiftId = newShift.Id;
                        _db.WeaverProductions.Add(newProductionJob);
                    }
                    _db.SaveChanges();
                }
                model.WeaverShiftId = newShift.Id;
            }
        }

        public void EndShift(EndShiftModel shiftInfo = null)
        {
            if (shiftInfo != null)
                model = shiftInfo;

            if (model == null)
                throw new Exception("Shift Model cannot be null.");

            if (model.JobId < 1)
                throw new Exception("JobId needs to be greater than one.");

            if (model.YardsInspected < 1)
                throw new Exception("Yards must be greater than zero.");

            if (model.WeaverShiftId < 1)
                throw new Exception("Error setting WeaverShiftId.");
            UpdateShiftTables(model);
           // Task.Run(() => UpdateShiftTables(model));

        }

        private void UpdateShiftTables(EndShiftModel shiftInfo)
        {
            try
            {
                if (shiftInfo != null && shiftInfo.JobId > 0)
                    updateConfirm(shiftInfo.JobId, shiftInfo.ThreadColorConfirm); 
                using (Inspection _db = new Inspection())
                {
                    var WeaverShift = (from x in _db.WeaverShifts where x.Id == shiftInfo.WeaverShiftId select x).FirstOrDefault();

                    if (WeaverShift != null)
                    {
                        WeaverShift.Finish = DateTime.Now;
                        WeaverShift.Comments = shiftInfo.Comments; 
                        _db.SaveChanges();
                    }
                    else
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("no weaver shift found."));

                    var WeaverProduction = (from x in _db.WeaverProductions where x.ShiftId == shiftInfo.WeaverShiftId select x).ToList();
                    

                    if (WeaverProduction != null && WeaverProduction.Count > 0)
                    {
                        WeaverProduction[] prodArr = WeaverProduction.ToArray();
                        int jobid = prodArr[0].JobSummaryId;
                        var RM = (from x in _db.InspectionJobSummaries where x.id == jobid select x).FirstOrDefault();
                        var AdjustedYards = Convert.ToDecimal(shiftInfo.YardsInspected); 
                        if (RM != null)
                        {
                            int rmid = (int)RM.RM_XrefId;
                            AdjustedYards = Helpers.Conversions.ToFabricYard(rmid, AdjustedYards); 
                        }

                        foreach (var item in WeaverProduction)
                        {
                            item.Yards = AdjustedYards;                        
                        }
                        
                        _db.SaveChanges();
                        
                    }
                    else
                        Elmah.ErrorSignal.FromCurrentContext().Raise(new Exception("no WeaverProduction Record found.")); 
                }
            } catch (Exception e)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(e);
            } 
        }

        private void updateConfirm(int jobid, bool confirm = true)
        {
            try
            {
                using (var _db = new Inspection())
                {
                    var RM = (from x in _db.InspectionJobSummaries where x.id == jobid select x).FirstOrDefault();
                    RM.ThreadColorConfirm = confirm;
                    _db.SaveChanges();
                }
            } catch (Exception ex)
            {

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
    }

    public class EndShiftModel
    {
        public int YardsInspected { get; set; }
        public string Comments { get; set; }
        public int JobId { get; set; }
        public int WeaverShiftId { get; set; }
        public string Weaver1 { get; set; }
        public string Weaver2 { get; set; }
        public bool ThreadColorConfirm { get; set; }
    }
}
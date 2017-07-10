using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Inspection_mvc.Models.EF;
using System;

namespace Inspection_mvc.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class EmployeeViewModel
    {
        public List<Models.EF.EmployeeNo> employees { get; set; }
        public jqgridData gridObj { get; set; }
    }

    public class RMViewModel
    {
        public IList<RollRM_Xref> records { get; set; }
    }
    public class RollByYardsViewModel
    {
        public int TemplateId { get; set; }
        public IList<TabTemplate> tabs { get; set; }
        public IList<ButtonCollection> buttons { get; set; }
    }

    public class DefectEntryViewModel
    {
        public int TemplateId { get; set; }
        public IList<TabTemplate> tabs { get; set; }
        public IList<ButtonCollection> buttons { get; set; }
        public ICollection<Helpers.InputObject> RequiredInputs { get; set; }
        public InspectionJobSummary currentJob { get; set; }
        public int currentShiftId { get; set; }
    }

    public class WeaverEntryViewModel
    {
        public IList<Helpers.PageInput> inputs { get; set; }
        public ICollection<Helpers.InputObject> RequiredInputs { get; set; }
        public int JobId { get; set; }
        public int LastWeaverShiftId { get; set; }
    }

    public class EndShiftViewModel
    {
        public IList<Helpers.PageInput> inputs { get; set; }
        public int JobId { get; set; }
        public int WeaverShiftId { get; set; }
    }

    public class InfoEntryViewModel
    {
        public List<Helpers.PageInput> inputs { get; set; }
        public List<RollRM_Xref> rmtable { get; set; }
    }
    public class RollCompleteViewModel
    {
        public List<Helpers.PageInput> inputs { get; set; }
        public int JobId { get; set; }
        public int WeaverShiftId { get; set; }
    }

    public class EndViewModel
    {
        public int JobId { get; set; }
        public int LastWeaverShiftId { get; set; }
        public Helpers.InputObject threadconfirm { get; set; }
        public string Comments { get; set; }
    }

    public class ButtonCollection
    {
        public int TabTemplateId { get; set; }
        public int ButtonId { get; set; }
        public string Name { get; set; }
        public int TabNumber { get; set; }
        public int TemplateId { get; set; }
        public string ButtonName { get; set; }
        public bool ProductSpecs { get; set; }
        public string DefectType { get; set; }
        public int id { get; set; }
        public string DefectCode { get; set; }
        public bool Hide { get; set; }
        public int ButtonLibraryId { get; set; }
        public bool Timer { get; set; }
        public string text { get; set; }
        public int ButtonTemplateId { get; set; }
    }
    public class jqgridData
    {
        public int total = 0;
        public int page = 0;
        public int records;
        public object userdata;
        public object rows;
    }
    public class jqgridLoad
    {
        public int rows;
        public int PageCnt;
        public bool paging;
        public string Filter1;
        public bool FilterFlag;
    }

    public class SourceCrud
    {
        public int Id;
        public string oper;
        public string Name;
        public int EmployeeId;
        public int realRow;
        public string FirstName;
        public string LastName;
        public string Initials;
    }

    public class RMCrud
    {
        public int Id;
        public string oper;
        public string RMin;
        public string RMout;
        public bool IDThread;
        public string IDThreadColor;
        public decimal YardCoefficient; 
    }

    public class InspectionJobSummaryIns
    {

        public int id;

        [StringLength(20)]
        public string JobType;

        [StringLength(30)]
        public string JobNumber;

        [StringLength(50)]
        public string DataNo;

        [StringLength(10)]
        public string CID;

        public int TemplateId;

        public int ItemPassCount;

        public int ItemFailCount;

        public int? WOQuantity;

        public int? WorkOrderPieces;

        public decimal? AQL_Level;

        [Required]
        [StringLength(15)]
        public string Standard;

        public int? SampleSize;

        public int? TotalInspectedItems;

        public int? RejectLimiter;

        public bool? Technical_PassFail;

        public DateTime? Technical_PassFail_Timestamp;

        public bool? UserConfirm_PassFail;

        public DateTime? UserConfirm_PassFail_Timestamp;

        public DateTime? Inspection_Started;

        public DateTime? Inspection_Finished;

        [StringLength(50)]
        public string PRP_Code;

        public double? UnitCost;

        public string UnitDesc;

        public string Comments;

        [StringLength(50)]
        public string ProdMachineName;

        public int? MajorsCount;

        public int? MinorsCount;

        public int? RepairsCount;

        public int? ScrapCount;

        [StringLength(50)]
        public string EmployeeNo;

        [StringLength(50)]
        public string CasePack;

        [StringLength(10)]
        public string WorkRoom;

    }

    public class fabricReport
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public DateTime FROMDATE { get; set; }
        public DateTime TODATE { get; set; }
        public string Yards_Inspected { get; set; }
        public string DefectCount { get; set; }
        public string DefectPercentage { get; set; }
    }

    public class fabricReport_new
    {
        public string OEOP_Operator { get; set; }
        public DateTime FROMDATE { get; set; }
        public DateTime TODATE { get; set; }
        public string Yards_Pieces_Inspected { get; set; }
        public string DefectCount { get; set; }
        public string DefectPercentage { get; set; }
    }
}
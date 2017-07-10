namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Inspection : DbContext
    {
        public Inspection()
            : base("name=Inspection")
        {
        }

        public virtual DbSet<ButtonLibrary> ButtonLibraries { get; set; }
        public virtual DbSet<ButtonTemplate> ButtonTemplates { get; set; }
        public virtual DbSet<DefectMaster> DefectMasters { get; set; }
        public virtual DbSet<DefectTimer> DefectTimers { get; set; }
        public virtual DbSet<EmployeeNo> EmployeeNoes { get; set; }
        public virtual DbSet<InspectionCompliance> InspectionCompliances { get; set; }
        public virtual DbSet<InspectionJobSummary> InspectionJobSummaries { get; set; }
        public virtual DbSet<InspectionType> InspectionTypes { get; set; }
        public virtual DbSet<ProductSpecification> ProductSpecifications { get; set; }
        public virtual DbSet<RollInspectionDetail> RollInspectionDetails { get; set; }
        public virtual DbSet<RollInspectionSummary> RollInspectionSummaries { get; set; }
        public virtual DbSet<SpecMeasurement> SpecMeasurements { get; set; }
        public virtual DbSet<TabTemplate> TabTemplates { get; set; }
        public virtual DbSet<TemplateActivator> TemplateActivators { get; set; }
        public virtual DbSet<TemplateName> TemplateNames { get; set; }
        public virtual DbSet<WeaverProduction> WeaverProductions { get; set; }
        public virtual DbSet<WeaverShift> WeaverShifts { get; set; }
        public virtual DbSet<INS_Summary_VW> INS_Summary_VW { get; set; }
        public virtual DbSet<LocationMaster_VW> LocationMaster_VW { get; set; }
        public virtual DbSet<OperatorShiftSummary> OperatorShiftSummaries { get; set; }
        public virtual DbSet<STT_VW> STT_VW { get; set; }
        public virtual DbSet<WeaverShiftSummary> WeaverShiftSummaries { get; set; }
        public virtual DbSet<WeaverWeightedDefect> WeaverWeightedDefects { get; set; }
        public virtual DbSet<RollRM_Xref> RollRM_Xref { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ButtonLibrary>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ButtonLibrary>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<ButtonLibrary>()
                .Property(e => e.DefectImage_Desc)
                .IsUnicode(false);

            modelBuilder.Entity<ButtonLibrary>()
                .HasMany(e => e.ButtonTemplates)
                .WithRequired(e => e.ButtonLibrary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ButtonTemplate>()
                .Property(e => e.DefectType)
                .IsUnicode(false);

            modelBuilder.Entity<ButtonTemplate>()
                .HasMany(e => e.DefectTimers)
                .WithRequired(e => e.ButtonTemplate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.DefectDesc)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.POnumber)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.DataNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.EmployeeNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.AQL)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.ThisPieceNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.SampleSize)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.TotalLotPieces)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Product)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.DefectClass)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Tablet)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.WorkOrder)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.LotNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Dimensions)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.LoomNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.DefectPoints)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.GriegeNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.RollNo)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Operation)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.Inspector)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.ItemNumber)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.InspectionState)
                .IsUnicode(false);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.CasePackConv)
                .HasPrecision(13, 2);

            modelBuilder.Entity<DefectMaster>()
                .Property(e => e.WorkRoom)
                .IsFixedLength();

            modelBuilder.Entity<DefectMaster>()
                .HasMany(e => e.DefectTimers)
                .WithRequired(e => e.DefectMaster)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DefectTimer>()
                .Property(e => e.StatusValue)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeNo>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeNo>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeNo>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<EmployeeNo>()
                .Property(e => e.CID)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeNo>()
                .Property(e => e.Type)
                .IsFixedLength();

            modelBuilder.Entity<EmployeeNo>()
                .HasMany(e => e.WeaverProductions)
                .WithRequired(e => e.EmployeeNo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionCompliance>()
                .Property(e => e.DataNo)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionCompliance>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionCompliance>()
                .Property(e => e.WAUPMJ)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionCompliance>()
                .Property(e => e.WADCTO)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionCompliance>()
                .Property(e => e.WASRST)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionCompliance>()
                .Property(e => e.Prp_Code)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.JobType)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.JobNumber)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.CID)
                .IsFixedLength();

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.AQL_Level)
                .HasPrecision(18, 1);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.Standard)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.PRP_Code)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.UnitDesc)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.ProdMachineName)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.EmployeeNo)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.CasePack)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.WorkRoom)
                .IsFixedLength();

            modelBuilder.Entity<InspectionJobSummary>()
                .HasMany(e => e.DefectTimers)
                .WithRequired(e => e.InspectionJobSummary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .Property(e => e.ThreadColor)
                .IsUnicode(false);

            modelBuilder.Entity<InspectionJobSummary>()
                .HasMany(e => e.WeaverProductions)
                .WithRequired(e => e.InspectionJobSummary)
                .HasForeignKey(e => e.JobSummaryId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductSpecification>()
                .Property(e => e.DataNo)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSpecification>()
                .Property(e => e.Spec_Description)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSpecification>()
                .Property(e => e.HowTo)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSpecification>()
                .Property(e => e.ME_SessionID)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSpecification>()
                .Property(e => e.SpecSource)
                .IsUnicode(false);

            modelBuilder.Entity<ProductSpecification>()
                .HasMany(e => e.SpecMeasurements)
                .WithRequired(e => e.ProductSpecification)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RollInspectionSummary>()
                .Property(e => e.LoomNo)
                .IsUnicode(false);

            modelBuilder.Entity<RollInspectionSummary>()
                .Property(e => e.RollNumber)
                .IsUnicode(false);

            modelBuilder.Entity<RollInspectionSummary>()
                .Property(e => e.Yards_Inspected)
                .HasPrecision(18, 1);

            modelBuilder.Entity<RollInspectionSummary>()
                .HasMany(e => e.RollInspectionDetails)
                .WithRequired(e => e.RollInspectionSummary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TabTemplate>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TabTemplate>()
                .HasMany(e => e.ButtonTemplates)
                .WithRequired(e => e.TabTemplate)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TemplateName>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<TemplateName>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<TemplateName>()
                .Property(e => e.LineType)
                .IsUnicode(false);

            modelBuilder.Entity<TemplateName>()
                .HasMany(e => e.DefectMasters)
                .WithRequired(e => e.TemplateName)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TemplateName>()
                .HasMany(e => e.RollInspectionDetails)
                .WithRequired(e => e.TemplateName)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TemplateName>()
                .HasMany(e => e.TabTemplates)
                .WithRequired(e => e.TemplateName)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TemplateName>()
                .HasMany(e => e.TemplateActivators)
                .WithRequired(e => e.TemplateName)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WeaverShift>()
                .HasMany(e => e.WeaverProductions)
                .WithRequired(e => e.WeaverShift)
                .HasForeignKey(e => e.ShiftId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.JobType)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.JobNumber)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.LOCCID)
                .IsFixedLength();

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.LOCName)
                .IsFixedLength();

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.TMPName)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.AQL_Level)
                .HasPrecision(18, 1);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Standard)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.UnitDesc)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.ProdMachineName)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.DefectDesc)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.POnumber)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.DataNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.EmployeeNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.ThisPieceNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.SampleSize)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.TotalLotPieces)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Product)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.DefectClass)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Tablet)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.WorkOrder)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.LotNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Location)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.DataType)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Dimensions)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Comment)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.LoomNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.DefectPoints)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.GriegeNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.RollNo)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Operation)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Inspector)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.ItemNumber)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.InspectionState)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.CasePackConv)
                .HasPrecision(13, 2);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.WorkRoom)
                .IsFixedLength();

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Owner)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.LineType)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.Abreviation)
                .IsUnicode(false);

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.DBname)
                .IsFixedLength();

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.CID)
                .IsFixedLength();

            modelBuilder.Entity<INS_Summary_VW>()
                .Property(e => e.ConnectionString)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster_VW>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster_VW>()
                .Property(e => e.Abreviation)
                .IsUnicode(false);

            modelBuilder.Entity<LocationMaster_VW>()
                .Property(e => e.DBname)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster_VW>()
                .Property(e => e.CID)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster_VW>()
                .Property(e => e.ConnectionString)
                .IsFixedLength();

            modelBuilder.Entity<OperatorShiftSummary>()
            .Property(e => e.LoomNumber)
            .IsUnicode(false);


            modelBuilder.Entity<OperatorShiftSummary>()
                .Property(e => e.ThreadColor)
                .IsUnicode(false);

            modelBuilder.Entity<OperatorShiftSummary>()
                .Property(e => e.Comments)
                .IsUnicode(false);

            modelBuilder.Entity<OperatorShiftSummary>()
                .Property(e => e.Yards_Pieces)
                .HasPrecision(38, 2);

            modelBuilder.Entity<OperatorShiftSummary>()
                .Property(e => e.Weight8)
                .HasPrecision(1, 1);

            modelBuilder.Entity<OperatorShiftSummary>()
                .Property(e => e.total)
                .HasPrecision(14, 1);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.Machine)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.WorkOrder)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.OperatorNo)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.DataNo)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.GreigeNo)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.CutLengthSpec)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.FinishLength)
                .HasPrecision(13, 2);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.ScheduledTime)
                .HasPrecision(13, 2);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.Efficiency)
                .HasPrecision(13, 3);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.AvgSheetsPerHour)
                .HasPrecision(13, 2);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.RunPass)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.DIFF_PERC)
                .HasPrecision(10, 2);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.WorkOrderID)
                .IsUnicode(false);

            modelBuilder.Entity<STT_VW>()
                .Property(e => e.Location)
                .IsFixedLength();

            modelBuilder.Entity<WeaverShiftSummary>()
             .Property(e => e.LoomNumber)
             .IsUnicode(false);

            modelBuilder.Entity<WeaverShiftSummary>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<WeaverShiftSummary>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<WeaverShiftSummary>()
                .Property(e => e.Yards)
                .HasPrecision(38, 2);

            modelBuilder.Entity<WeaverShiftSummary>()
                .Property(e => e.Weight8)
                .HasPrecision(1, 1);

            modelBuilder.Entity<WeaverShiftSummary>()
                .Property(e => e.total)
                .HasPrecision(14, 1);

            modelBuilder.Entity<WeaverWeightedDefect>()
                .Property(e => e.Firstname)
                .IsUnicode(false);

            modelBuilder.Entity<WeaverWeightedDefect>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<WeaverWeightedDefect>()
                .Property(e => e.Yards)
                .HasPrecision(38, 2);

            modelBuilder.Entity<WeaverWeightedDefect>()
                .Property(e => e.Weight8)
                .HasPrecision(1, 1);

            modelBuilder.Entity<WeaverWeightedDefect>()
                .Property(e => e.total)
                .HasPrecision(14, 1);

            modelBuilder.Entity<RollRM_Xref>()
                .Property(e => e.RMin)
                .IsUnicode(false);

            modelBuilder.Entity<RollRM_Xref>()
                .Property(e => e.RMout)
                .IsUnicode(false);

            modelBuilder.Entity<RollRM_Xref>()
                .Property(e => e.IDThreadColor)
                .IsUnicode(false);
        }
    }
}

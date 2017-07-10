namespace LoadConversions.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class InspectionContext : DbContext
    {
        public InspectionContext()
            : base("name=InspectionContext")
        {
        }
        public virtual DbSet<RollRM_Xref> RollRM_Xref { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

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

namespace Inspection_mvc.Models.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AprManager : DbContext
    {
        public AprManager()
            : base("name=AprManager")
        {
        }

        public virtual DbSet<EmailMaster> EmailMasters { get; set; }
        public virtual DbSet<LinkedServerMaster> LinkedServerMasters { get; set; }
        public virtual DbSet<LocationMaster> LocationMasters { get; set; }
        public virtual DbSet<OPCMaster> OPCMasters { get; set; }
        public virtual DbSet<SPC_ApplicationLog> SPC_ApplicationLog { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public virtual DbSet<UserCrudActivityLog> UserCrudActivityLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailMaster>()
                .Property(e => e.HomeLocation)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<LinkedServerMaster>()
                .Property(e => e.DSN_Type)
                .IsUnicode(false);

            modelBuilder.Entity<LinkedServerMaster>()
                .Property(e => e.LinkedServerName)
                .IsUnicode(false);

            modelBuilder.Entity<LocationMaster>()
                .Property(e => e.Name)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster>()
                .Property(e => e.Abreviation)
                .IsUnicode(false);

            modelBuilder.Entity<LocationMaster>()
                .Property(e => e.DBname)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster>()
                .Property(e => e.CID)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster>()
                .Property(e => e.ConnectionString)
                .IsFixedLength();

            modelBuilder.Entity<LocationMaster>()
                .HasMany(e => e.LinkedServerMasters)
                .WithRequired(e => e.LocationMaster)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LocationMaster>()
                .HasMany(e => e.OPCMasters)
                .WithRequired(e => e.LocationMaster)
                .HasForeignKey(e => e.LocationId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SPC_ApplicationLog>()
                .Property(e => e.type)
                .IsUnicode(false);

            modelBuilder.Entity<UserActivityLog>()
                .Property(e => e.DBOrigin)
                .IsUnicode(false);

            modelBuilder.Entity<UserActivityLog>()
                .Property(e => e.UserID)
                .IsUnicode(false);

            modelBuilder.Entity<UserActivityLog>()
                .Property(e => e.DeviceType)
                .IsUnicode(false);

            modelBuilder.Entity<UserActivityLog>()
                .Property(e => e.CID)
                .IsUnicode(false);

            modelBuilder.Entity<UserActivityLog>()
                .HasMany(e => e.UserCrudActivityLogs)
                .WithRequired(e => e.UserActivityLog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserCrudActivityLog>()
                .Property(e => e.oper)
                .IsUnicode(false);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infrastructure.bcs.affairs.Entities
{
    public partial class AffairsContext : DbContext
    {
        public AffairsContext()
        {
        }

        public AffairsContext(DbContextOptions<AffairsContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("basedConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Departments>(entity => {
            //    entity.ToTable("Departments", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});
            //modelBuilder.Entity<Projects>(entity => {
            //    entity.ToTable("Projects", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});
            //modelBuilder.Entity<RequisitionTypes>(entity => {
            //    entity.ToTable("RequisitionTypes", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});
            //modelBuilder.Entity<UserTypes>(entity => {
            //    entity.ToTable("UserTypes", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});
            //modelBuilder.Entity<RequisitionAttachments>(entity => {
            //    entity.ToTable("RequisitionAttachments", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});
            //modelBuilder.Entity<Roles>(entity => {
            //    entity.ToTable("Roles", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});

            //modelBuilder.Entity<Companies>(entity => {
            //    entity.ToTable("Companies", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});

            //modelBuilder.Entity<Organisations>(entity => {
            //    entity.ToTable("Organisations", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});

            //modelBuilder.Entity<Suppliers>(entity => {
            //    entity.ToTable("Suppliers", "Sapphire");
            //    entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            //});

            //modelBuilder.Entity<Currencies>().ToTable("Currencies", "Sapphire");
            //modelBuilder.Entity<Requisitions>().ToTable("Requisitions", "Sapphire");
            //modelBuilder.Entity<SupplierUsers>().ToTable("SupplierUsers", "Sapphire");
            //modelBuilder.Entity<Users>().ToTable("Users", "Sapphire");

            ////views
            //modelBuilder.Entity<vwUsers>(entity => { entity.ToView("vwUsers", "Sapphire"); });
            //modelBuilder.Entity<vwRequisitions>(entity => { entity.ToView("vwRequisitions", "Sapphire"); });

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

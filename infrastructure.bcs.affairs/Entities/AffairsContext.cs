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

        public AffairsContext(DbContextOptions<AffairsContext> options) : base(options)  {  }

        public DbSet<BethelTypes> BethelType { get; set; }
        public DbSet<Bethels> Bethel { get; set; }
        public DbSet<Countries> Country { get; set; }
        public DbSet<Departments> Department { get; set; }


        #region view
        public DbSet<BethelLists> BethelList { get; set; }
        #endregion 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("basedConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BethelTypes>(entity =>
            {
                entity.ToTable("BethelTypes", "Registration");
                entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            });
            modelBuilder.Entity<Departments>(entity =>
            {
                entity.ToTable("Departments", "Manager");
                entity.Property(e => e.Id).UseIdentityColumn(); entity.HasKey(o => o.Id);
            });
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

            modelBuilder.Entity<Countries>().ToTable("Countries", "Manager");
            modelBuilder.Entity<Bethels>().ToTable("Bethels", "Registration");
            //modelBuilder.Entity<SupplierUsers>().ToTable("SupplierUsers", "Sapphire");
            //modelBuilder.Entity<Users>().ToTable("Users", "Sapphire");

            ////views
            modelBuilder.Entity<BethelLists>(entity => { entity.ToView("BethelLists", "Registration"); });
            //modelBuilder.Entity<vwRequisitions>(entity => { entity.ToView("vwRequisitions", "Sapphire"); });

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

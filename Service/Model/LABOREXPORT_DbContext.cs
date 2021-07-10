using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Service.Model
{
    public partial class LABOREXPORT_DbContext : DbContext
    {
        public LABOREXPORT_DbContext()
            : base("name=LABOREXPORT_DbContext")
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Company>()
                .Property(e => e.CityCode)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserRoles)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.DepartmentCode)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CompanyCode)
                .IsUnicode(false);
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace PROYECTO_ISO810.Models
{
    public partial class EmpleadoModel : DbContext
    {
        public EmpleadoModel()
            : base("name=EmpleadoModel")
        {
        }

        public virtual DbSet<Empleados> Empleados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>()
                .Property(e => e.NombreCompleto)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Cedula)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Departamento)
                .IsUnicode(false);

            modelBuilder.Entity<Empleados>()
                .Property(e => e.Cargo)
                .IsUnicode(false);
        }
    }
}

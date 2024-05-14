namespace PROYECTO_ISO810.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Empleados
    {
        [Key]
        public int IDEmpleado { get; set; }

        [StringLength(100)]
        public string NombreCompleto { get; set; }

        [StringLength(16)]
        public string Cedula { get; set; }

        [StringLength(50)]
        public string Departamento { get; set; }

        [StringLength(50)]
        public string Cargo { get; set; }

        public decimal? Salario { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaDeInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaDeNacimiento { get; set; }
    }
}

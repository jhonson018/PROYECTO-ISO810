namespace PROYECTO_ISO810.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmpleadosModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        IDEmpleado = c.Int(nullable: false, identity: true),
                        NombreCompleto = c.String(maxLength: 100, unicode: false),
                        Cedula = c.String(maxLength: 16, unicode: false),
                        Departamento = c.String(maxLength: 50, unicode: false),
                        Cargo = c.String(maxLength: 50, unicode: false),
                        Salario = c.Decimal(precision: 18, scale: 2),
                        FechaDeInicio = c.DateTime(storeType: "date"),
                        FechaDeNacimiento = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.IDEmpleado);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Empleados");
        }
    }
}

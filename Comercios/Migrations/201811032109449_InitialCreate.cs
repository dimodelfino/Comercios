namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        fechaRealizacion = c.DateTime(nullable: false),
                        total = c.Double(nullable: false),
                        cantidadProductos = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 80),
                        descripcion = c.String(nullable: false, maxLength: 450),
                        costo = c.Double(nullable: false),
                        precioSugerido = c.Double(nullable: false),
                        tiempoPrevisto = c.Int(nullable: false),
                        paisOrigen = c.String(nullable: false, maxLength: 80),
                        cantidadMinima = c.Int(nullable: false),
                        pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pedidoes", t => t.pedido_Id)
                .Index(t => t.pedido_Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 80),
                        contrasena = c.String(nullable: false, maxLength: 20),
                        email = c.String(nullable: false, maxLength: 80),
                        rol = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productoes", "pedido_Id", "dbo.Pedidoes");
            DropIndex("dbo.Usuarios", new[] { "email" });
            DropIndex("dbo.Productoes", new[] { "pedido_Id" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Productoes");
            DropTable("dbo.Pedidoes");
        }
    }
}

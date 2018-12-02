namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
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
                        usuario_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuarios", t => t.usuario_Id)
                .Index(t => t.usuario_Id);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        cantidad = c.Int(nullable: false),
                        costoItem = c.Double(nullable: false),
                        producto_Id = c.Int(),
                        Pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Productoes", t => t.producto_Id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id)
                .Index(t => t.producto_Id)
                .Index(t => t.Pedido_Id);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 80),
                        descripcion = c.String(nullable: false, maxLength: 450),
                        costo = c.Double(nullable: false),
                        precioSugerido = c.Double(nullable: false),
                        esFabircado = c.Boolean(nullable: false),
                        tiempoPrevisto = c.Int(),
                        paisOrigen = c.String(maxLength: 80),
                        cantidadMinima = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false, maxLength: 80),
                        contrasena = c.String(nullable: false, maxLength: 20),
                        email = c.String(nullable: false, maxLength: 80),
                        rol = c.String(),
                        fechaRegistro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Items", "Pedido_Id", "dbo.Pedidoes");
            DropForeignKey("dbo.Items", "producto_Id", "dbo.Productoes");
            DropIndex("dbo.Usuarios", new[] { "email" });
            DropIndex("dbo.Items", new[] { "Pedido_Id" });
            DropIndex("dbo.Items", new[] { "producto_Id" });
            DropIndex("dbo.Pedidoes", new[] { "usuario_Id" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Productoes");
            DropTable("dbo.Items");
            DropTable("dbo.Pedidoes");
        }
    }
}

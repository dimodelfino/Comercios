namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Carrito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        idProducto = c.Int(nullable: false),
                        idPedido = c.Int(nullable: false),
                        cantidad = c.Int(nullable: false),
                        Pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id)
                .Index(t => t.Pedido_Id);
            
            DropColumn("dbo.Pedidoes", "cantidadProductos");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pedidoes", "cantidadProductos", c => c.Int(nullable: false));
            DropForeignKey("dbo.Items", "Pedido_Id", "dbo.Pedidoes");
            DropIndex("dbo.Items", new[] { "Pedido_Id" });
            DropTable("dbo.Items");
        }
    }
}

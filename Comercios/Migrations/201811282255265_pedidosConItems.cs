namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pedidosConItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Items", "fechaProductoAgregado", c => c.DateTime(nullable: false));
            AddColumn("dbo.Items", "producto_Id", c => c.Int());
            CreateIndex("dbo.Items", "producto_Id");
            AddForeignKey("dbo.Items", "producto_Id", "dbo.Productoes", "Id");
            DropColumn("dbo.Items", "idProducto");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Items", "idProducto", c => c.Int(nullable: false));
            DropForeignKey("dbo.Items", "producto_Id", "dbo.Productoes");
            DropIndex("dbo.Items", new[] { "producto_Id" });
            DropColumn("dbo.Items", "producto_Id");
            DropColumn("dbo.Items", "fechaProductoAgregado");
        }
    }
}

namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributosCarritoEnusuarioPedid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pedidoes", "idUsuario", c => c.Int(nullable: false));
            AddColumn("dbo.Pedidoes", "Usuario_Id", c => c.Int());
            CreateIndex("dbo.Pedidoes", "Usuario_Id");
            AddForeignKey("dbo.Pedidoes", "Usuario_Id", "dbo.Usuarios", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pedidoes", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.Pedidoes", new[] { "Usuario_Id" });
            DropColumn("dbo.Pedidoes", "Usuario_Id");
            DropColumn("dbo.Pedidoes", "idUsuario");
        }
    }
}

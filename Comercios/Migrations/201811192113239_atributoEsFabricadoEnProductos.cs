namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atributoEsFabricadoEnProductos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "esFabircado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Productoes", "esFabircado");
        }
    }
}

namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productosINt : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productoes", "tiempoPrevisto", c => c.Int(nullable: false));
            AlterColumn("dbo.Productoes", "cantidadMinima", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productoes", "cantidadMinima", c => c.String());
            AlterColumn("dbo.Productoes", "tiempoPrevisto", c => c.String());
        }
    }
}

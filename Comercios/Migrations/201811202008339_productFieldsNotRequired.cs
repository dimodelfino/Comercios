namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productFieldsNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Productoes", "paisOrigen", c => c.String(maxLength: 80));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Productoes", "paisOrigen", c => c.String(nullable: false, maxLength: 80));
        }
    }
}

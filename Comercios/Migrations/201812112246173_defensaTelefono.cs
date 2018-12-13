namespace Comercios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class defensaTelefono : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "telefono", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "telefono");
        }
    }
}

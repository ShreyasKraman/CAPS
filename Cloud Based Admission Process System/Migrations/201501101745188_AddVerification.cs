namespace MvcApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddVerification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Universities", "Verification", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Universities", "Verification");
        }
    }
}

namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Description");
        }
    }
}

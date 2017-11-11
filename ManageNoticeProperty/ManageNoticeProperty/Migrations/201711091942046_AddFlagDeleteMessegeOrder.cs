namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFlagDeleteMessegeOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "isDelete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "isDelete");
        }
    }
}

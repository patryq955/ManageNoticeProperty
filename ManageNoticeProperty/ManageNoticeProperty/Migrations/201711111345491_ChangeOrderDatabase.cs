namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOrderDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "isDeleteBuyer", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "isDeleteSeller", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "isDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "isDelete", c => c.Boolean(nullable: false));
            DropColumn("dbo.Orders", "isDeleteSeller");
            DropColumn("dbo.Orders", "isDeleteBuyer");
        }
    }
}

namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOrder : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "BuyUser_Id", newName: "BuyUserID");
            RenameIndex(table: "dbo.Orders", name: "IX_BuyUser_Id", newName: "IX_BuyUserID");
            DropColumn("dbo.Orders", "CopyUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CopyUserId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.Orders", name: "IX_BuyUserID", newName: "IX_BuyUser_Id");
            RenameColumn(table: "dbo.Orders", name: "BuyUserID", newName: "BuyUser_Id");
        }
    }
}

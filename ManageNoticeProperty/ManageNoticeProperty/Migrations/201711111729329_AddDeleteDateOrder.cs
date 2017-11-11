namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDeleteDateOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "DeleteBuyerDate", c => c.DateTime());
            AddColumn("dbo.Orders", "DeleteSellerDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeleteSellerDate");
            DropColumn("dbo.Orders", "DeleteBuyerDate");
        }
    }
}

namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldSellDateToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "SellDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "SellDate");
        }
    }
}

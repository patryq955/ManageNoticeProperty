namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDateSellProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Flats", "SellDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Flats", "SellDate");
        }
    }
}

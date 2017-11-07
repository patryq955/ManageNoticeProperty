namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastReapirBigError : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "Photo");
            AddColumn("dbo.Albums", "Photo", c => c.Binary(nullable: false));
        
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Photo", c => c.Binary(nullable: false));
            DropColumn("dbo.Albums", "Photo");
        }
    }
}

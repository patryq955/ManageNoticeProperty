namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhotoField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Albums", "Photo", c => c.Binary());
            DropColumn("dbo.Albums", "Path");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Albums", "Path", c => c.String(nullable: false));
            DropColumn("dbo.Albums", "Photo");
        }
    }
}

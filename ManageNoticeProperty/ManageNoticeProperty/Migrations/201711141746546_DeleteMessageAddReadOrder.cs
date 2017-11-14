namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteMessageAddReadOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "FlatId", "dbo.Flats");
            DropForeignKey("dbo.Messages", "FromUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ToUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Messages", new[] { "FromUserId" });
            DropIndex("dbo.Messages", new[] { "ToUserId" });
            DropIndex("dbo.Messages", new[] { "FlatId" });
            AddColumn("dbo.Orders", "isReadSeller", c => c.Boolean(nullable: false));
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        FromUserId = c.String(maxLength: 128),
                        ToUserId = c.String(maxLength: 128),
                        FlatId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId);
            
            DropColumn("dbo.Orders", "isReadSeller");
            CreateIndex("dbo.Messages", "FlatId");
            CreateIndex("dbo.Messages", "ToUserId");
            CreateIndex("dbo.Messages", "FromUserId");
            AddForeignKey("dbo.Messages", "ToUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "FromUserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Messages", "FlatId", "dbo.Flats", "FlatId", cascadeDelete: true);
        }
    }
}

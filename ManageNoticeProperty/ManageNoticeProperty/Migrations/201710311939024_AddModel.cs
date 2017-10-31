namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        FlatId = c.Int(nullable: false),
                        Path = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Flats", t => t.FlatId, cascadeDelete: true)
                .Index(t => t.FlatId);
            
            CreateTable(
                "dbo.Flats",
                c => new
                    {
                        FlatId = c.Int(nullable: false, identity: true),
                        QuantityRoom = c.Int(nullable: false),
                        TypeFlatID = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Area = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Condignation = c.Int(nullable: false),
                        IsBalcon = c.Boolean(nullable: false),
                        Description = c.String(nullable: false),
                        IsHidden = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        City = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        AddFlate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FlatId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        FlatId = c.Int(nullable: false),
                        CopyUserId = c.Int(nullable: false),
                        BuyUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.AspNetUsers", t => t.BuyUser_Id)
                .ForeignKey("dbo.Flats", t => t.FlatId, cascadeDelete: true)
                .Index(t => t.FlatId)
                .Index(t => t.BuyUser_Id);
            
            CreateTable(
                "dbo.TypeFlats",
                c => new
                    {
                        TypeFlatID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Flat_FlatId = c.Int(),
                    })
                .PrimaryKey(t => t.TypeFlatID)
                .ForeignKey("dbo.Flats", t => t.Flat_FlatId)
                .Index(t => t.Flat_FlatId);
            
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
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Flats", t => t.FlatId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.FromUserId)
                .ForeignKey("dbo.AspNetUsers", t => t.ToUserId)
                .Index(t => t.FromUserId)
                .Index(t => t.ToUserId)
                .Index(t => t.FlatId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ToUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "FromUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "FlatId", "dbo.Flats");
            DropForeignKey("dbo.Flats", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TypeFlats", "Flat_FlatId", "dbo.Flats");
            DropForeignKey("dbo.Orders", "FlatId", "dbo.Flats");
            DropForeignKey("dbo.Orders", "BuyUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Albums", "FlatId", "dbo.Flats");
            DropIndex("dbo.Messages", new[] { "FlatId" });
            DropIndex("dbo.Messages", new[] { "ToUserId" });
            DropIndex("dbo.Messages", new[] { "FromUserId" });
            DropIndex("dbo.TypeFlats", new[] { "Flat_FlatId" });
            DropIndex("dbo.Orders", new[] { "BuyUser_Id" });
            DropIndex("dbo.Orders", new[] { "FlatId" });
            DropIndex("dbo.Flats", new[] { "UserId" });
            DropIndex("dbo.Albums", new[] { "FlatId" });
            DropTable("dbo.Messages");
            DropTable("dbo.TypeFlats");
            DropTable("dbo.Orders");
            DropTable("dbo.Flats");
            DropTable("dbo.Albums");
        }
    }
}

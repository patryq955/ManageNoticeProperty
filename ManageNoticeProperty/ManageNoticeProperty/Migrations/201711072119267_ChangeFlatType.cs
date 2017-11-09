namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeFlatType : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.TypeFlats", "Flat_FlatId", "dbo.Flats");
           // DropIndex("dbo.TypeFlats", new[] { "Flat_FlatId" });
          //  DropColumn("dbo.Flats", "TypeFlatID");
            //RenameColumn(table: "dbo.Flats", name: "TypeFlatID", newName: "TypeFlatID");
            CreateIndex("dbo.Flats", "TypeFlatID");
            AddForeignKey("dbo.Flats", "TypeFlatID", "dbo.TypeFlats", "TypeFlatID", cascadeDelete: true);
          //  DropColumn("dbo.TypeFlats", "Flat_FlatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TypeFlats", "Flat_FlatId", c => c.Int());
            DropForeignKey("dbo.Flats", "TypeFlatID", "dbo.TypeFlats");
            DropIndex("dbo.Flats", new[] { "TypeFlatID" });
            RenameColumn(table: "dbo.Flats", name: "TypeFlatID", newName: "Flat_FlatId");
            AddColumn("dbo.Flats", "TypeFlatID", c => c.Int(nullable: false));
            CreateIndex("dbo.TypeFlats", "Flat_FlatId");
            AddForeignKey("dbo.TypeFlats", "Flat_FlatId", "dbo.Flats", "FlatId");
        }
    }
}

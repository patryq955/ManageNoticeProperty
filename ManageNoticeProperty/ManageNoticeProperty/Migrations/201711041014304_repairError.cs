namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class repairError : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Flats", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.Flats", name: "IX_UserId", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Flats", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Flats", name: "User_Id", newName: "UserId");
        }
    }
}

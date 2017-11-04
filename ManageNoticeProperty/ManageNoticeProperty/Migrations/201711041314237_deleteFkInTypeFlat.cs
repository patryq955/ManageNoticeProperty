namespace ManageNoticeProperty.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteFkInTypeFlat : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Flats", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Flats", name: "IX_User_Id", newName: "IX_UserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Flats", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Flats", name: "UserId", newName: "User_Id");
        }
    }
}

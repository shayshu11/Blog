namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedSenioritycolumnfromFandb : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fans", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Fans", "LastName", c => c.String(nullable: false));
            AlterColumn("dbo.Fans", "Password", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.Fans", "Seniority");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Fans", "Seniority", c => c.Int(nullable: false));
            AlterColumn("dbo.Fans", "Password", c => c.String(nullable: false));
            AlterColumn("dbo.Fans", "LastName", c => c.String());
            AlterColumn("dbo.Fans", "FirstName", c => c.String());
        }
    }
}

namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addloginpage : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fans", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Fans", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fans", "Password", c => c.String());
            AlterColumn("dbo.Fans", "Email", c => c.String());
        }
    }
}

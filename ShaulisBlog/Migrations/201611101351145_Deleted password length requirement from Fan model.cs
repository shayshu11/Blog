namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeletedpasswordlengthrequirementfromFanmodel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Fans", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Fans", "Password", c => c.String(nullable: false, maxLength: 30));
        }
    }
}

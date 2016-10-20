namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class emailpassworddateFormatUpdateDates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BlogPosts", "UpdateDate", c => c.DateTime(nullable: true));
            AddColumn("dbo.Fans", "Email", c => c.String());
            AddColumn("dbo.Fans", "Password", c => c.String());
            AddColumn("dbo.Comments", "UpdateDate", c => c.DateTime(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "UpdateDate");
            DropColumn("dbo.Fans", "Password");
            DropColumn("dbo.Fans", "Email");
            DropColumn("dbo.BlogPosts", "UpdateDate");
        }
    }
}

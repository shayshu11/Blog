namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateformats : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BlogPosts", "UpdateDate", c => c.DateTime());
            AlterColumn("dbo.Comments", "UpdateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "UpdateDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.BlogPosts", "UpdateDate", c => c.DateTime(nullable: false));
        }
    }
}

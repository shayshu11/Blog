namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BlogPost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WriterId = c.Int(nullable: false),
                        Content = c.String(),
                        Title = c.String(),
                        PostDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fans", t => t.WriterId, cascadeDelete: true)
                .Index(t => t.WriterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogPosts", "WriterId", "dbo.Fans");
            DropIndex("dbo.BlogPosts", new[] { "WriterId" });
            DropTable("dbo.BlogPosts");
        }
    }
}

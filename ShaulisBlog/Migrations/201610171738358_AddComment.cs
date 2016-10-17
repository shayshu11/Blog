namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        WriterId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                        Content = c.String(),
                        Title = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Fans", t => t.WriterId, cascadeDelete: true)
                .ForeignKey("dbo.BlogPosts", t => t.PostId, cascadeDelete: false)
                .Index(t => t.WriterId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostId", "dbo.BlogPosts");
            DropForeignKey("dbo.Comments", "WriterId", "dbo.Fans");
            DropIndex("dbo.Comments", new[] { "PostId" });
            DropIndex("dbo.Comments", new[] { "WriterId" });
            DropTable("dbo.Comments");
        }
    }
}

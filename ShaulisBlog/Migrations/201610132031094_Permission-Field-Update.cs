namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PermissionFieldUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permissions", "canPost", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canComment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canDeletePost", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canDeleteSelfComment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canDeleteAllComments", c => c.Boolean(nullable: false));
            DropColumn("dbo.Permissions", "canRead");
            DropColumn("dbo.Permissions", "canWrite");
            DropColumn("dbo.Permissions", "canDelete");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissions", "canDelete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canWrite", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canRead", c => c.Boolean(nullable: false));
            DropColumn("dbo.Permissions", "canDeleteAllComments");
            DropColumn("dbo.Permissions", "canDeleteSelfComment");
            DropColumn("dbo.Permissions", "canDeletePost");
            DropColumn("dbo.Permissions", "canComment");
            DropColumn("dbo.Permissions", "canPost");
        }
    }
}

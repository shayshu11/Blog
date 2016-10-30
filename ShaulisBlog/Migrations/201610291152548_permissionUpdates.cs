namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class permissionUpdates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Permissions", "type", c => c.Int(nullable: false));
            DropColumn("dbo.Permissions", "canPost");
            DropColumn("dbo.Permissions", "canComment");
            DropColumn("dbo.Permissions", "canDeletePost");
            DropColumn("dbo.Permissions", "canDeleteSelfComment");
            DropColumn("dbo.Permissions", "canDeleteAllComments");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permissions", "canDeleteAllComments", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canDeleteSelfComment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canDeletePost", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canComment", c => c.Boolean(nullable: false));
            AddColumn("dbo.Permissions", "canPost", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Permissions", "type", c => c.String());
        }
    }
}

namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Blog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        type = c.String(),
                        canRead = c.Boolean(nullable: false),
                        canWrite = c.Boolean(nullable: false),
                        canDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);

            CreateTable(
                "dbo.Fans",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    FirstName = c.String(),
                    LastName = c.String(),
                    Gender = c.Int(nullable: false),
                    DateOfBirth = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID);

            AddColumn("dbo.Fans", "permissionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Fans", "permissionId");
            AddForeignKey("dbo.Fans", "permissionId", "dbo.Permissions", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fans", "permissionId", "dbo.Permissions");
            DropIndex("dbo.Fans", new[] { "permissionId" });
            DropColumn("dbo.Fans", "permissionId");
            DropTable("dbo.Permissions");
        }
    }
}

namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcreationdatetoFan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "CreationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fans", "CreationDate");
        }
    }
}

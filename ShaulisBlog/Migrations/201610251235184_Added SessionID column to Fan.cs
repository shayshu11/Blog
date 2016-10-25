namespace ShaulisBlog.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSessionIDcolumntoFan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fans", "SessionID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Fans", "SessionID");
        }
    }
}

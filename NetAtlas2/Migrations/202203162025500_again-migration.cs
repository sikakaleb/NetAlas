namespace NetAtlas2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class againmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Bio", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Users", "FullName", c => c.String(maxLength: 8000, unicode: false));
            AlterColumn("dbo.Users", "Country", c => c.String(maxLength: 8000, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Country", c => c.String());
            AlterColumn("dbo.Users", "FullName", c => c.String());
            DropColumn("dbo.Users", "Bio");
        }
    }
}

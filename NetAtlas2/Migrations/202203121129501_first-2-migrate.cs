namespace NetAtlas2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first2migrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "FullName", c => c.String());
            AddColumn("dbo.Users", "Country", c => c.String());
            AddColumn("dbo.Users", "DatNaiss", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "DatNaiss");
            DropColumn("dbo.Users", "Country");
            DropColumn("dbo.Users", "FullName");
        }
    }
}

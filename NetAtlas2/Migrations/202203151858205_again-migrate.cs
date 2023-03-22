namespace NetAtlas2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class againmigrate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Publications", "titre", c => c.String(maxLength: 5000, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Publications", "titre");
        }
    }
}

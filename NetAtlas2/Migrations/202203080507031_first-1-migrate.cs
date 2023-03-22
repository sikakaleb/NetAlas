namespace NetAtlas2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first1migrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 900, unicode: false),
                        UserName = c.String(nullable: false, maxLength: 500, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ImageUrl = c.String(),
                        CreatedOn = c.DateTime(),
                        Identity = c.String(maxLength: 1500, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id, unique: true)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Publications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        CreatedOn = c.DateTime(),
                        UserId = c.Int(nullable: false),
                        CommentId = c.Int(nullable: false),
                        Publication_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .ForeignKey("dbo.Publications", t => t.Publication_Id)
                .Index(t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.Publication_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 500, unicode: false),
                        Password = c.String(nullable: false, maxLength: 500),
                        Email = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ImageUrl = c.String(),
                        CreatedOn = c.DateTime(),
                        AllFriend = c.String(maxLength: 8000, unicode: false),
                        Invitation = c.String(maxLength: 8000, unicode: false),
                        Advice = c.Int(nullable: false),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Text = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Lien",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Link = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Moderateur",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 900, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.Id)
                .Index(t => t.Id, unique: true);
            
            CreateTable(
                "dbo.Photo",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Phot_Picture = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Video",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Vid_Picture = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publications", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Video", "Id", "dbo.Publications");
            DropForeignKey("dbo.Photo", "Id", "dbo.Publications");
            DropForeignKey("dbo.Moderateur", "Id", "dbo.Admins");
            DropForeignKey("dbo.Lien", "Id", "dbo.Publications");
            DropForeignKey("dbo.Comment", "Id", "dbo.Publications");
            DropForeignKey("dbo.Publications", "UserId", "dbo.Users");
            DropForeignKey("dbo.Replies", "Publication_Id", "dbo.Publications");
            DropForeignKey("dbo.Replies", "UserId", "dbo.Users");
            DropForeignKey("dbo.Replies", "CommentId", "dbo.Comment");
            DropIndex("dbo.Video", new[] { "Id" });
            DropIndex("dbo.Photo", new[] { "Id" });
            DropIndex("dbo.Moderateur", new[] { "Id" });
            DropIndex("dbo.Lien", new[] { "Id" });
            DropIndex("dbo.Comment", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "UserName" });
            DropIndex("dbo.Replies", new[] { "Publication_Id" });
            DropIndex("dbo.Replies", new[] { "CommentId" });
            DropIndex("dbo.Replies", new[] { "UserId" });
            DropIndex("dbo.Publications", new[] { "UserId" });
            DropIndex("dbo.Admins", new[] { "Email" });
            DropIndex("dbo.Admins", new[] { "UserName" });
            DropIndex("dbo.Admins", new[] { "Id" });
            DropTable("dbo.Video");
            DropTable("dbo.Photo");
            DropTable("dbo.Moderateur");
            DropTable("dbo.Lien");
            DropTable("dbo.Comment");
            DropTable("dbo.Users");
            DropTable("dbo.Replies");
            DropTable("dbo.Publications");
            DropTable("dbo.Admins");
        }
    }
}

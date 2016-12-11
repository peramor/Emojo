namespace Emojo.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FollowRelationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Followed_UserId = c.Long(),
                        Follower_UserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Followed_UserId)
                .ForeignKey("dbo.Users", t => t.Follower_UserId)
                .Index(t => t.Followed_UserId)
                .Index(t => t.Follower_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        Username = c.String(nullable: false),
                        FullName = c.String(),
                        ProfilePhoto = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.String(nullable: false, maxLength: 128),
                        LinkStandard = c.String(),
                        LinkLow = c.String(),
                        LinkThumbnail = c.String(),
                        Anger = c.Double(nullable: false),
                        Fear = c.Double(nullable: false),
                        Happiness = c.Double(nullable: false),
                        Sadness = c.Double(nullable: false),
                        Surprise = c.Double(nullable: false),
                        User_UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "User_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Follower_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Followed_UserId" });
            DropTable("dbo.Photos");
            DropTable("dbo.Users");
            DropTable("dbo.FollowRelationships");
        }
    }
}

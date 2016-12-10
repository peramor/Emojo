namespace Emojo.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class follower_relationships : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users");
            DropIndex("dbo.FollowRelationships", new[] { "Follower_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Followed_UserId" });
            DropTable("dbo.FollowRelationships");
        }
    }
}

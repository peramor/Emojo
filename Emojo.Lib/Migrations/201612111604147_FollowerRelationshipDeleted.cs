namespace Emojo.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FollowerRelationshipDeleted : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowRelationships", "Followed_Id", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Follower_Id", "dbo.Users");
            DropIndex("dbo.FollowRelationships", new[] { "Followed_Id" });
            DropIndex("dbo.FollowRelationships", new[] { "Follower_Id" });
            DropTable("dbo.FollowRelationships");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FollowRelationships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Followed_Id = c.Int(),
                        Follower_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.FollowRelationships", "Follower_Id");
            CreateIndex("dbo.FollowRelationships", "Followed_Id");
            AddForeignKey("dbo.FollowRelationships", "Follower_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.FollowRelationships", "Followed_Id", "dbo.Users", "Id");
        }
    }
}

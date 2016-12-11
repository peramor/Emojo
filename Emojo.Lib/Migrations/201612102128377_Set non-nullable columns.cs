namespace Emojo.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Setnonnullablecolumns : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users");
            DropForeignKey("dbo.Photos", "User_UserId", "dbo.Users");
            DropIndex("dbo.FollowRelationships", new[] { "Followed_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Follower_UserId" });
            DropIndex("dbo.Photos", new[] { "User_UserId" });
            AlterColumn("dbo.FollowRelationships", "Followed_UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.FollowRelationships", "Follower_UserId", c => c.Long(nullable: false));
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Photos", "User_UserId", c => c.Long(nullable: false));
            CreateIndex("dbo.FollowRelationships", "Followed_UserId");
            CreateIndex("dbo.FollowRelationships", "Follower_UserId");
            CreateIndex("dbo.Photos", "User_UserId");
            AddForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Photos", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "User_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Follower_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Followed_UserId" });
            AlterColumn("dbo.Photos", "User_UserId", c => c.Long());
            AlterColumn("dbo.Users", "Username", c => c.String());
            AlterColumn("dbo.FollowRelationships", "Follower_UserId", c => c.Long());
            AlterColumn("dbo.FollowRelationships", "Followed_UserId", c => c.Long());
            CreateIndex("dbo.Photos", "User_UserId");
            CreateIndex("dbo.FollowRelationships", "Follower_UserId");
            CreateIndex("dbo.FollowRelationships", "Followed_UserId");
            AddForeignKey("dbo.Photos", "User_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users", "UserId");
        }
    }
}

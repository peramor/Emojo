namespace Emojo.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurrogatekeyonUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users");
            DropForeignKey("dbo.Photos", "User_UserId", "dbo.Users");
            DropIndex("dbo.FollowRelationships", new[] { "Followed_UserId" });
            DropIndex("dbo.FollowRelationships", new[] { "Follower_UserId" });
            DropIndex("dbo.Photos", new[] { "User_UserId" });
            RenameColumn(table: "dbo.FollowRelationships", name: "Followed_UserId", newName: "Followed_Id");
            RenameColumn(table: "dbo.FollowRelationships", name: "Follower_UserId", newName: "Follower_Id");
            RenameColumn(table: "dbo.Photos", name: "User_UserId", newName: "User_Id");
            DropPrimaryKey("dbo.Users");
            AddColumn("dbo.Users", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.FollowRelationships", "Followed_Id", c => c.Int());
            AlterColumn("dbo.FollowRelationships", "Follower_Id", c => c.Int());
            AlterColumn("dbo.Photos", "User_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Users", "Id");
            CreateIndex("dbo.FollowRelationships", "Followed_Id");
            CreateIndex("dbo.FollowRelationships", "Follower_Id");
            CreateIndex("dbo.Photos", "User_Id");
            AddForeignKey("dbo.FollowRelationships", "Followed_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.FollowRelationships", "Follower_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Photos", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Follower_Id", "dbo.Users");
            DropForeignKey("dbo.FollowRelationships", "Followed_Id", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "User_Id" });
            DropIndex("dbo.FollowRelationships", new[] { "Follower_Id" });
            DropIndex("dbo.FollowRelationships", new[] { "Followed_Id" });
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Photos", "User_Id", c => c.Long(nullable: false));
            AlterColumn("dbo.FollowRelationships", "Follower_Id", c => c.Long());
            AlterColumn("dbo.FollowRelationships", "Followed_Id", c => c.Long());
            DropColumn("dbo.Users", "Id");
            AddPrimaryKey("dbo.Users", "UserId");
            RenameColumn(table: "dbo.Photos", name: "User_Id", newName: "User_UserId");
            RenameColumn(table: "dbo.FollowRelationships", name: "Follower_Id", newName: "Follower_UserId");
            RenameColumn(table: "dbo.FollowRelationships", name: "Followed_Id", newName: "Followed_UserId");
            CreateIndex("dbo.Photos", "User_UserId");
            CreateIndex("dbo.FollowRelationships", "Follower_UserId");
            CreateIndex("dbo.FollowRelationships", "Followed_UserId");
            AddForeignKey("dbo.Photos", "User_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.FollowRelationships", "Follower_UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.FollowRelationships", "Followed_UserId", "dbo.Users", "UserId");
        }
    }
}

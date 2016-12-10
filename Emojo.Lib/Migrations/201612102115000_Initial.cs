namespace Emojo.Lib.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
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
                        User_UserId = c.Long(),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Long(nullable: false, identity: true),
                        Username = c.String(),
                        FullName = c.String(),
                        ProfilePhoto = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "User_UserId", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
        }
    }
}

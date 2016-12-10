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
                        Id = c.Int(nullable: false, identity: true),
                        Anger = c.Double(nullable: false),
                        Fear = c.Double(nullable: false),
                        Happiness = c.Double(nullable: false),
                        Sadness = c.Double(nullable: false),
                        Surprise = c.Double(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Photos", "User_Id", "dbo.Users");
            DropIndex("dbo.Photos", new[] { "User_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Photos");
        }
    }
}

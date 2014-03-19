namespace Trolltampen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedAddressToContact : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Addresses", new[] { "MediaTypeID" });
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false),
                        City = c.String(),
                        Sted = c.String(),
                        PostNo = c.String(),
                        Description = c.String(),
                        MediaTypeID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            DropTable("dbo.Addresses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Position = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false),
                        City = c.String(),
                        Sted = c.String(),
                        PostNo = c.String(),
                        Description = c.String(),
                        MediaTypeID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Contacts", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Contacts", new[] { "MediaTypeID" });
            DropTable("dbo.Contacts");
            CreateIndex("dbo.Addresses", "MediaTypeID");
            AddForeignKey("dbo.Addresses", "MediaTypeID", "dbo.MediaTypes", "ID", cascadeDelete: true);
        }
    }
}

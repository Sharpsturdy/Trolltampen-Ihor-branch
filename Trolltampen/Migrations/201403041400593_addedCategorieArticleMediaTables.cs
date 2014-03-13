namespace Trolltampen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCategorieArticleMediaTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Headline = c.String(nullable: false),
                        Ingress = c.String(),
                        Body = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OrderNum = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        ExtLink = c.String(),
                        MediaTypeID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        OrderNum = c.Int(nullable: false),
                        MediaTypeID = c.Int(nullable: false),
                        ContentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: false)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.MediaTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhotoGalleries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.GalleryPhotoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        PhotoGalleryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhotoGalleries", t => t.PhotoGalleryID, cascadeDelete: true)
                .Index(t => t.PhotoGalleryID);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaTypeID = c.Int(nullable: false),
                        FileName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
            CreateTable(
                "dbo.VideoLinks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MediaTypeID = c.Int(nullable: false),
                        Link = c.String(),
                        Hosting = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MediaTypes", t => t.MediaTypeID, cascadeDelete: true)
                .Index(t => t.MediaTypeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articles", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.Articles", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.VideoLinks", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.Photos", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.PhotoGalleries", "MediaTypeID", "dbo.MediaTypes");
            DropForeignKey("dbo.GalleryPhotoes", "PhotoGalleryID", "dbo.PhotoGalleries");
            DropIndex("dbo.Articles", new[] { "MediaTypeID" });
            DropIndex("dbo.Articles", new[] { "CategoryID" });
            DropIndex("dbo.Categories", new[] { "MediaTypeID" });
            DropIndex("dbo.VideoLinks", new[] { "MediaTypeID" });
            DropIndex("dbo.Photos", new[] { "MediaTypeID" });
            DropIndex("dbo.PhotoGalleries", new[] { "MediaTypeID" });
            DropIndex("dbo.GalleryPhotoes", new[] { "PhotoGalleryID" });
            DropTable("dbo.VideoLinks");
            DropTable("dbo.Photos");
            DropTable("dbo.GalleryPhotoes");
            DropTable("dbo.PhotoGalleries");
            DropTable("dbo.MediaTypes");
            DropTable("dbo.Categories");
            DropTable("dbo.Articles");
        }
    }
}

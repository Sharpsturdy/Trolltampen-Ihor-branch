namespace Trolltampen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFieldForGalleryPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GalleryPhotoes", "OrderNum", c => c.Int(nullable: false));
            AddColumn("dbo.GalleryPhotoes", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GalleryPhotoes", "isActive");
            DropColumn("dbo.GalleryPhotoes", "OrderNum");
        }
    }
}

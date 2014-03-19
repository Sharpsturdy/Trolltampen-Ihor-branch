namespace Trolltampen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateAddressTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "Position", c => c.String(nullable: false));
            AddColumn("dbo.Addresses", "PhoneNum", c => c.String(nullable: false));
            AddColumn("dbo.Addresses", "Description", c => c.String());
            AddColumn("dbo.Addresses", "MediaTypeID", c => c.Int(nullable: false));
            AddColumn("dbo.Addresses", "ContentID", c => c.Int(nullable: false));
            CreateIndex("dbo.Addresses", "MediaTypeID");
            AddForeignKey("dbo.Addresses", "MediaTypeID", "dbo.MediaTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "MediaTypeID", "dbo.MediaTypes");
            DropIndex("dbo.Addresses", new[] { "MediaTypeID" });
            DropColumn("dbo.Addresses", "ContentID");
            DropColumn("dbo.Addresses", "MediaTypeID");
            DropColumn("dbo.Addresses", "Description");
            DropColumn("dbo.Addresses", "PhoneNum");
            DropColumn("dbo.Addresses", "Position");
        }
    }
}

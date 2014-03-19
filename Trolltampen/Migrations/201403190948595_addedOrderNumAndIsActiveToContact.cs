namespace Trolltampen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedOrderNumAndIsActiveToContact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "OrderNum", c => c.Int(nullable: false));
            AddColumn("dbo.Contacts", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Contacts", "IsActive");
            DropColumn("dbo.Contacts", "OrderNum");
        }
    }
}

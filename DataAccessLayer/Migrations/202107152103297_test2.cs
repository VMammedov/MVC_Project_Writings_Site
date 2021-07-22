namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Headings", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Contents", "HeadingID", "dbo.Headings");
            DropIndex("dbo.Headings", new[] { "CategoryID" });
            DropIndex("dbo.Contents", new[] { "HeadingID" });
            DropColumn("dbo.Headings", "CategoryID");
            DropColumn("dbo.Contents", "HeadingID");
        }

        public override void Down()
        {
            AddColumn("dbo.Contents", "HeadingID", c => c.Int(nullable: false));
            AddColumn("dbo.Headings", "CategoryID", c => c.Int(nullable: false));
            CreateIndex("dbo.Contents", "HeadingID");
            CreateIndex("dbo.Headings", "CategoryID");
            AddForeignKey("dbo.Contents", "HeadingID", "dbo.Headings", "HeadingID", cascadeDelete: true);
            AddForeignKey("dbo.Headings", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
        }
    }
}

namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_add_again_fk_in_all : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contents", "HeadingID", c => c.Int(nullable: false));
            AddColumn("dbo.Contents", "WriterID", c => c.Int());
            AddColumn("dbo.Headings", "CategoryID", c => c.Int(nullable: false));
            AddColumn("dbo.Headings", "WriterID", c => c.Int(nullable: false));
            CreateIndex("dbo.Headings", "CategoryID");
            CreateIndex("dbo.Headings", "WriterID");
            CreateIndex("dbo.Contents", "HeadingID");
            CreateIndex("dbo.Contents", "WriterID");
            AddForeignKey("dbo.Headings", "CategoryID", "dbo.Categories", "CategoryID", cascadeDelete: true);
            AddForeignKey("dbo.Contents", "HeadingID", "dbo.Headings", "HeadingID", cascadeDelete: true);
            AddForeignKey("dbo.Contents", "WriterID", "dbo.Writers", "WriterID");
            AddForeignKey("dbo.Headings", "WriterID", "dbo.Writers", "WriterID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Headings", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Contents", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Contents", "HeadingID", "dbo.Headings");
            DropForeignKey("dbo.Headings", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Contents", new[] { "WriterID" });
            DropIndex("dbo.Contents", new[] { "HeadingID" });
            DropIndex("dbo.Headings", new[] { "WriterID" });
            DropIndex("dbo.Headings", new[] { "CategoryID" });
            DropColumn("dbo.Headings", "WriterID");
            DropColumn("dbo.Headings", "CategoryID");
            DropColumn("dbo.Contents", "WriterID");
            DropColumn("dbo.Contents", "HeadingID");
        }
    }
}

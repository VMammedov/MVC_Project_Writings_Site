namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Headings", "WriterID", "dbo.Writers");
            DropForeignKey("dbo.Contents", "WriterID", "dbo.Writers");
            DropIndex("dbo.Headings", new[] { "WriterID" });
            DropIndex("dbo.Contents", new[] { "WriterID" });
            DropPrimaryKey("dbo.Writers");
            AddColumn("dbo.Headings", "Writer_WriterID", c => c.Long());
            AddColumn("dbo.Contents", "Writer_WriterID", c => c.Long());
            AlterColumn("dbo.Writers", "WriterID", c => c.Long(nullable: false, identity: true));
            AddPrimaryKey("dbo.Writers", "WriterID");
            CreateIndex("dbo.Headings", "Writer_WriterID");
            CreateIndex("dbo.Contents", "Writer_WriterID");
            AddForeignKey("dbo.Headings", "Writer_WriterID", "dbo.Writers", "WriterID");
            AddForeignKey("dbo.Contents", "Writer_WriterID", "dbo.Writers", "WriterID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contents", "Writer_WriterID", "dbo.Writers");
            DropForeignKey("dbo.Headings", "Writer_WriterID", "dbo.Writers");
            DropIndex("dbo.Contents", new[] { "Writer_WriterID" });
            DropIndex("dbo.Headings", new[] { "Writer_WriterID" });
            DropPrimaryKey("dbo.Writers");
            AlterColumn("dbo.Writers", "WriterID", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Contents", "Writer_WriterID");
            DropColumn("dbo.Headings", "Writer_WriterID");
            AddPrimaryKey("dbo.Writers", "WriterID");
            CreateIndex("dbo.Contents", "WriterID");
            CreateIndex("dbo.Headings", "WriterID");
            AddForeignKey("dbo.Contents", "WriterID", "dbo.Writers", "WriterID");
            AddForeignKey("dbo.Headings", "WriterID", "dbo.Writers", "WriterID", cascadeDelete: true);
        }
    }
}

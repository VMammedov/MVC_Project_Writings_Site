namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_adding_draft_class : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drafts",
                c => new
                    {
                        MessageIDDraft = c.Int(nullable: false, identity: true),
                        SenderMail = c.String(maxLength: 50),
                        ReceiverMailDraft = c.String(maxLength: 50),
                        SubjectDraft = c.String(maxLength: 100),
                        MessageContentDraft = c.String(),
                        MessageDateDraft = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageIDDraft);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Drafts");
        }
    }
}

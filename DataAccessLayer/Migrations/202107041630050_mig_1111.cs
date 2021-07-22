namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_1111 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "SenderMail", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Messages", "ReceiverMail", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "ReceiverMail", c => c.String(maxLength: 50));
            AlterColumn("dbo.Messages", "SenderMail", c => c.String(maxLength: 50));
        }
    }
}

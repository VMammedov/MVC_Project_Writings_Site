namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_deleting_adminusername_adminpassword : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Admins", "AdminUserName");
            DropColumn("dbo.Admins", "AdminPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "AdminPassword", c => c.String(maxLength: 50));
            AddColumn("dbo.Admins", "AdminUserName", c => c.String(maxLength: 50));
        }
    }
}

namespace LTQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_account : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Role", newName: "Roles");
            DropPrimaryKey("dbo.Roles");
            AddColumn("dbo.Account", "RoleID", c => c.String(maxLength: 10));
            AlterColumn("dbo.Roles", "RoleID", c => c.String(nullable: false, maxLength: 10));
            AlterColumn("dbo.Roles", "RoleName", c => c.String(maxLength: 50));
            AddPrimaryKey("dbo.Roles", "RoleID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Roles");
            AlterColumn("dbo.Roles", "RoleName", c => c.String());
            AlterColumn("dbo.Roles", "RoleID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Account", "RoleID");
            AddPrimaryKey("dbo.Roles", "RoleID");
            RenameTable(name: "dbo.Roles", newName: "Role");
        }
    }
}

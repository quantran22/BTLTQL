namespace LTQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class altr_column_student : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Student", "StudentName", c => c.String(nullable: false));
            AlterColumn("dbo.Student", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Student", "Address", c => c.String());
            AlterColumn("dbo.Student", "StudentName", c => c.String());
        }
    }
}

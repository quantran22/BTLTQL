namespace LTQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_column_student_address : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Student", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Student", "Address");
        }
    }
}

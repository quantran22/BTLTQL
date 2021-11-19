namespace LTQL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_table_article : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        ArticleID = c.String(nullable: false, maxLength: 128),
                        Author = c.String(),
                        ArticleContent = c.String(),
                    })
                .PrimaryKey(t => t.ArticleID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Article");
        }
    }
}

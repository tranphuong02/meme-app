namespace DatabaseAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableChapter : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Chapters", "CategoryId");
            AddForeignKey("dbo.Chapters", "CategoryId", "dbo.Categories", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chapters", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Chapters", new[] { "CategoryId" });
            DropColumn("dbo.Chapters", "CategoryId");
        }
    }
}

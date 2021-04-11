namespace NBITTask.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ValidationsAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Products", "Image", c => c.Binary(nullable: false));
            AlterColumn("dbo.Reviews", "CustomerReview", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "CustomerReview", c => c.String());
            AlterColumn("dbo.Products", "Image", c => c.Binary());
            AlterColumn("dbo.Products", "Name", c => c.String());
        }
    }
}

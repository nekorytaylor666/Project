namespace ExamWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsConfirmed : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsConfirmed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsConfirmed");
        }
    }
}

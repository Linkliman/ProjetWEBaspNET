namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentVM : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "Id_Music", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CommentModels", "Id_Music");
        }
    }
}

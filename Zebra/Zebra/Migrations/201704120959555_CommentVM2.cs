namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentVM2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CommentModels", "Music", c => c.Int(nullable: false));
            DropColumn("dbo.CommentModels", "Id_Music");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CommentModels", "Id_Music", c => c.Int(nullable: false));
            DropColumn("dbo.CommentModels", "Music");
        }
    }
}

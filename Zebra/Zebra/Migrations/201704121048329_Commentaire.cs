namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Commentaire : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CommentModels", "ID_User", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CommentModels", "ID_User", c => c.Int(nullable: false));
        }
    }
}

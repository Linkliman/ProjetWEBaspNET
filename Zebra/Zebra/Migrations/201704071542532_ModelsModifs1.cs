namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsModifs1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MusicModels", "Title", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MusicModels", "Title", c => c.Int(nullable: false));
        }
    }
}

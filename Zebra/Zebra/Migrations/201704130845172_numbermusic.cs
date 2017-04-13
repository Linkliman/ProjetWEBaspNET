namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numbermusic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicModels", "numberMusic", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicModels", "numberMusic");
        }
    }
}

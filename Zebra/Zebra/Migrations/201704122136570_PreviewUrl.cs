namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PreviewUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicModels", "PreviewUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicModels", "PreviewUrl");
        }
    }
}

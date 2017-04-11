namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicModels", "Album", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicModels", "Album");
        }
    }
}

namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlbumType2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MusicModels", "Genre");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicModels", "Genre", c => c.String());
        }
    }
}

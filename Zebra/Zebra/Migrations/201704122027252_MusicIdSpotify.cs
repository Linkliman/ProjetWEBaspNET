namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MusicIdSpotify : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicModels", "ID_Spotify", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicModels", "ID_Spotify");
        }
    }
}

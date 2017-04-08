namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Created_By : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlbumModels", "Created_by", c => c.String());
            AddColumn("dbo.MusicModels", "Created_by", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicModels", "Created_by");
            DropColumn("dbo.AlbumModels", "Created_by");
        }
    }
}

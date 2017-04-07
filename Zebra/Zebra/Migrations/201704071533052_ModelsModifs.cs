namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelsModifs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SimpleArtists",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Href = c.String(),
                        Name = c.String(),
                        Type = c.String(),
                        Uri = c.String(),
                        Error_Status = c.Int(nullable: false),
                        Error_Message = c.String(),
                        AlbumModels_ID = c.Int(),
                        MusicModels_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AlbumModels", t => t.AlbumModels_ID)
                .ForeignKey("dbo.MusicModels", t => t.MusicModels_ID)
                .Index(t => t.AlbumModels_ID)
                .Index(t => t.MusicModels_ID);
            
            DropColumn("dbo.AlbumModels", "Genre");
            DropColumn("dbo.AlbumModels", "ID_User");
            DropColumn("dbo.MusicModels", "ID_User");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MusicModels", "ID_User", c => c.Int(nullable: false));
            AddColumn("dbo.AlbumModels", "ID_User", c => c.Int(nullable: false));
            AddColumn("dbo.AlbumModels", "Genre", c => c.String());
            DropForeignKey("dbo.SimpleArtists", "MusicModels_ID", "dbo.MusicModels");
            DropForeignKey("dbo.SimpleArtists", "AlbumModels_ID", "dbo.AlbumModels");
            DropIndex("dbo.SimpleArtists", new[] { "MusicModels_ID" });
            DropIndex("dbo.SimpleArtists", new[] { "AlbumModels_ID" });
            DropTable("dbo.SimpleArtists");
        }
    }
}

namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class genre : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicModels", "Genre", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MusicModels", "Genre");
        }
    }
}

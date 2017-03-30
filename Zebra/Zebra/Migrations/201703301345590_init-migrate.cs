namespace Zebra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initmigrate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AlbumModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 60),
                        ReleaseDate = c.DateTime(nullable: false),
                        Genre = c.String(),
                        prix = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Note = c.Int(nullable: false),
                        ID_User = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CommentModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_User = c.Int(nullable: false),
                        Content = c.String(),
                        ID_Album_ID = c.Int(),
                        ID_Music_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AlbumModels", t => t.ID_Album_ID)
                .ForeignKey("dbo.MusicModels", t => t.ID_Music_ID)
                .Index(t => t.ID_Album_ID)
                .Index(t => t.ID_Music_ID);
            
            CreateTable(
                "dbo.MusicModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Genre = c.String(),
                        prix = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ID_User = c.Int(nullable: false),
                        Note = c.Int(nullable: false),
                        ID_Album_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AlbumModels", t => t.ID_Album_ID)
                .Index(t => t.ID_Album_ID);
            
            CreateTable(
                "dbo.OrderModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ID_User = c.Int(nullable: false),
                        ID_Album_ID = c.Int(),
                        ID_Music_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AlbumModels", t => t.ID_Album_ID)
                .ForeignKey("dbo.MusicModels", t => t.ID_Music_ID)
                .Index(t => t.ID_Album_ID)
                .Index(t => t.ID_Music_ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.OrderModels", "ID_Music_ID", "dbo.MusicModels");
            DropForeignKey("dbo.OrderModels", "ID_Album_ID", "dbo.AlbumModels");
            DropForeignKey("dbo.CommentModels", "ID_Music_ID", "dbo.MusicModels");
            DropForeignKey("dbo.MusicModels", "ID_Album_ID", "dbo.AlbumModels");
            DropForeignKey("dbo.CommentModels", "ID_Album_ID", "dbo.AlbumModels");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OrderModels", new[] { "ID_Music_ID" });
            DropIndex("dbo.OrderModels", new[] { "ID_Album_ID" });
            DropIndex("dbo.MusicModels", new[] { "ID_Album_ID" });
            DropIndex("dbo.CommentModels", new[] { "ID_Music_ID" });
            DropIndex("dbo.CommentModels", new[] { "ID_Album_ID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OrderModels");
            DropTable("dbo.MusicModels");
            DropTable("dbo.CommentModels");
            DropTable("dbo.AlbumModels");
        }
    }
}

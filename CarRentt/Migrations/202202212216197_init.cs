namespace CarRentt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.categories",
                c => new
                    {
                        CategorieID = c.Int(nullable: false, identity: true),
                        NomCategorie = c.String(nullable: false, maxLength: 100),
                        DescriptionCategorie = c.String(nullable: false, maxLength: 300),
                    })
                .PrimaryKey(t => t.CategorieID)
                .Index(t => t.NomCategorie, unique: true);
            
            CreateTable(
                "dbo.LigneDeReservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false),
                        VoitureID = c.Int(nullable: false),
                        Duree = c.Int(nullable: false),
                        PrixTTC = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.ReservationID, t.VoitureID })
                .ForeignKey("dbo.reservations", t => t.ReservationID, cascadeDelete: true)
                .ForeignKey("dbo.voitures", t => t.VoitureID, cascadeDelete: true)
                .Index(t => t.ReservationID)
                .Index(t => t.VoitureID);
            
            CreateTable(
                "dbo.reservations",
                c => new
                    {
                        ReservationID = c.Int(nullable: false, identity: true),
                        DateReservation = c.DateTime(nullable: false),
                        UserID = c.String(maxLength: 128),
                        Etat = c.String(nullable: false),
                        Total = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ImageCinRecto = c.String(),
                        ImagePermisRecto = c.String(),
                        ImageCinVerso = c.String(),
                        ImagePermisVerso = c.String(),
                        etat = c.String(),
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
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.voitures",
                c => new
                    {
                        VoitureID = c.Int(nullable: false, identity: true),
                        Matricule = c.String(nullable: false, maxLength: 30),
                        DateDeMiseEnCirculation = c.DateTime(nullable: false),
                        TypeCarburant = c.String(nullable: false),
                        PrixJournaliere = c.Double(nullable: false),
                        Image = c.String(nullable: false, maxLength: 200),
                        ModeleID = c.Int(nullable: false),
                        IsAvailable = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoitureID)
                .ForeignKey("dbo.modeles", t => t.ModeleID, cascadeDelete: true)
                .Index(t => t.Matricule, unique: true)
                .Index(t => t.ModeleID);
            
            CreateTable(
                "dbo.modeles",
                c => new
                    {
                        ModeleID = c.Int(nullable: false, identity: true),
                        SerieVoiture = c.String(nullable: false, maxLength: 20),
                        MarqueVoiture = c.String(nullable: false, maxLength: 30),
                        CategorieID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ModeleID)
                .ForeignKey("dbo.categories", t => t.CategorieID, cascadeDelete: true)
                .Index(t => t.CategorieID);
            
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
                "dbo.CartItems",
                c => new
                    {
                        ItemId = c.String(nullable: false, maxLength: 128),
                        CartId = c.String(),
                        Duree = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Idvoiture = c.Int(nullable: false),
                        Voiture_VoitureID = c.Int(),
                    })
                .PrimaryKey(t => t.ItemId)
                .ForeignKey("dbo.voitures", t => t.Voiture_VoitureID)
                .Index(t => t.Voiture_VoitureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CartItems", "Voiture_VoitureID", "dbo.voitures");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.LigneDeReservations", "VoitureID", "dbo.voitures");
            DropForeignKey("dbo.voitures", "ModeleID", "dbo.modeles");
            DropForeignKey("dbo.modeles", "CategorieID", "dbo.categories");
            DropForeignKey("dbo.LigneDeReservations", "ReservationID", "dbo.reservations");
            DropForeignKey("dbo.reservations", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CartItems", new[] { "Voiture_VoitureID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.modeles", new[] { "CategorieID" });
            DropIndex("dbo.voitures", new[] { "ModeleID" });
            DropIndex("dbo.voitures", new[] { "Matricule" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.reservations", new[] { "UserID" });
            DropIndex("dbo.LigneDeReservations", new[] { "VoitureID" });
            DropIndex("dbo.LigneDeReservations", new[] { "ReservationID" });
            DropIndex("dbo.categories", new[] { "NomCategorie" });
            DropTable("dbo.CartItems");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.modeles");
            DropTable("dbo.voitures");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.reservations");
            DropTable("dbo.LigneDeReservations");
            DropTable("dbo.categories");
        }
    }
}

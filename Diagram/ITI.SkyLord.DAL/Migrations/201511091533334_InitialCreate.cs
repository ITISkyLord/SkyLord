namespace ITI.SkyLord.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apprentices",
                c => new
                    {
                        ApprenticeId = c.Long(nullable: false, identity: true),
                        ApprenticeLevel_LevelId = c.Long(),
                        Mage_MageId = c.Long(),
                    })
                .PrimaryKey(t => t.ApprenticeId)
                .ForeignKey("dbo.Levels", t => t.ApprenticeLevel_LevelId)
                .ForeignKey("dbo.Mages", t => t.Mage_MageId)
                .Index(t => t.ApprenticeLevel_LevelId)
                .Index(t => t.Mage_MageId);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelId = c.Long(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        ApprenticeLevelId = c.Int(),
                        TechnologyLevelId = c.Long(),
                        MageLevelId = c.Long(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Cost_RessourceId = c.Long(),
                    })
                .PrimaryKey(t => t.LevelId)
                .ForeignKey("dbo.Ressources", t => t.Cost_RessourceId)
                .Index(t => t.Cost_RessourceId);
            
            CreateTable(
                "dbo.Ressources",
                c => new
                    {
                        RessourceId = c.Long(nullable: false, identity: true),
                        Cristal = c.Int(nullable: false),
                        Magic = c.Int(nullable: false),
                        Metal = c.Int(nullable: false),
                        Wood = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RessourceId);
            
            CreateTable(
                "dbo.Armies",
                c => new
                    {
                        ArmyId = c.Long(nullable: false, identity: true),
                        ArmyState = c.Int(nullable: false),
                        Island_IslandId = c.Int(),
                    })
                .PrimaryKey(t => t.ArmyId)
                .ForeignKey("dbo.Islands", t => t.Island_IslandId)
                .Index(t => t.Island_IslandId);
            
            CreateTable(
                "dbo.Islands",
                c => new
                    {
                        IslandId = c.Int(nullable: false, identity: true),
                        IsCapital = c.Boolean(nullable: false),
                        Loyalty = c.Int(nullable: false),
                        Name = c.String(),
                        AllRessources_RessourceId = c.Long(),
                        Coordinates_CoordinateId = c.Long(),
                        Owner_PlayerId = c.Long(),
                        Map_MapId = c.Long(),
                    })
                .PrimaryKey(t => t.IslandId)
                .ForeignKey("dbo.Ressources", t => t.AllRessources_RessourceId)
                .ForeignKey("dbo.Coordinates", t => t.Coordinates_CoordinateId)
                .ForeignKey("dbo.Players", t => t.Owner_PlayerId)
                .ForeignKey("dbo.Maps", t => t.Map_MapId)
                .Index(t => t.AllRessources_RessourceId)
                .Index(t => t.Coordinates_CoordinateId)
                .Index(t => t.Owner_PlayerId)
                .Index(t => t.Map_MapId);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        BuildingLevel_LevelId = c.Long(),
                        Island_IslandId = c.Int(),
                    })
                .PrimaryKey(t => t.BuildingId)
                .ForeignKey("dbo.Levels", t => t.BuildingLevel_LevelId)
                .ForeignKey("dbo.Islands", t => t.Island_IslandId)
                .Index(t => t.BuildingLevel_LevelId)
                .Index(t => t.Island_IslandId);
            
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        CoordinateId = c.Long(nullable: false, identity: true),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CoordinateId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        PlayerId = c.Long(nullable: false),
                        Name = c.String(),
                        Guild_GuildId = c.Long(),
                        World_WorldId = c.Long(),
                    })
                .PrimaryKey(t => t.PlayerId)
                .ForeignKey("dbo.Guilds", t => t.Guild_GuildId)
                .ForeignKey("dbo.Profils", t => t.PlayerId)
                .ForeignKey("dbo.Worlds", t => t.World_WorldId)
                .Index(t => t.PlayerId)
                .Index(t => t.Guild_GuildId)
                .Index(t => t.World_WorldId);
            
            CreateTable(
                "dbo.Guilds",
                c => new
                    {
                        GuildId = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        InternalMessage = c.String(),
                        Name = c.String(),
                        PublicMessage = c.String(),
                        Recrutement = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GuildId);
            
            CreateTable(
                "dbo.Profils",
                c => new
                    {
                        ProfilId = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Mail = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.ProfilId);
            
            CreateTable(
                "dbo.Technologies",
                c => new
                    {
                        TechnologyId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        TechnologyLevel_LevelId = c.Long(),
                        Player_PlayerId = c.Long(),
                    })
                .PrimaryKey(t => t.TechnologyId)
                .ForeignKey("dbo.Levels", t => t.TechnologyLevel_LevelId)
                .ForeignKey("dbo.Players", t => t.Player_PlayerId)
                .Index(t => t.TechnologyLevel_LevelId)
                .Index(t => t.Player_PlayerId);
            
            CreateTable(
                "dbo.Worlds",
                c => new
                    {
                        WorldId = c.Long(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.WorldId);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        MapId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.MapId)
                .ForeignKey("dbo.Worlds", t => t.MapId)
                .Index(t => t.MapId);
            
            CreateTable(
                "dbo.Regiments",
                c => new
                    {
                        RegimentId = c.Long(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Unit_UnitId = c.Long(),
                        Army_ArmyId = c.Long(),
                    })
                .PrimaryKey(t => t.RegimentId)
                .ForeignKey("dbo.Units", t => t.Unit_UnitId)
                .ForeignKey("dbo.Armies", t => t.Army_ArmyId)
                .Index(t => t.Unit_UnitId)
                .Index(t => t.Army_ArmyId);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        UnitId = c.Long(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                        UnitType = c.Int(nullable: false),
                        UnitDamageType = c.Int(nullable: false),
                        UnitCost_RessourceId = c.Long(),
                        UnitStatistics_UnitStatisticId = c.Int(),
                    })
                .PrimaryKey(t => t.UnitId)
                .ForeignKey("dbo.Ressources", t => t.UnitCost_RessourceId)
                .ForeignKey("dbo.UnitStatistics", t => t.UnitStatistics_UnitStatisticId)
                .Index(t => t.UnitCost_RessourceId)
                .Index(t => t.UnitStatistics_UnitStatisticId);
            
            CreateTable(
                "dbo.UnitStatistics",
                c => new
                    {
                        UnitStatisticId = c.Int(nullable: false, identity: true),
                        Attack = c.Int(nullable: false),
                        Capacity = c.Int(nullable: false),
                        Consumption = c.Int(nullable: false),
                        MagicResist = c.Int(nullable: false),
                        PhysicResist = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UnitStatisticId);
            
            CreateTable(
                "dbo.CombatReports",
                c => new
                    {
                        CombatReportId = c.Long(nullable: false, identity: true),
                        ObjectReport = c.String(),
                        Report = c.String(),
                        Receiver_PlayerId = c.Long(),
                    })
                .PrimaryKey(t => t.CombatReportId)
                .ForeignKey("dbo.Players", t => t.Receiver_PlayerId)
                .Index(t => t.Receiver_PlayerId);
            
            CreateTable(
                "dbo.GuildRoles",
                c => new
                    {
                        GuildRoleId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Rights = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GuildRoleId);
            
            CreateTable(
                "dbo.Mages",
                c => new
                    {
                        MageId = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Island_IslandId = c.Int(),
                        MageLevel_LevelId = c.Long(),
                    })
                .PrimaryKey(t => t.MageId)
                .ForeignKey("dbo.Islands", t => t.Island_IslandId)
                .ForeignKey("dbo.Levels", t => t.MageLevel_LevelId)
                .Index(t => t.Island_IslandId)
                .Index(t => t.MageLevel_LevelId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Long(nullable: false, identity: true),
                        MessageObject = c.String(),
                        CoreMessage = c.String(),
                        Receiver_PlayerId = c.Long(),
                        Sender_PlayerId = c.Long(),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Players", t => t.Receiver_PlayerId)
                .ForeignKey("dbo.Players", t => t.Sender_PlayerId)
                .Index(t => t.Receiver_PlayerId)
                .Index(t => t.Sender_PlayerId);
            
            CreateTable(
                "dbo.Spells",
                c => new
                    {
                        SpellId = c.Long(nullable: false, identity: true),
                        Description = c.String(),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.SpellId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "Sender_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Messages", "Receiver_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Mages", "MageLevel_LevelId", "dbo.Levels");
            DropForeignKey("dbo.Mages", "Island_IslandId", "dbo.Islands");
            DropForeignKey("dbo.Apprentices", "Mage_MageId", "dbo.Mages");
            DropForeignKey("dbo.Levels", "Cost_RessourceId", "dbo.Ressources");
            DropForeignKey("dbo.CombatReports", "Receiver_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Regiments", "Army_ArmyId", "dbo.Armies");
            DropForeignKey("dbo.Regiments", "Unit_UnitId", "dbo.Units");
            DropForeignKey("dbo.Units", "UnitStatistics_UnitStatisticId", "dbo.UnitStatistics");
            DropForeignKey("dbo.Units", "UnitCost_RessourceId", "dbo.Ressources");
            DropForeignKey("dbo.Players", "World_WorldId", "dbo.Worlds");
            DropForeignKey("dbo.Maps", "MapId", "dbo.Worlds");
            DropForeignKey("dbo.Islands", "Map_MapId", "dbo.Maps");
            DropForeignKey("dbo.Technologies", "Player_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Technologies", "TechnologyLevel_LevelId", "dbo.Levels");
            DropForeignKey("dbo.Players", "PlayerId", "dbo.Profils");
            DropForeignKey("dbo.Islands", "Owner_PlayerId", "dbo.Players");
            DropForeignKey("dbo.Players", "Guild_GuildId", "dbo.Guilds");
            DropForeignKey("dbo.Islands", "Coordinates_CoordinateId", "dbo.Coordinates");
            DropForeignKey("dbo.Buildings", "Island_IslandId", "dbo.Islands");
            DropForeignKey("dbo.Buildings", "BuildingLevel_LevelId", "dbo.Levels");
            DropForeignKey("dbo.Armies", "Island_IslandId", "dbo.Islands");
            DropForeignKey("dbo.Islands", "AllRessources_RessourceId", "dbo.Ressources");
            DropForeignKey("dbo.Apprentices", "ApprenticeLevel_LevelId", "dbo.Levels");
            DropIndex("dbo.Messages", new[] { "Sender_PlayerId" });
            DropIndex("dbo.Messages", new[] { "Receiver_PlayerId" });
            DropIndex("dbo.Mages", new[] { "MageLevel_LevelId" });
            DropIndex("dbo.Mages", new[] { "Island_IslandId" });
            DropIndex("dbo.CombatReports", new[] { "Receiver_PlayerId" });
            DropIndex("dbo.Units", new[] { "UnitStatistics_UnitStatisticId" });
            DropIndex("dbo.Units", new[] { "UnitCost_RessourceId" });
            DropIndex("dbo.Regiments", new[] { "Army_ArmyId" });
            DropIndex("dbo.Regiments", new[] { "Unit_UnitId" });
            DropIndex("dbo.Maps", new[] { "MapId" });
            DropIndex("dbo.Technologies", new[] { "Player_PlayerId" });
            DropIndex("dbo.Technologies", new[] { "TechnologyLevel_LevelId" });
            DropIndex("dbo.Players", new[] { "World_WorldId" });
            DropIndex("dbo.Players", new[] { "Guild_GuildId" });
            DropIndex("dbo.Players", new[] { "PlayerId" });
            DropIndex("dbo.Buildings", new[] { "Island_IslandId" });
            DropIndex("dbo.Buildings", new[] { "BuildingLevel_LevelId" });
            DropIndex("dbo.Islands", new[] { "Map_MapId" });
            DropIndex("dbo.Islands", new[] { "Owner_PlayerId" });
            DropIndex("dbo.Islands", new[] { "Coordinates_CoordinateId" });
            DropIndex("dbo.Islands", new[] { "AllRessources_RessourceId" });
            DropIndex("dbo.Armies", new[] { "Island_IslandId" });
            DropIndex("dbo.Levels", new[] { "Cost_RessourceId" });
            DropIndex("dbo.Apprentices", new[] { "Mage_MageId" });
            DropIndex("dbo.Apprentices", new[] { "ApprenticeLevel_LevelId" });
            DropTable("dbo.Spells");
            DropTable("dbo.Messages");
            DropTable("dbo.Mages");
            DropTable("dbo.GuildRoles");
            DropTable("dbo.CombatReports");
            DropTable("dbo.UnitStatistics");
            DropTable("dbo.Units");
            DropTable("dbo.Regiments");
            DropTable("dbo.Maps");
            DropTable("dbo.Worlds");
            DropTable("dbo.Technologies");
            DropTable("dbo.Profils");
            DropTable("dbo.Guilds");
            DropTable("dbo.Players");
            DropTable("dbo.Coordinates");
            DropTable("dbo.Buildings");
            DropTable("dbo.Islands");
            DropTable("dbo.Armies");
            DropTable("dbo.Ressources");
            DropTable("dbo.Levels");
            DropTable("dbo.Apprentices");
        }
    }
}

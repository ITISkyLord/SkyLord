using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface ILevelContext : ICustomContext
    {
        DbSet<Ressource> Ressources { get; set; }
        DbSet<Building> Buildings { get; set; }
        DbSet<Technology> Technologies { get; set; }
        DbSet<Level> Levels { get; set; }
        DbSet<BuildingLevel> BuildingLevels { get; set; }
        DbSet<TechnologyLevel> TechnologyLevels { get; set; }
        DbSet<Requirement> Requirements { get; set; }
        DbSet<Army> Armies { get; set; }
        DbSet<Regiment> Regiments { get; set; }
        DbSet<Unit> Units { get; set; }
        DbSet<Bonus> Bonuses { get; set; }
        DbSet<BonusOnBuilding> BonusesOnBuilding { get; set; }
        DbSet<BonusOnTechnology> BonusesOnTechnology { get; set; }
        DbSet<BonusOnUnit> BonusesOnUnit { get; set; }
    }
}

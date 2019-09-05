using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace ITI.SkyLord
{
    public class IslandServices
    {
        public IIslandContext currentContext { get; set; }

        public IslandServices(IIslandContext context)
        {
            currentContext = context;
        }

        /// <summary>
        /// Add Ressources in database
        /// </summary>
        /// <param name="typeRessource">The name of the ressource which are choosen</param>
        /// <param name="idPlayerActive">The id of the player which is connected</param>
        /// <param name="quantity">The quantity of ressource you want add</param>
        public void AddRessources(string typeRessource, long idPlayerActive, int quantity)
        {
            Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();

            switch (typeRessource)
            {
                case ("Cristal"):
                    ressource.AddCristal(quantity);
                    break;
                case ("Magic"):
                    ressource.AddMagic(quantity);
                    break;
                case ("Metal"):
                    ressource.AddMetal(quantity);
                    break;
                case ("Wood"):
                    ressource.AddWood(quantity);
                    break;
                default:
                    break;
            }
            currentContext.SaveChanges();
        }

        /// <summary>
        /// Add Ressources in database
        /// </summary>
        /// <param name="typeRessource">The name of the ressource which are choosen</param>
        /// <param name="idPlayerActive">The id of the player which is connected</param>
        /// <param name="quantity">The quantity of ressource you want withdraw</param>
        public void WithdrawRessource(string typeRessource, long idPlayerActive, int quantity)
        {
            Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();

            switch (typeRessource)
            {
                case ("Cristal"):
                    ressource.WithdrawCristal(quantity);
                    break;
                case ("Magic"):
                    ressource.WithdrawMagic(quantity);
                    break;
                case ("Metal"):
                    ressource.WithdrawMetal(quantity);
                    break;
                case ("Wood"):
                    ressource.WithdrawWood(quantity);
                    break;
                default:
                    break;
            }
            currentContext.SaveChanges();
        }

        public int DisplayCristal(long idPlayerActive)
        {
            Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();
            return ressource.Cristal;
        }

        public int DisplayWood(long idPlayerActive)
        {
            Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();
            return ressource.Wood;
        }

        public int DisplayMagic(long idPlayerActive)
        {
            Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();
            return ressource.Magic;
        }

        public int DisplayMetal(long idPlayerActive)
        {
            Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();
            return ressource.Metal;
        }

        public Ressource Ressource(long idPlayerActive)
        {
             Ressource ressource = currentContext.Islands.Include(r => r.AllRessources).Include(p => p.Owner).Where(i => i.Owner.PlayerId == idPlayerActive).Select(res => res.AllRessources).SingleOrDefault();
            return ressource;
        }
    }
}
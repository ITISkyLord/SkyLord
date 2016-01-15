﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts
{
    public interface IBuildingEventContext : IDisposable, IInfrastructure<IServiceProvider>
    {
        DbSet<BuildingEvent> BuildingEvents { get; set; }
        DbSet<UpgradeEvent> UpgradeEvents { get; set; }
        int SaveChanges();
    }
}

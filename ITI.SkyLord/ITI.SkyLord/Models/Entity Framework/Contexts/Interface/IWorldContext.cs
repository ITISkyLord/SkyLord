using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface IWorldContext : ICustomContext
    {
        DbSet<World> Worlds { get; set; }
        DbSet<Coordinate> Coordinates { get; set; }
    }
}

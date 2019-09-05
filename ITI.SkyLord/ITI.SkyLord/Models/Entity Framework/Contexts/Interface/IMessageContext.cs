using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    public interface IMessageContext : ICustomContext
    {
        DbSet<Message> Messages { get; set; }

    }
}

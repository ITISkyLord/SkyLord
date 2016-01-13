using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Models.Entity_Framework.Contexts.Interface
{
    interface ISuperInterfaceContext : 
        IArmyContext, 
        IArmyEventContext, 
        IBuildingEventContext, 
        ICustomContext, 
        IEventContext, 
        IGenericContext, 
        IIslandContext, 
        IMessageContext, 
        IPlayerContext, 
        IStandardContext, 
        ITechnologyEventContext, 
        IUnitEventContext, 
        IWorldContext,
        ILevelContext
    {
    }
}

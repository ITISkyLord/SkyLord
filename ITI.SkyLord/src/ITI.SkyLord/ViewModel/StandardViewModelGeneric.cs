using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.ViewModel
{
    public class StandardViewModel<T> : StandardViewModel
    {
        public StandardViewModel(IStandardContext context, int playerId, int currendIslandId) : base (context, playerId, currendIslandId) { }

        T SpecificViewModel { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

namespace ITI.SkyLord.ViewModel
{
    public class StandardViewModel<T> : StandardViewModel
    {
        public StandardViewModel(int playerId, int currendIslandId) : base (playerId, currendIslandId)
        {
        }


        T SpecificViewModel { get; set; }

    }
}

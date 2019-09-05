using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord.ViewModel
{
    public class StandardViewModel<T> : StandardViewModel
    {
        public StandardViewModel() : base () { }

        T SpecificViewModel { get; set; }

    }
}

using Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.SkyLord.DAL.Contexts.GameModel
{
   public partial class Apprentice
    {
        public long ApprenticeId { get; set; }
        public ApprenticeLevel ApprenticeLevel { get; set; }
    }
}

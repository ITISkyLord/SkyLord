using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Mage
    {
        #region Properties
        [Key]
        public long MageId { get; set; }

        public string Name { get; set; }

        public MageLevel MageLevel { get; set; }

        public IList<Apprentice> Apprentices { get; set; }

        public Island Island { get; set; }

        #endregion
    }
}
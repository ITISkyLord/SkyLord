
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITI.SkyLord
{
    public class Regiment
    {
        #region Properties
        [Key]
        public long RegimentId { get; set; }


        internal string Name { get; set; }

        public Unit Unit { get; set; }

        public int Number { get; set; }
        #endregion
    }
}

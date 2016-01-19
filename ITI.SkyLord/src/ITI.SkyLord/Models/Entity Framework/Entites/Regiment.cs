
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.SkyLord
{
    public class Regiment
    {
        #region Properties
        [Key]
        public long RegimentId { get; set; }

        public Unit Unit { get; set; }

        public int Number { get; set; }

        [Column("ArmyArmyId")]
        public long? ArmyId { get; set; }

        public Army Army { get; set; }
        #endregion
    }
}

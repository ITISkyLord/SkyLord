using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Apprentice
    {
        private ApprenticeLevel _level;
        public Apprentice()
        {
            this.Level = new ApprenticeLevel();
        }

        [Key]
        public long ApprenticeId { get; set; }

        public ApprenticeLevel Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level =  value ;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Profil
    {
        #region Properties
        [Key]
        public long ProfilId { get; set; }

        public string Description { get; set; }
        public string Mail { get; set; }
        public string SiteWeb { get; set; }
        #endregion
    }
}
using ITI.SkyLord.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModels.Profil
{
    public class ProfilViewModel : StandardViewModel
    {
        [Required]
        [StringLength( 1500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0 )]
        [DataType( DataType.Text )]
        [Display( Name = "Description" )]
        public string Description { get; set; }
        public string Mail { get; set; }
        public string SiteWeb { get; set; }
        public string Name { get; set; }
        public long PlayerId { get; set; }
        public long CurrentPlayerId { get; set; }


    }
}

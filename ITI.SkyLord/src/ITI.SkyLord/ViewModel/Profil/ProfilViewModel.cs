﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModels.Profil
{
    public class ProfilViewModel
    {
        [Required]
        [StringLength( 1500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 0 )]
        [DataType( DataType.Text )]
        [Display( Name = "Description" )]
        public string Description { get; set; }

       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Technology
    {

        #region Properties
        [Key]
        public long TechnologyId { get; set; }

        public string Name { get; set; }

        public TechnologyLevel Level { get; set; }
        #endregion

    }

    // Idées de techno
    //Augmenter la limite d’îles( le nombre max d’île possédée ) = limiter le nombre d’apprenti dans la troupe.
    //Augmenter la vitesse d’invocation
    //Réduire la consommation de magie
    //Accès aux troupes progressif (1 par type d’unité)
    //Amélioration de la vitesse de déplacement
    //Amélioration de l’attaque des unités
    //Amélioration de la défense des unités
    //Augmenter la charge des unités( sac sans fond)
}
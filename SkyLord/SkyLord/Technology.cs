using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord.Model
{
    public class Technology
    {
        // private int _id;
        private string _name;
        private TechnologyLevel _level;
        // Implémentation XML pour les avantages et description

        public Technology(string name )
        {
            if ( String.IsNullOrWhiteSpace( name ) ) throw new ArgumentException( "The technology name cannot be null or white space." );

            _name = name;
            _level = new TechnologyLevel();
        }

        #region Properties
        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public TechnologyLevel Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        } 
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
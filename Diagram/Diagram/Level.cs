using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public abstract class Level
    {
        private int _number;
        private Ressource _cost;
        public Level( int number = 1 )
        {
            _number = number;
        }
        public int Number
        {
            get
            {
                return _number;
            }

            protected set
            {
               _number = value;
            }
        }
        // Coût en ressources à voir avec le XML et la base de données.
        // Le reste ce sera des méthodes.

    }
}
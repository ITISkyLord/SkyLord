using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Mage
    {
        //private int _id;
        private MageLevel _mageLevel;
        private List<Apprentice> _apprentices;
        private Island _island;

        public Mage( Island island )
        {
            if( island == null ) throw new ArgumentNullException( "island is null" );
            _island = island;
            this._mageLevel = new MageLevel();
            this._apprentices = new List<Apprentice>();
        }

        public MageLevel MageLevel
        {
            get
            {
                return _mageLevel;
            }

            set
            {
                _mageLevel = value;
            }
        }
        public List<Apprentice> Apprentices
        {
            get
            {
                return _apprentices;
            }

            set
            {
                _apprentices = value;
            }
        }
    }
}
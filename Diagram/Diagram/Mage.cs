using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Mage
    {
        private MageLevel _mageLevel;
        private readonly List<Apprentice> _apprentices;
        private Island _island;
        private string _name;

        /// <summary>
        /// Create a new mage who is associated to an island.
        /// </summary>
        /// <param name="island">It's the island associated to the mage.</param>
        public Mage( Island island, string name )
        {
            if( island == null ) throw new ArgumentNullException( "island is null" );
            _island = island;
            this._mageLevel = new MageLevel();
            this._apprentices = new List<Apprentice>();
            if (name == null ) throw new ArgumentNullException("Name cannot be null.");
            if (name.Length < 4) throw new InvalidOperationException("Enter a name with more than 3 caracters.");
            _name = name;

        }

        #region Properties
        /// <summary>
        /// Gets or sets the MageLevel.
        /// </summary>
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

        /// <summary>
        /// Gets the list of apprentices of the mage.
        /// </summary>
        public List<Apprentice> Apprentices
        {
            get
            {
                return _apprentices;
            }
        } 
        #endregion
    }
}
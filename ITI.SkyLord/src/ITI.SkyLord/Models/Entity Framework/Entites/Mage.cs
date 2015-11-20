using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Mage
    {
        private long _mageId;
        private string _name;
        private MageLevel _mageLevel;
        private List<Apprentice> _apprentices;
        private Island _island;

        /// <summary>
        /// Create a new mage who is associated to an island.
        /// </summary>
        /// <param name="island">It's the island associated to the mage.</param>
        public Mage( Island island, string name )
        {
            if( island == null ) throw new ArgumentNullException( "island is null" );
            if( String.IsNullOrWhiteSpace( name ) ) throw new ArgumentNullException( "The name of the mage can not be null or a white space." );
            if( name.Count() < 4 ) throw new InvalidOperationException( "The name of the mage must be at least 4 characters" );
            Island = island;
            this._mageLevel = new MageLevel();
            this._apprentices = new List<Apprentice>();
            // TODO : générer un nom de mage rigolo
            this._name = "Mage";
            _name = name;

        }

        #region Properties
        /// <summary>
        /// Gets or sets the Name of the mage
        /// </summary>
        /// 
        [Key]
        public long MageId { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the MageLevel.
        /// </summary>
        public MageLevel MageLevel { get; set; }

        /// <summary>
        /// Gets the list of apprentices of the mage.
        /// </summary>
        public List<Apprentice> Apprentices { get; set; }

        public Island Island { get; set; }
        #endregion
    }
}
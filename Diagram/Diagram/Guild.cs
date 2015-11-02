using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Guild
    {
        private string _name;
        private readonly Dictionary<Player, GuildRole> _members;
        private string _description;
        private string _internalMessage;
        private string _publicMessage;
        private bool _recrutement;

        /// <summary>
        /// Create a new guild with a name.
        /// </summary>
        /// <param name="name">It's the name of the guild. Cannot be null or white space</param>
        public Guild( string name )
        {
            if ( String.IsNullOrWhiteSpace( name ) ) throw new ArgumentException( "The guild name cannot be empty or white space." );
            
            _name = name;
            _members = new Dictionary<Player, GuildRole>();
            _description = "Aucune description pour le moment...";
            _internalMessage = "Bienvenue dans la guilde " + name;
            _publicMessage = "Bienvenue dans la guilde " + name;
            _recrutement = false;
        }

        #region Properties

        /// <summary>
        /// Gets or sets the Name of the guild.
        /// </summary>
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
        /// Gets the dictionnary Player,GuildRole of the Members of the guild.
        /// </summary>
        public Dictionary<Player, GuildRole> Members
        {
            get
            {
                return _members;
            }
        }

        /// <summary>
        /// Gets or sets the description of the guild.
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }

        /// <summary>
        /// Gets or sets the internal message of the guild.
        /// </summary>
        public string InternalMessage
        {
            get
            {
                return _internalMessage;
            }

            set
            {
                _internalMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets the Public message of the guild.
        /// </summary>
        public string PublicMessage
        {
            get
            {
                return _publicMessage;
            }

            set
            {
                _publicMessage = value;
            }
        }

        /// <summary>
        /// Gets or sets if recrutement is active or inactive. True of false.
        /// </summary>
        public bool Recrutement
        {
            get
            {
                return _recrutement;
            }

            set
            {
                _recrutement = value;
            }
        } 
        #endregion
    }
}
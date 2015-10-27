using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Guild
    {
        // private int _id;
        private string _name;
        private Dictionary<Player, GuildRole> _members;
        private string _description;
        private string _internalMessage;
        private string _publicMessage;
        private bool _recrutement;

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

        public Dictionary<Player, GuildRole> Members
        {
            get
            {
                return _members;
            }

            set
            {
                _members = value;
            }
        }

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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Guild
    {
        private string _name;
        private  List<GuildMember> _members;
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
            
            Name = name;
            Members = new List<GuildMember>();
            Description = "Aucune description pour le moment...";
            InternalMessage = "Bienvenue dans la guilde " + name;
            PublicMessage = "Bienvenue dans la guilde " + name;
            Recrutement = false;
        }

        #region Properties
        [Key]
        public long GuildId { get; set; }

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

        public List<GuildMember> Members
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
                _recrutement =  value ;
            }
        }

        #endregion
    }
}
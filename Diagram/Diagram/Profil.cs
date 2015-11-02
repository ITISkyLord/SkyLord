using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Profil
    {
        private string _mail;
        private string _password;
        private readonly Player _owner;
        // Avatar
        private string _description;

        public Profil( Player player, string mail, string password )
        {
            if( !IsValidEmailStatic.IsValidEmail( mail ) ) throw new ArgumentException( "Invalid mail" );
            if( String.IsNullOrWhiteSpace( password ) ) throw new ArgumentNullException( "password is null or has white space." );
            if( player == null ) throw new ArgumentNullException();
            this._mail = mail;
            this._owner = player;
            this._password = password;
        }

        #region Properties
        public string Mail
        {
            get
            {
                return _mail;
            }

            set
            {
                _mail = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
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

        public Player Owner
        {
            get
            {
                return _owner;
            }
        } 
        #endregion
    }
}
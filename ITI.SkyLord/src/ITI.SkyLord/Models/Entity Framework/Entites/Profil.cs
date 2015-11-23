using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Profil
    {
        private string _mail;
        private string _password;
        private string _description;

        /// <summary>
        /// Creates a Profile.
        /// </summary>
        /// <param name="player">The player the profile belongs to.</param>
        /// <param name="mail">The mail of the player.</param>
        /// <param name="password">The password of the player.</param>
        public Profil( Player player, string mail, string password )
        {
            if( !IsValidEmailStatic.IsValidEmail( mail ) ) throw new ArgumentException( "Invalid mail" );
            if( String.IsNullOrWhiteSpace( password ) ) throw new ArgumentNullException( "password is null or has white space." );
            if( player == null ) throw new ArgumentNullException();
            this.Mail = mail;
            // this._owner = player;
            this.Password = password;
        }

        #region Properties
        [Key]
        public long ProfilId { get; set; }

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

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>


        #endregion
    }
}
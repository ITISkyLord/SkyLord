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
        public Profil( string mail = "toto@gmail.com", string password = "password" )
        {
            if( !IsValidEmailStatic.IsValidEmail( mail ) ) throw new ArgumentException( "Invalid mail" );
            if( String.IsNullOrWhiteSpace( password ) ) throw new ArgumentNullException( "password is null or has white space." );
            this.Mail = mail;
            // this._owner = player;
            this.Password = password;
        }

        /// <summary>
        /// Empty Constructor for EF
        /// </summary>
        public Profil()
        {
            _mail = "toto@gmail.com";
            _password = "password";
        }
        #region Properties
        [Key]
        public long ProfilId { get; set; }

        [EmailAddress]
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

        [StringLength( 60, MinimumLength = 3 ) ]
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
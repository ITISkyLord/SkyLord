using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Profil
    {
        private string _mail;
        private string _password;
        private readonly Player _owner;
        // Avatar
        private string _description;

        /// <summary>
        /// Creates a Profile.
        /// </summary>
        /// <param name="player">The player the profile belongs to.</param>
        /// <param name="mail">The mail of the player.</param>
        /// <param name="password">The password of the player.</param>
        public Profil(Player player, string mail, string password)
        {
            if (!IsValidEmailStatic.IsValidEmail(mail)) throw new ArgumentException("Invalid mail");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password is null or has white space.");
            if (player == null) throw new ArgumentNullException();
            this._mail = mail;
            this._owner = player;
            this._password = password;
        }

        //public Profil( string mail, string password)
        //{
        //    if (!IsValidEmailStatic.IsValidEmail(mail)) throw new ArgumentException("Invalid mail");
        //    if (String.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password is null or has white space.");
        //    this._mail = mail;
        //    this._password = password;
        //}

        #region Properties
        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
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
        /// <summary>
        /// Gets or sets the description of the player.
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
        /// Gets the player that this profile belongs to.
        /// </summary>
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
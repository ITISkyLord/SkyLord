using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Diagram
{
    public class Player
    {
        private readonly string _name;
        private readonly Profil _profil;
        private List<Island> _islands;
        private Guild _guild;
        private List<Technology> _techs;
        private bool invalid = false;


        public Player( string name, string mail = "toto@gmail.com", string password = "password" )
        {
            if( String.IsNullOrWhiteSpace( name ) ) throw new ArgumentNullException( "name is null or has white space" );
            if( String.IsNullOrWhiteSpace( password ) ) throw new ArgumentNullException( "password is null or has white space" );
            if( !IsValidEmail( mail ) ) throw new ArgumentException( "Invalid mail" );
            if( name.Length > 50 ) throw new ArgumentOutOfRangeException( " Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment." );
            if( password.Length > 75 ) throw new ArgumentOutOfRangeException( " Maximum lenght of password is 75. Your name lenght is " + password.Length + " at the moment." );
            this._name = name;
            this._profil = new Profil(mail, password);
            this._islands = new List<Island>();
            this._techs = new List<Technology>();
            
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }
        public Profil Profil
        {
            get
            {
                return _profil;
            }
        }
        public List<Island> Islands
        {
            get
            {
                return _islands;
            }

            set
            {
                _islands = value;
            }
        }
        public Guild Guild
        {
            get
            {
                return _guild;
            }

            set
            {
                _guild = value;
            }
        }
        public List<Technology> Techs
        {
            get
            {
                return _techs;
            }

            set
            {
                _techs = value;
            }
        }

        #region IsValidEmail
        private bool IsValidEmail( string strIn )
        {
            invalid = false;
            if( String.IsNullOrEmpty( strIn ) )
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace( strIn, @"(@)(.+)$", this.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds( 200 ) );
            }
            catch( RegexMatchTimeoutException )
            {
                return false;
            }

            if( invalid )
                return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch( strIn,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds( 250 ) );
            }
            catch( RegexMatchTimeoutException )
            {
                return false;
            }
        }
        private string DomainMapper( Match match )
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii( domainName );
            }
            catch( ArgumentException )
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        } 
        #endregion
    }
}

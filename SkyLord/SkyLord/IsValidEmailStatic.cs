﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ITI.SkyLord.Model
{
    public static class IsValidEmailStatic
    {
        static bool _invalid = false;

        public static bool IsValidEmail( string strIn )
        {
            _invalid = false;
            if( String.IsNullOrEmpty( strIn ) )
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace( strIn, @"(@)(.+)$", IsValidEmailStatic.DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds( 200 ) );
            }
            catch( RegexMatchTimeoutException )
            {
                return false;
            }

            if( _invalid )
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
        private static string DomainMapper( Match match )
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
                _invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}

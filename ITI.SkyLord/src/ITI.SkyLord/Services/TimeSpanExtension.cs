using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Services
{
    public static class TimeSpanExtension
    {
        public static string CustomToString( this TimeSpan timeSpan )
        {
            if ( timeSpan.TotalMinutes < 1 ) return timeSpan.ToString( "ss\\s" );
            if ( timeSpan.TotalHours < 1 ) return timeSpan.ToString( "mm\\mss\\s" );
            if ( timeSpan.TotalDays < 1 ) return timeSpan.ToString( "hh\\hmm\\mss\\s" );
            else return timeSpan.ToString( "dd\\jhh\\hmm\\mss\\s" );
        }
    }
}

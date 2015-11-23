using ITI.SkyLord.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITI.SkyLord
{
    public class RegimentList : List<Regiment>
    {

        [Key]
        public long RegimentListId { get; set; }

        public void Add( Unit unit, int number )
        {
            Add( new Regiment( unit, number ) );
        }

        //private Regiment GetAgregatedRegiment( Unit unit, int number )
        //{
        //    if ( this.Any( r => r.Unit == unit ) )
        //    {
        //        int initialUnitNumber = this.Where( r => r.Unit == unit ).Select(r => r.Number).SingleOrDefault();
        //        int finalUnitNumber = initialUnitNumber + number;

        //        this.Remove( this.Where( r => r.Unit == unit ).SingleOrDefault() );
        //        return new Regiment( unit, finalUnitNumber );
        //    }
        //    else
        //        return new Regiment( unit, number );
        //}

    }
}

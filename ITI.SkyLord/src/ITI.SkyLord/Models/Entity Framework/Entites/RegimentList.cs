
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
    }
}

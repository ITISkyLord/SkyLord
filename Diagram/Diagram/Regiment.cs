using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class Regiment 
    {
        Dictionary<string, int> _units;
        Army _army;

        public Regiment()
        {
            _units = new Dictionary<string, int>();
        }
        public Dictionary<string, int> Units
        {
            get
            {
                return _units;
            }

            set
            {
                _units = value;
            }
        }

        public Army Army
        {
            get
            {
                return _army;
            }

            set
            {
                _army = value;
            }
        }
    }
}

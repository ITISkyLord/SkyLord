using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Diagram
{

    public struct Coordinate
    {
        private readonly int _x;
        private readonly int _y;
        
        public Coordinate( int x, int y )
        {
            this._x = x;
            this._y = y;
        }

        public int X
        {
            get
            {
                return _x;
            }
        }
        public int Y
        {
            get
            {
                return _y;
            }
        }
    }
}
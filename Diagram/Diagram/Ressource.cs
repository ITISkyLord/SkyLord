using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Ressource
    {
        private int _wood;
        private int _metal;
        private int _cristal;
        private int _magic;

        public Ressource( int wood, int metal, int cristal, int magic )
        {
            if( wood < 0 || metal < 0 || cristal < 0 || magic < 0) throw new ArgumentOutOfRangeException( "Ressources cannot be under 0." );
            this._wood = wood;
            this._metal = metal;
            this._cristal = cristal;
            this._magic = magic;
        }

        #region Properties
        public int Wood
        {
            get
            {
                return _wood;
            }
        }

        public int Metal
        {
            get
            {
                return _metal;
            }
        }

        public int Cristal
        {
            get
            {
                return _cristal;
            }
        }

        public int Magic
        {
            get
            {
                return _magic;
            }
        }
        #endregion
    }
}
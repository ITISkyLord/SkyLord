using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class CombatResult
    {
        Island _winningIsland;
        Island _loosingIsland;
        Ressource _pillagedRessources;
        Army _winningArmy;
        Army _loosingArmy;
        double _ratioPhysic;
        double _ratioMagic;

        #region Properties
        public Island WinningIsland
        {
            get
            {
                return _winningIsland;
            }
        }

        public Island LoosingIsland
        {
            get
            {
                return _loosingIsland;
            }
        }

        public Ressource PillagedRessources
        {
            get
            {
                return _pillagedRessources;
            }
        }

        public Army AttackingArmy
        {
            get
            {
                return _winningArmy;
            }
        }

        public Army DefendingArmy
        {
            get
            {
                return _loosingArmy;
            }
        } 
        #endregion

        public CombatResult( Army attackingArmy, Army defendingArmy, double ratioPhysic, double ratioMagic )
        {
            // result est null egalité
            this._winningArmy = attackingArmy;
            this._loosingArmy = defendingArmy;
            this._winningIsland = attackingArmy.Island;
            this._loosingIsland = defendingArmy.Island;
            this._ratioPhysic = ratioPhysic;
            this._ratioMagic = ratioMagic;
        }

        internal Army Resolve()
        {
            double result = _winningArmy.Regiments.Values.First() * Math.Pow( _ratioPhysic , 1.5);
            int regiment = _winningArmy.Regiments.Values.First();
            regiment -= (int)result;
            _winningArmy.Regiments.Remove( _winningArmy.Regiments.Keys.First() );
            _winningArmy.Regiments.Add( new Warrior(), regiment );
            Console.WriteLine( "Regiment = " + regiment );
            Console.WriteLine( " result = " + result );
            return _winningArmy;
        }



        // Renvoie les armées avec pertes et/ou ce qui reste dedans
        // Renvoi qui gagne et qui perd
        // Ressources pillées
        // Bâtiments/Défenses détruites, abîmées.
    }
}

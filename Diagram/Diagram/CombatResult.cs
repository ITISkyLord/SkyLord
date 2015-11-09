using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diagram
{
    public class CombatResult
    {
        Ressource _pillagedRessources;
        Army _winningArmy;
        Army _loosingArmy;

        #region Properties
        
        public Ressource PillagedRessources
        {
            get
            {
                return _pillagedRessources;
            }
        }

        public Army WinningArmy
        {
            get
            {
                return _winningArmy;
            }
        }

        public Army LoosingArmy
        {
            get
            {
                return _loosingArmy;
            }
        } 
        #endregion

        public CombatResult( Army winningArmy, Army loosingArmy )
        {
            if ( winningArmy == null ) throw new ArgumentNullException( "Winning Army", "The winning army cannot be null" );
            if ( loosingArmy == null ) throw new ArgumentNullException( "Loosing Army", "The loosing army cannot be null" );

            this._winningArmy = winningArmy;
            this._loosingArmy = loosingArmy;
            if( winningArmy.ArmyState == ArmyState.movement )
                _pillagedRessources = CalculatePillagedResult();
            else
                _pillagedRessources = null;
            // _pillagedRessources = winningArmy.GetPillagedRessources;
        }

        private Ressource CalculatePillagedResult()
        {
            int wood;
            int metal;
            int cristal;
            int magic;
            int capacityOfPillaged = 1;
            
            foreach( Regiment r in _winningArmy.Regiments )
            {
                capacityOfPillaged = r.Unit.UnitStatistics.Capacity * r.Number;
            }
            Ressource ressources = new Ressource(capacityOfPillaged/4, capacityOfPillaged/4, capacityOfPillaged/4, capacityOfPillaged/4);
            _loosingArmy.Island.AllRessources.ChangeRessources( ressources, false );
            _winningArmy.Island.AllRessources.ChangeRessources( ressources);
            return ressources;

        }

        // Renvoie les armées avec pertes et/ou ce qui reste dedans
        // Renvoi qui gagne et qui perd
        // Ressources pillées
        // Bâtiments/Défenses détruites, abîmées.
    }
}

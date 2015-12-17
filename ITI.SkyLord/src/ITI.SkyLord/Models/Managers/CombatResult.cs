﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;

namespace ITI.SkyLord
{
    public class CombatResult
    {
        Ressource _pillagedRessources;
        Army _winningArmy;
        Army _loosingArmy;
        CombatManager _combatManager;
        int _capacityOfPillaged;
        int _flag = 4;
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

        public int CapacityOfPillaged
        {
            get
            {
                return _capacityOfPillaged;
            }

        }
        #endregion

        public CombatResult( Army winningArmy, Army loosingArmy, CombatManager cm )
        {
            if( winningArmy == null ) throw new ArgumentNullException( "Winning Army", "The winning army cannot be null" );
            if( loosingArmy == null ) throw new ArgumentNullException( "Loosing Army", "The loosing army cannot be null" );

            _combatManager = cm;

            this._winningArmy = winningArmy;
            this._loosingArmy = loosingArmy;
            if( winningArmy.ArmyState == ArmyState.movement )
            {
                _pillagedRessources = CalculatePillagedResult();

                //Island isl1 = cm.ArmyManager.CurrentContext.Islands.Where(i=> i.IslandId == winningArmy.Island.IslandId).SingleOrDefault();
                //Island isl2 = cm.ArmyManager.CurrentContext.Islands.Where(i=> i.IslandId == loosingArmy.Island.IslandId).SingleOrDefault();
                //isl1.AllRessources = _winningArmy.Island.AllRessources;
                //isl2.AllRessources = _loosingArmy.Island.AllRessources;

                //cm.ArmyManager.CurrentContext.SaveChanges();

            }
            else
                _pillagedRessources = null;
        }

        private Ressource CalculatePillagedResult()
        {
            int eachCapacityPillaged = 0;
            Ressource ressources;

            foreach( Regiment r in _winningArmy.Regiments )
            {
                _capacityOfPillaged += r.Unit.UnitStatistics.Capacity * r.Number;
            }

            eachCapacityPillaged = _capacityOfPillaged / 4;
            ressources = new Ressource { Wood = eachCapacityPillaged, Metal = eachCapacityPillaged, Cristal = eachCapacityPillaged, Magic = eachCapacityPillaged };

            Ressource newRessources = recursivite();
            if( ressources == newRessources )
            {
                _winningArmy.Island.AllRessources.ChangeRessources( ressources );
                _loosingArmy.Island.AllRessources.ChangeRessources( ressources, false );
            }
            else if( _flag > 0 )
            {

                eachCapacityPillaged = _capacityOfPillaged / _flag;
                if( eachCapacityPillaged < _loosingArmy.Island.AllRessources.Wood )
                {
                    _loosingArmy.Island.AllRessources.ChangeWood( -eachCapacityPillaged );
                    _winningArmy.Island.AllRessources.ChangeWood( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingArmy.Island.AllRessources.Metal )
                {
                    _loosingArmy.Island.AllRessources.ChangeMetal( -eachCapacityPillaged );
                    _winningArmy.Island.AllRessources.ChangeMetal( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingArmy.Island.AllRessources.Cristal )
                {
                    _loosingArmy.Island.AllRessources.ChangeCristal( -eachCapacityPillaged );
                    _winningArmy.Island.AllRessources.ChangeCristal( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingArmy.Island.AllRessources.Magic )
                {
                    _loosingArmy.Island.AllRessources.ChangeMagic( -eachCapacityPillaged );
                    _winningArmy.Island.AllRessources.ChangeMagic( eachCapacityPillaged );
                }
            }

            //Console.WriteLine( "Après : _loosingArmy wood : " + _loosingArmy.Island.AllRessources.Wood );
            //Console.WriteLine( "Après : _loosingArmy metal : " + _loosingArmy.Island.AllRessources.Metal );
            //Console.WriteLine( "Après : _loosingArmy cristal : " + _loosingArmy.Island.AllRessources.Cristal );
            //Console.WriteLine( "Après : _loosingArmy magic : " + _loosingArmy.Island.AllRessources.Magic );
            //Console.WriteLine();
            //Console.WriteLine( "Après : _winningArmy wood : " + _winningArmy.Island.AllRessources.Wood );
            //Console.WriteLine( "Après : _winningArmy metal : " + _winningArmy.Island.AllRessources.Metal );
            //Console.WriteLine( "Après : _winningArmy cristal : " + _winningArmy.Island.AllRessources.Cristal );
            //Console.WriteLine( "Après : _winningArmy magic : " + _winningArmy.Island.AllRessources.Magic );
            //Console.WriteLine();

            return ressources;

        }
        private Ressource recursivite()
        {

            int eachCapacityPillaged = _capacityOfPillaged / 4;
            Ressource ressources = new Ressource { Wood = eachCapacityPillaged, Metal = eachCapacityPillaged, Cristal = eachCapacityPillaged, Magic = eachCapacityPillaged };

            int rest = 0;
            if( ressources.Wood > _loosingArmy.Island.AllRessources.Wood && _loosingArmy.Island.AllRessources.Wood > 1 )
            {
                rest = FindRest( "wood", ressources );
                _winningArmy.Island.AllRessources.ChangeWood( _loosingArmy.Island.AllRessources.Wood );
                _loosingArmy.Island.AllRessources.ChangeWood( -(_loosingArmy.Island.AllRessources.Wood) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            if( ressources.Metal > _loosingArmy.Island.AllRessources.Metal && _loosingArmy.Island.AllRessources.Metal > 1 )
            {
                rest = FindRest( "metal", ressources );
                _winningArmy.Island.AllRessources.ChangeMetal( _loosingArmy.Island.AllRessources.Metal );
                _loosingArmy.Island.AllRessources.ChangeMetal( -(_loosingArmy.Island.AllRessources.Metal) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;

            }
            if( ressources.Cristal > _loosingArmy.Island.AllRessources.Cristal && _loosingArmy.Island.AllRessources.Cristal > 1 )
            {
                rest = FindRest( "cristal", ressources );
                _winningArmy.Island.AllRessources.ChangeCristal( _loosingArmy.Island.AllRessources.Cristal );
                _loosingArmy.Island.AllRessources.ChangeCristal( -(_loosingArmy.Island.AllRessources.Cristal) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            if( ressources.Magic > _loosingArmy.Island.AllRessources.Magic && _loosingArmy.Island.AllRessources.Magic > 1 )
            {
                rest = FindRest( "magic", ressources );
                _winningArmy.Island.AllRessources.ChangeMagic( _loosingArmy.Island.AllRessources.Magic );
                _loosingArmy.Island.AllRessources.ChangeMagic( -(_loosingArmy.Island.AllRessources.Magic) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            return ressources;
        }

        private int FindRest( string ressourceName, Ressource ressource )
        {
            int rest = 0;
            if( ressourceName == "wood" )
            {
                rest = (_loosingArmy.Island.AllRessources.Wood - ressource.Wood);
            }
            else if( ressourceName == "metal" )
            {
                rest = (_loosingArmy.Island.AllRessources.Metal - ressource.Metal);

            }
            else if( ressourceName == "cristal" )
            {
                rest = (_loosingArmy.Island.AllRessources.Cristal - ressource.Cristal);

            }
            else if( ressourceName == "magic" )
            {
                rest = (_loosingArmy.Island.AllRessources.Magic - ressource.Magic);
            }

            return -rest;
        }

    }
}

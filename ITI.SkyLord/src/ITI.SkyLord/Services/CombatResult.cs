using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.Models.Entity_Framework.Entites.Events;
using ITI.SkyLord.Services;

namespace ITI.SkyLord
{
    public class CombatResult
    {
        Ressource _pillagedRessources;
        Message _combatReportWinner;
        Message _combatReportLooser;
        Ressource _winningRessource;
        Ressource _loosingRessource;
        private ArmyEvent _armyEvent;
        Army _winningArmy;
        Army _loosingArmy;
        CombatManager _combatManager;
        int _capacityOfPillaged;
        int _flag = 4;
        RessourceManager _ressourceManager;
        #region Properties

        public Ressource PillagedRessources
        {
            get
            {
                return _pillagedRessources;
            }
        }

        private RessourceManager RessourceManager
        {
            get
            {
                return _ressourceManager;
            }
        }

        public Message CombatReportWinner
        {
            get
            {
                return _combatReportWinner;
            }
        }
        public Message CombatReportLooser
        {
            get
            {
                return _combatReportWinner;
            }
        }

        private SetupContext _context { get; set; }

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

        public CombatResult( Army winningArmy, Army loosingArmy, CombatManager cm, ArmyEvent ae, SetupContext ctx )
        {
            if( winningArmy == null ) throw new ArgumentNullException( "Winning Army", "The winning army cannot be null" );
            if( loosingArmy == null ) throw new ArgumentNullException( "Loosing Army", "The loosing army cannot be null" );
            _context = ctx;
            _combatManager = cm;
            EventPackManager epm = new EventPackManager(ctx);
            _ressourceManager = epm.RessourceManager;
            _winningRessource = _ressourceManager.CloneRessource( winningArmy.Island.AllRessources );
            _loosingRessource = _ressourceManager.CloneRessource( loosingArmy.Island.AllRessources );


            this._winningArmy = winningArmy;
            this._loosingArmy = loosingArmy;
            if( winningArmy.ArmyState == ArmyState.movement )
            {
                _pillagedRessources = CalculatePillagedResult();
                if( _pillagedRessources == null )
                    _pillagedRessources = new Ressource();

            }
            else
                _pillagedRessources = null;

            //ae.PillagedRessources = new Ressource() { Cristal = 100, Magic = 100, Metal = 100, Wood = 100 };

            ctx.Ressources.Add( _pillagedRessources );
            ae.PillagedRessources = _pillagedRessources;
            ctx.SaveChanges();

            #region CombatReport
            string coreMessageWinner = "";
            string coreMessageLooser = "";

            if( _pillagedRessources == null && winningArmy.ArmyState == ArmyState.movement )
            {
                coreMessageWinner = _winningArmy.Island.Name + " (vous) a gagné contre " + _loosingArmy.Island.Name + ".\n "
                + _winningArmy.Island.Name + " n'a pillé aucune ressource."
                + _loosingArmy.Island.Name + " a perdu toutes ses troupes et " + _winningArmy.Island.Name + " (vous) a perdu : ";

                coreMessageLooser = _loosingArmy.Island.Name + " (vous) a perdu contre " + _winningArmy.Island.Name + ".\n Mais "
                + _winningArmy.Island.Name + " n'a pillé aucune ressource."
                + " Vous avez perdu toutes vos troupes et " + _winningArmy.Island.Name + " a perdu : ";
            }
            else if( winningArmy.ArmyState == ArmyState.movement )
            {
                coreMessageWinner = _winningArmy.Island.Name + " a gagné contre " + _loosingArmy.Island.Name + ".\n"
                + _winningArmy.Island.Name + " a pillé " + _pillagedRessources.Wood + " bois, " + _pillagedRessources.Metal + " métal, " + _pillagedRessources.Cristal + " cristal et " + _pillagedRessources.Wood + " magie."
                + _loosingArmy.Island.Name + " a perdu toutes ses troupes en défenses et " + _winningArmy.Island.Name + " a perdu : ";

                coreMessageLooser = _loosingArmy.Island.Name + " a perdu contre " + _winningArmy.Island.Name + ".\n"
                + _winningArmy.Island.Name + " a pillé " + _pillagedRessources.Wood + " bois, " + _pillagedRessources.Metal + " métal, " + _pillagedRessources.Cristal + " cristal et " + _pillagedRessources.Wood + " magie."
                + _loosingArmy.Island.Name + " a perdu toutes ses troupes en défenses et " + _winningArmy.Island.Name + " a perdu : ";
            }
            else
            {
                coreMessageWinner = _winningArmy.Island.Name + " (vous) a gagné contre " + _loosingArmy.Island.Name + ".\n "
                + _loosingArmy.Island.Name + " a perdu toutes ses troupes et " + _winningArmy.Island.Name + " (vous) a perdu : ";

                coreMessageLooser = _loosingArmy.Island.Name + " (vous) a perdu contre " + _winningArmy.Island.Name + ".\n "
                 + _loosingArmy.Island.Name + " a perdu toutes ses troupes et " + _winningArmy.Island.Name + " (vous) a perdu : ";
            }


            foreach( KeyValuePair<string, int> kvp in cm.Loss )
            {
                coreMessageWinner += " \n" + kvp.Value + " " + kvp.Key + " sur " + _winningArmy.Regiments.Where(a => a.Unit.Name == kvp.Key).Select( b => b.Number.ToString()).First();
            }

            coreMessageWinner += ".";
            _combatReportWinner = new Message()
            {
                MessageObject = _winningArmy.Island.Name + "a gagné contre " + _loosingArmy.Island.Name + ".",
                Read = false,
                isCombatReport = true,
                Sender = _loosingArmy.Island.Owner,
                Receiver = _winningArmy.Island.Owner,
                CoreMessage = coreMessageWinner
            }; 
            #endregion


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
            if( ressources.Equals( newRessources ) )
            {
                //   _context.SaveChanges();
                RessourceManager.RemoveRessource( _loosingArmy.Island.AllRessources, _loosingRessource );
               // _loosingRessource.ChangeRessources( ressources, false );
                _loosingArmy.Island.AllRessources = _loosingRessource;
            }
            else if( _flag > 0 )
            {

                eachCapacityPillaged = _capacityOfPillaged / _flag;
                if( eachCapacityPillaged < _loosingRessource.Wood )
                {
                    _loosingRessource.AddWood( -eachCapacityPillaged );
                    _winningRessource.AddWood( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingRessource.Metal )
                {
                    _loosingRessource.AddMetal( -eachCapacityPillaged );
                    _winningRessource.AddMetal( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingRessource.Cristal )
                {
                    _loosingRessource.AddCristal( -eachCapacityPillaged );
                    _winningRessource.AddCristal( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingRessource.Magic )
                {
                    _loosingRessource.AddMagic( -eachCapacityPillaged );
                    _winningRessource.AddMagic( eachCapacityPillaged );
                }

                ressources = null;
            }
            return ressources;

        }
        private Ressource recursivite()
        {

            int eachCapacityPillaged = _capacityOfPillaged / 4;
            Ressource ressources = new Ressource { Wood = eachCapacityPillaged, Metal = eachCapacityPillaged, Cristal = eachCapacityPillaged, Magic = eachCapacityPillaged };

            int rest = 0;
            if( ressources.Wood > _loosingRessource.Wood && _loosingRessource.Wood > 1 )
            {
                rest = FindRest( "wood", ressources );
                _winningRessource.AddWood( _loosingRessource.Wood );
                _loosingRessource.AddWood( -( _loosingRessource.Wood ) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            if( ressources.Metal > _loosingRessource.Metal && _loosingRessource.Metal > 1 )
            {
                rest = FindRest( "metal", ressources );
                _winningRessource.AddMetal( _loosingRessource.Metal );
                _loosingRessource.AddMetal( -( _loosingRessource.Metal ) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;

            }
            if( ressources.Cristal > _loosingRessource.Cristal && _loosingRessource.Cristal > 1 )
            {
                rest = FindRest( "cristal", ressources );
                _winningRessource.AddCristal( _loosingRessource.Cristal );
                _loosingRessource.AddCristal( -( _loosingRessource.Cristal ) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            if( ressources.Magic > _loosingRessource.Magic && _loosingRessource.Magic > 1 )
            {
                rest = FindRest( "magic", ressources );
                _winningRessource.AddMagic( _loosingRessource.Magic );
                _loosingRessource.AddMagic( -( _loosingRessource.Magic ) );
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
                rest = (_loosingRessource.Wood - ressource.Wood);
            }
            else if( ressourceName == "metal" )
            {
                rest = (_loosingRessource.Metal - ressource.Metal);

            }
            else if( ressourceName == "cristal" )
            {
                rest = (_loosingRessource.Cristal - ressource.Cristal);

            }
            else if( ressourceName == "magic" )
            {
                rest = (_loosingRessource.Magic - ressource.Magic);
            }

            return -rest;
        }

    }
}

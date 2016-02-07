﻿using System;
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
        Army _winningArmy;
        Army _loosingArmy;
        CombatManager _combatManager;
        int _capacityOfPillaged;
        int _flag;
        #region Properties

        public Ressource PillagedRessources
        {
            get
            {
                return _pillagedRessources;
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
                return _combatReportLooser;
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
        public CombatResult( Army sendingArmy, ArmyEvent ae, SetupContext ctx )
        {
            _context = ctx;
            EventPackManager epm = new EventPackManager(ctx);
            RessourceManager.AddRessource( ae.Destination.AllRessources, ae.PillagedRessources );
            string coreMessageWinner = "";
            coreMessageWinner = ae.Destination.Name + " a reçu les ressources : " + ae.PillagedRessources.Wood + " bois, " + ae.PillagedRessources.Metal + " métal, " + ae.PillagedRessources.Cristal + " cristal et " + ae.PillagedRessources.Wood + " magie depuis l'île : " + sendingArmy.Island.Name;
            _combatReportWinner = new Message()
            {
                MessageObject = ae.Destination.Name + " a reçu les ressources de " + sendingArmy.Island.Name + ".",
                Read = false,
                isCombatReport = true,
                Sender = ae.Island.Owner,
                Receiver = ae.Island.Owner,
                CoreMessage = coreMessageWinner,
                DateSent = DateTime.Now
            };
        }
        public CombatResult( Army winningArmy, Army loosingArmy, CombatManager cm, ArmyEvent ae, SetupContext ctx, Army tmpWinArmy, Army tmpLooseArmy )
        {
            if( winningArmy == null ) throw new ArgumentNullException( "Winning Army", "The winning army cannot be null" );
            if( loosingArmy == null ) throw new ArgumentNullException( "Loosing Army", "The loosing army cannot be null" );
            _context = ctx;
            _combatManager = cm;
            _flag = 4;

            _pillagedRessources = new Ressource();
            EventPackManager epm = new EventPackManager(ctx);
            _winningRessource = RessourceManager.CloneRessource( winningArmy.Island.AllRessources );
            _loosingRessource = RessourceManager.CloneRessource( loosingArmy.Island.AllRessources );


            this._winningArmy = winningArmy;
            this._loosingArmy = loosingArmy;
            if( winningArmy.ArmyState == ArmyState.movement )
            {
                CalculatePillagedResult();

                //ae.PillagedRessources = new Ressource() { Cristal = 100, Magic = 100, Metal = 100, Wood = 100 };
                if( _pillagedRessources != null )
                {
                    ctx.Ressources.Add( _pillagedRessources );
                    ctx.SaveChanges();
                    ae.PillagedRessources = _pillagedRessources;
                    RessourceManager.RemoveRessource( _loosingArmy.Island.AllRessources, _pillagedRessources );

                    _winningArmy.Island.Owner.RessourcePillaged +=  RessourceManager.GetRessourcesSumForHighscores(_pillagedRessources);
                }
            }
            else _pillagedRessources = null;
             
                #region Highscores Managment
                _winningArmy.Island.Owner.NumberOfVictory++;
                #endregion


            #region CombatReport
            string coreMessageWinner = "";
            string coreMessageLooser = "";
            if( String.IsNullOrEmpty( _loosingArmy.Island.Name ) )
                _loosingArmy.Island.Name = "Île sauvage";
            if( winningArmy.ArmyState == ArmyState.movement )
            {
                coreMessageWinner = _winningArmy.Island.Name + " a gagné contre " + _loosingArmy.Island.Name + ".\n "
                + _winningArmy.Island.Name + " a pillé " + _pillagedRessources.Wood + " bois, " + _pillagedRessources.Metal + " métaux, " + _pillagedRessources.Cristal + " cristaux et " + _pillagedRessources.Wood + " magie. ";

                coreMessageLooser = _loosingArmy.Island.Name + " a perdu contre " + _winningArmy.Island.Name + ".\n "
                + _winningArmy.Island.Name + " a pillé " + _pillagedRessources.Wood + " bois, " + _pillagedRessources.Metal + " métaux, " + _pillagedRessources.Cristal + " cristaux et " + _pillagedRessources.Wood + " magie. ";
            }
            else
            {
                coreMessageWinner = _winningArmy.Island.Name + " (vous) a gagné contre " + _loosingArmy.Island.Name + ".\n ";

                coreMessageLooser = _loosingArmy.Island.Name + " (vous) a perdu contre " + _winningArmy.Island.Name + ".\n ";
            }

            coreMessageWinner += _loosingArmy.Island.Name + " a perdu toutes ses troupes soit : ";
            coreMessageLooser += _loosingArmy.Island.Name + " (vous) a perdu toutes ses troupes soit : ";
            if( tmpLooseArmy != null && tmpLooseArmy.Regiments.Count > 0 )
            {
                foreach( Regiment regiment in tmpLooseArmy.Regiments )
                {
                    coreMessageWinner += " \n" + regiment.Number + "    ";
                    coreMessageLooser += " \n" + regiment.Number + regiment.Number + "    ";

                }
            } else
            {
                coreMessageWinner += " \n aucune unité. ";
                coreMessageLooser += " \n aucune unité. ";
            }


            coreMessageWinner += "et " + _winningArmy.Island.Name + " (vous) a perdu : ";
            coreMessageLooser += "et " + _winningArmy.Island.Name + " a perdu : ";
            foreach( KeyValuePair<string, int> kvp in cm.Loss )
            {
                coreMessageWinner += " \n" + kvp.Value + " " + kvp.Key + " sur " + tmpWinArmy.Regiments.Where( a => a.Unit.Name == kvp.Key ).Select( b => b.Number.ToString() ).First() + "    ";
                coreMessageLooser += " \n" + kvp.Value + " " + kvp.Key + " sur " + tmpWinArmy.Regiments.Where( a => a.Unit.Name == kvp.Key ).Select( b => b.Number.ToString() ).First() + "    ";

            }

            coreMessageWinner += ".";
            _combatReportWinner = new Message()
            {
                MessageObject = _winningArmy.Island.Name + " (vous) a gagné contre " + _loosingArmy.Island.Name + ".",
                Read = false,
                isCombatReport = true,
                Sender = _winningArmy.Island.Owner,
                Receiver = _winningArmy.Island.Owner,
                CoreMessage = coreMessageWinner,
                DateSent = DateTime.Now
            };
            coreMessageLooser += ".";
            _combatReportLooser = new Message()
            {
                MessageObject = _loosingArmy.Island.Name + " (vous) a perdu contre " + _winningArmy.Island.Name + ".",
                Read = false,
                isCombatReport = true,
                Sender = _loosingArmy.Island.Owner,
                Receiver = _loosingArmy.Island.Owner,
                CoreMessage = coreMessageLooser,
                DateSent = DateTime.Now
            };
            #endregion
        }



        private void CalculatePillagedResult()
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
                //   RessourceManager.RemoveRessource( _loosingArmy.Island.AllRessources, _loosingRessource );
                // _loosingRessource.ChangeRessources( ressources, false );
                _pillagedRessources = ressources;
                _loosingArmy.Island.AllRessources = _loosingRessource;
                _context.SaveChanges();
            }
            else if( _flag > 0 )
            {

                eachCapacityPillaged = _capacityOfPillaged / _flag;
                if( eachCapacityPillaged < _loosingRessource.Wood )
                {
                    _loosingRessource.AddWood( -eachCapacityPillaged );
                    _winningRessource.AddWood( eachCapacityPillaged );
                    _pillagedRessources.AddWood( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingRessource.Metal )
                {
                    _loosingRessource.AddMetal( -eachCapacityPillaged );
                    _winningRessource.AddMetal( eachCapacityPillaged );
                    _pillagedRessources.AddMetal( eachCapacityPillaged );

                }
                if( eachCapacityPillaged < _loosingRessource.Cristal )
                {
                    _loosingRessource.AddCristal( -eachCapacityPillaged );
                    _winningRessource.AddCristal( eachCapacityPillaged );
                    _pillagedRessources.AddCristal( eachCapacityPillaged );
                }
                if( eachCapacityPillaged < _loosingRessource.Magic )
                {
                    _loosingRessource.AddMagic( -eachCapacityPillaged );
                    _winningRessource.AddMagic( eachCapacityPillaged );
                    _pillagedRessources.AddMagic( eachCapacityPillaged );
                }


            }
        }
        private Ressource recursivite()
        {

            int eachCapacityPillaged = _capacityOfPillaged / 4;
            Ressource ressources = new Ressource { Wood = eachCapacityPillaged, Metal = eachCapacityPillaged, Cristal = eachCapacityPillaged, Magic = eachCapacityPillaged };

            int rest = 0;
            if( ressources.Wood > _loosingRessource.Wood )
            {
                rest = FindRest( "wood", ressources );
                _winningRessource.AddWood( _loosingRessource.Wood );
                _pillagedRessources.AddWood( _loosingRessource.Wood );
                _loosingRessource.AddWood( -(_loosingRessource.Wood) );
                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            if( ressources.Metal > _loosingRessource.Metal )
            {
                rest = FindRest( "metal", ressources );
                _winningRessource.AddMetal( _loosingRessource.Metal );
                _pillagedRessources.AddMetal( _loosingRessource.Metal );
                _loosingRessource.AddMetal( -(_loosingRessource.Metal) );

                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;

            }
            if( ressources.Cristal > _loosingRessource.Cristal )
            {
                rest = FindRest( "cristal", ressources );
                _winningRessource.AddCristal( _loosingRessource.Cristal );
                _pillagedRessources.AddCristal( _loosingRessource.Cristal );
                _loosingRessource.AddCristal( -(_loosingRessource.Cristal) );

                _capacityOfPillaged += rest;
                ressources = recursivite();
                _flag -= 1;
            }
            if( ressources.Magic > _loosingRessource.Magic )
            {
                rest = FindRest( "magic", ressources );
                _winningRessource.AddMagic( _loosingRessource.Magic );
                _pillagedRessources.AddMagic( _loosingRessource.Magic );
                _loosingRessource.AddMagic( -(_loosingRessource.Magic) );

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

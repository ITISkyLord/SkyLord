using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{

    public class Spell
    {
        private string _name;
        private string _description;

        [Key]
        public long SpellId { get; set; }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                _description = value;
            }
        }



        //        Découverte de buffs temporaires qu’on peut activer(?)
        //Buff d’attaque / défense
        //Vitesse de déplacement
        //Illusion d’optique ⇒ Beaucoup de ressources sont cachées en même temps.
        //Illusion/Contre illusion ⇒ scout ne rapporte rien, voire de mauvaises informations (le joueur choisis ce qu’il affiche )
        //Augmentation de capacité de charge
        //Téléportation d’une troupe en interne d’une île à l’autre

    }
}
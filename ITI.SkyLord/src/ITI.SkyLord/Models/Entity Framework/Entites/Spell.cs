using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ITI.SkyLord
{
    public class Spell
    {
        [Key]
        public long SpellId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }



        // Découverte de buffs temporaires qu’on peut activer(?)
        //Buff d’attaque / défense
        //Vitesse de déplacement
        //Illusion d’optique ⇒ Beaucoup de ressources sont cachées en même temps.
        //Illusion/Contre illusion ⇒ scout ne rapporte rien, voire de mauvaises informations (le joueur choisis ce qu’il affiche )
        //Augmentation de capacité de charge
        //Téléportation d’une troupe en interne d’une île à l’autre

    }
}
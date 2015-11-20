using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ITI.SkyLord
{
    public class Player
    {
        private long _playerId;
        private string _name;
        private Profil _profil;
        private List<Island> _islands;
        private Guild _guild;
        private List<Technology> _technologies;
        private World _world;

        //TODO Changer les valeurs par défauts. À voir avec le profil.
        /// <summary>
        /// Creates a Player.
        /// </summary>
        /// <param name="world">The world in wich the player is playing.</param>
        /// <param name="name">The name of the player.</param>
        /// <param name="mail">The email adress of the player.</param>
        /// <param name="password">The password of the player.</param>
        public Player(World world, string name, string mail = "toto@gmail.com", string password = "password")
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name is null or has white space.");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password is null or has white space.");
            if (world == null) throw new ArgumentNullException("world is null.");
            if (!IsValidEmailStatic.IsValidEmail(mail)) throw new ArgumentException("Invalid mail");
            if (name.Length > 50) throw new ArgumentOutOfRangeException(" Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment.");
            if (password.Length > 75) throw new ArgumentOutOfRangeException(" Maximum lenght of password is 75. Your name lenght is " + password.Length + " at the moment.");
            this._name = name;
            this._world = world;
            this._islands = new List<Island>();
            this._technologies = new List<Technology>();
            this._profil = new Profil(this, mail, password);
        }

        [Key]
        public long PlayerId { get; set; }
        /// <summary>
        /// Gets the Name of the Player.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Gets the Profile of the Player.
        /// </summary>
        public Profil Profil { get; set; }
        /// <summary>
        /// Gets or sets the list of Islands the Player controls.
        /// </summary>
        public List<Island> Islands { get; set; }
        /// <summary>
        /// Gets or sets the guild the player belongs to.
        /// </summary>
        public Guild Guild { get; set; }
        /// <summary>
        /// Gets or sets the technology the Player has developped.
        /// </summary>
        public List<Technology> Technologies { get; set; }

    }
}
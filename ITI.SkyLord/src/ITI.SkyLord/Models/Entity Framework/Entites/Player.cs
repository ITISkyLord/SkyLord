using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ITI.SkyLord
{
    public class Player
    {
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
            this.Name = name;
            this.World = world;
            this.Islands = new List<Island>();
            this.Technologies = new List<Technology>();
            this.Profil = new Profil( mail, password );
        }

        /// <summary>
        /// Empty Constructor for EF
        /// </summary>
        public Player()
        {
            this.Profil = new Profil();
            this.Islands = new List<Island>();
            this.Technologies = new List<Technology>();
        }
        [Key]
        public long PlayerId { get; set; }

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
        
        public Profil Profil
        {
            get
            {
                return _profil;
            }

            set
            {
                _profil = value;
            }
        }

        public List<Island> Islands
        {
            get
            {
                return _islands;
            }

            set
            {
                _islands = value;
            }
        }

        public Guild Guild
        {
            get
            {
                return _guild;
            }

            set
            {
                _guild = value;
            }
        }

        public List<Technology> Technologies
        {
            get
            {
                return _technologies;
            }

            set
            {
                _technologies = value;
            }
        }

        public World World
        {
            get
            {
                return _world;
            }

            set
            {
                _world = value;
            }
        }
    }
}
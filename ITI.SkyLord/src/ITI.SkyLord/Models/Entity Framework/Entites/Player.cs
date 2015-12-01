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
        public string _name;
        public Profil _profil;
        private List<Island> _islands;
        private Guild _guild;
        private List<Technology> _technologies;
        private World _world;

        #region Constructor

        //TODO Changer les valeurs par défauts. À voir avec le profil.
        /// <summary>
        /// Creates a Player.
        /// </summary>
        /// <param name="world">The world in wich the player is playing.</param>
        /// <param name="name">The name of the player.</param>
        /// <param name="mail">The email adress of the player.</param>
        /// <param name="password">The password of the player.</param>
        public Player(World world, string name, string mail, string password, string description)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name is null or has white space.");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password is null or has white space.");
            if (world == null) throw new ArgumentNullException("world is null.");
            if (!IsValidEmailStatic.IsValidEmail(mail)) throw new ArgumentException("Invalid mail");
            if (name.Length > 50) throw new ArgumentOutOfRangeException(" Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment.");
            if (password.Length > 1000) throw new ArgumentOutOfRangeException(" Maximum lenght of password is 1000. Your password lenght is " + password.Length + " at the moment.");
            this.Name = name;
            this.World = world;
            this.Islands = new List<Island>();
            this.Technologies = new List<Technology>();
            this.Profil = new Profil( mail, password, description );
        }

        public Player(World world, string name, string mail, string password)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name is null or has white space.");
            if (String.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password is null or has white space.");
            if (world == null) throw new ArgumentNullException("world is null.");
            if (!IsValidEmailStatic.IsValidEmail(mail)) throw new ArgumentException("Invalid mail");
            if (name.Length > 50) throw new ArgumentOutOfRangeException(" Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment.");
            if (password.Length > 1000) throw new ArgumentOutOfRangeException(" Maximum lenght of password is 1000. Your password lenght is " + password.Length + " at the moment.");
            this.Name = name;
            this.World = world;
            this.Islands = new List<Island>();
            this.Technologies = new List<Technology>();
            this.Profil = new Profil(mail, password);
        }

        public Player(World world, string name, string mail)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name is null or has white space.");
            if (world == null) throw new ArgumentNullException("world is null.");
            if (name.Length > 50) throw new ArgumentOutOfRangeException(" Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment.");
            if (!IsValidEmailStatic.IsValidEmail(mail)) throw new ArgumentException("Invalid mail");
            this.Name = name;
            this.World = world;
            this.Islands = new List<Island>();
            this.Technologies = new List<Technology>();
        }

        public Player(World world, string name)
        {
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name is null or has white space.");
            if (world == null) throw new ArgumentNullException("world is null.");
            if (name.Length > 50) throw new ArgumentOutOfRangeException(" Maximum lenght of name is 50. Your name lenght is " + name.Length + " at the moment.");
            this.Name = name;
            this.World = world;
            this.Islands = new List<Island>();
            this.Technologies = new List<Technology>();
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
        #endregion


        [Key]
        public long PlayerId { get; set; }

        [StringLength( 20, MinimumLength = 3 ) ]
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
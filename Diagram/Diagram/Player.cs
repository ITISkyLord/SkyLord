using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Player
    {
        private int id;
        private readonly string name;
        private Profil profil;
        private List<Island> islands;
        private List<Message> messages;
        private Guild guild;
        private List<Technology> techs;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Guild
    {
        private int _id;
        private string _name;
        private Dictionary<Player, GuildRole> _members;
        private string _description;
        private string _internalMessage;
        private string _publicMessage;
        private bool _recrutement; 

        
    }
}
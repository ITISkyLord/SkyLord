using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Diagram
{
    public class Message
    {
        private int id;
        private string messageObject;
        private string message;
        private Player receiver;
        private Player sender; // À voir si l'admin doit lancer un message à tout le monde.

    }
}
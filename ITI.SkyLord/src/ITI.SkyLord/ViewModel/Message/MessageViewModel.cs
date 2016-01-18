using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.ViewModel
{
    public class MessageViewModel : StandardViewModel
    {
       public List<Message> AllMessages { get; set; }
       public Message Message { get; set; }
    }
}

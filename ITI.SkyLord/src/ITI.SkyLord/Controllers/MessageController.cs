using ITI.SkyLord.Models.Entity_Framework.Contexts;
using ITI.SkyLord.ViewModel;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ITI.SkyLord.Controllers
{
    public class MessageController : Controller
    {
        [FromServices]
        public MessageContext messageContext { get; set; }
        [FromServices]
        public PlayerContext playerContext { get; set; }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public IActionResult GetMessage(int id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMessages(long islandId = 0)
        {
            MessageManager messageManager = new MessageManager(messageContext);
            Player currentPlayer = playerContext.GetPlayer(User.GetUserId());
            MessageViewModel mvm = new MessageViewModel();

            mvm.AllMessages = (List<Message>)messageManager.GetAllMessage(currentPlayer);

            messageContext.FillStandardVM(mvm, playerContext.GetPlayer(User.GetUserId()).PlayerId, islandId);
            return View(mvm);
        }

        [HttpPost]
        public IActionResult SendMessage(Player sender, Player receiver, string CoreMessage, string ObjectMessage, bool read)
        {
            return View();
        }

        public IActionResult DeleteMessage(long id)
        {
            return View();
        }
        [HttpGet]
        public IActionResult ReadMessage(long id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMessageUnread(long playerId)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMessageSent()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMessageRead()
        {
            return View();
        }
    }
}

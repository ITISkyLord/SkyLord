using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITI.SkyLord.Controllers
{
    public class MessageController : Controller
    {
        [FromServices]
        MessageContext messageContext { get; set; }


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
        public IActionResult GetAllMessages()
        {
            return View();
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

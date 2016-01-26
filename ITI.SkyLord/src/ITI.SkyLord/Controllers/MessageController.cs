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
    public class MessageController : GenericController
    {
        public MessageController( [FromServices]SetupContext setupcontext )
            :base(setupcontext)
        {
        }
        [HttpGet]
        public IActionResult Index( long islandId = 0 )
        {
            MessageViewModel mvm = new MessageViewModel();
            SetupContext.FillStandardVM( mvm, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return View( "Messages", mvm );
        }
        [HttpGet]
        public IActionResult GetAllMessages( long islandId = 0 )
        {
            MessageManager messageManager = new MessageManager( SetupContext );
            Player currentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            MessageViewModel mvm = new MessageViewModel();

            mvm.AllMessages = messageManager.GetAllMessage( currentPlayer ).ToList();

            SetupContext.FillStandardVM( mvm, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( mvm );
        }
        public IActionResult SeeThisMessage( long messageId, long islandId = 0 )
        {
            MessageManager messageManager = new MessageManager( SetupContext );
            Player currentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            MessageViewModel mvm = new MessageViewModel();

            mvm.Message = messageManager.GetThisMessage( messageId );
            mvm.Message.Read = true;
            mvm.FromId = mvm.Message.Sender.PlayerId;
            SetupContext.SaveChanges();
            SetupContext.FillStandardVM( mvm, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( mvm );
        }
        public IActionResult Answer( MessageViewModel model, long islandId = 0 )
        {
            MessageManager messageManager = new MessageManager( SetupContext );
            Player currentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            model.AllMessages = messageManager.GetAllMessage( currentPlayer ).ToList();
            Player from = SetupContext.Players.Single( p => p.PlayerId == model.FromId );
            messageManager.SendMessage( currentPlayer, from, model.Answer, model.ObjectOfSender, false );
            SetupContext.SaveChanges();
            ModelState.AddModelError( "message", "Le message a été bien envoyé." );
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );
            return View( "Messages", model );

        }
        public IActionResult SendMessage( MessageViewModel model, long islandId = 0 )
        {
            MessageManager messageManager = new MessageManager( SetupContext );
            Player currentPlayer = SetupContext.GetPlayer( User.GetUserId() );
            Player playerTo = SetupContext.Players.SingleOrDefault( p => p.Name == model.To );
            if( playerTo == null )
            {
                ModelState.AddModelError( "sender", "L'utilisateur sous ce nom : " + model.To + " n'existe pas" );
            }
            else
            {
                messageManager.SendMessage( currentPlayer, playerTo, model.Answer, model.ObjectOfSender, false );
                SetupContext.SaveChanges();
                ModelState.AddModelError( "message", "Le message a été bien envoyé." );
            }
            SetupContext.FillStandardVM( model, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return View( "Messages", model );

        }

        public IActionResult UnReadMessage( long messageId, long islandId )
        {
            MessageManager messageManager = new MessageManager( SetupContext );
            MessageViewModel mvm = new MessageViewModel();
            mvm.Message = messageManager.GetThisMessage( messageId );
            mvm.Message.Read = false;
            SetupContext.SaveChanges();
            SetupContext.FillStandardVM( mvm, SetupContext.GetPlayer( User.GetUserId() ).PlayerId, islandId );

            return View( "Messages", mvm );

        }

        public IActionResult DeleteMessage( long id )
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllMessageUnread( long playerId )
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

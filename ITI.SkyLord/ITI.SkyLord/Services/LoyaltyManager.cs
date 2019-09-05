using ITI.SkyLord.Models.Entity_Framework.Contexts;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.SkyLord.Models.Entity_Framework.Contexts.Interface;

namespace ITI.SkyLord
{
    public class LoyaltyManager
    {
        public SetupContext CurrentContext { get; set; }

        /// <summary>
        /// Creates a new LoyaltyManager instance
        /// </summary>
        public LoyaltyManager( SetupContext currentContext )
        {
            CurrentContext = currentContext;
        }

        /// <summary>
        /// Resolve Loyalty accumulated in the given island
        /// </summary>
        /// <param name="island">Current Island</param>
        public void ResolveLoyalty( Island island )
        {
            DateTime checkTime = GetLastCheckTime(island);
            if( !checkTime.Equals( new DateTime() ) )
            {
                // Calculate gap between last time and now
                int gap = (int)(DateTime.Now - checkTime).TotalSeconds;
                int loyaltyToAdd;

                int HardCodedLoyaltyProductionEachHour = 10;

                loyaltyToAdd = (int)(gap * GetProductionEachSecond( HardCodedLoyaltyProductionEachHour ));

                if( loyaltyToAdd >= 1 )
                {
                    AddLoyalty( island, loyaltyToAdd );
                    SetLastCheckTime( island );
                }
            }

            if( checkTime.Equals( new DateTime() ) )
            {
                SetLastCheckTime( island );
            }

            CurrentContext.SaveChanges();
        }

        #region LoyaltyProductionHelpers
        private void SetLastCheckTime( Island island )
        {
            island.LoyaltyCheckTime = DateTime.Now;
        }
        private DateTime GetLastCheckTime( Island island )
        {
            if( island.LoyaltyCheckTime != null )
            {
                return island.LoyaltyCheckTime;
            }
            return new DateTime();
        }
        private static double GetProductionEachSecond( int productionEachHour )
        {
            return (double)productionEachHour / 3600f;
        }
        #endregion
        /// <summary>
        /// Add some loyalty for the given island
        /// </summary>
        /// <param name="island"></param>
        /// <param name="amountToAdd">Expect a positive number</param>
        public void AddLoyalty( Island island, int amountToAdd )
        {
            if( amountToAdd < 0 ) throw new ArgumentException( "amountToAdd can't be under 0. Expect a positive number." );
            island.Loyalty += amountToAdd;
            if( island.Loyalty > 100 )
            {
                island.Loyalty = 100;
            }
        }
        /// <summary>
        /// Withdraw some loyalty for the given island
        /// </summary>
        /// <param name="island"></param>
        /// <param name="amountToWithdraw">Expect a positive number</param>
        public void WithdrawLoyalty( Island island, int amountToWithdraw )
        {
            if( amountToWithdraw < 0 ) throw new ArgumentException( "amountToWithdraw can't be under 0. Expect a positive number." );
            island.Loyalty -= amountToWithdraw;
            if( island.Loyalty < 0 )
            {
                island.Loyalty = 0;
            }
        }
    }
}
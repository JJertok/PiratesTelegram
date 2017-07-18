using Pirates.Engine.Models;
using Pirates.Telegram.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Telegram.Managers
{
    /// <summary>
    /// Build suitable view depends on players properties
    /// </summary>
    class ViewBuilder
    {
        public static IView Build(Player player)
        {
            return new WorldView(player);
        }
    }
}

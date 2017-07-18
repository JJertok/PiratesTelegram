using Pirates.Engine.Enums;
using Pirates.Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Engine.Infrastructures
{
    /// <summary>
    /// Can be use to create world of location (sea, city, shop and etc)
    /// </summary>
    public interface ILocation
    {
        // Player's on this location
        IEnumerable<Player> Players { get; }
        // Nearest location
        IDictionary<LocationDirections, ILocation> LinkedLocations { get; }
        void Remove(Player player);
        void Add(Player player);
    }
}

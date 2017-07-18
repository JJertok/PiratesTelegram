using Pirates.Engine.Enums;
using Pirates.Engine.Infrastructures;
using Pirates.Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Engine.Locations
{
    // Location for sea cell
    public class WorldCell : ILocation
    {
        #region Private fields and methods

        private List<Player> _players;
        private World _world;
        private int _x, _y;

        #endregion

        #region Public properties

        public IEnumerable<Player> Players
        {
            get
            {
                return _players;
            }
        }

        public int X { get { return _x; } }
        public int Y { get { return _y; } }
        public IDictionary<LocationDirections, ILocation> LinkedLocations
        {
            get
            {
                var result = new Dictionary<LocationDirections, ILocation>();
                if (Left != null) { result.Add(LocationDirections.Left, Left); }
                if (Right != null) { result.Add(LocationDirections.Right, Right); }
                if (Up != null) { result.Add(LocationDirections.Up, Up); }
                if (Down != null) { result.Add(LocationDirections.Down, Down); }

                return result;
            }
        }
        public ILocation Left { get; set; }
        public ILocation Right { get; set; }
        public ILocation Up { get; set; }
        public ILocation Down { get; set; }

        #endregion

        public WorldCell(int x, int y, World world)
        {
            _players = new List<Player>();
            _x = x;
            _y = y;
            _world = world;
        }

        public void Remove(Player player)
        {
            _players.Remove(player);
        }

        public void Add(Player player)
        {
            _players.Add(player);
        }

        // Get neareast area around the cell
        public ILocation[,] GetArea()
        {
            var neareastArea = _world.GetArea(X, Y, 5, 5);
            foreach (var location in neareastArea)
            {
                if (location != this)
                {
                    foreach (var player in location.Players)
                    {
                        if (DateTime.Now - player.LastUpdated > TimeSpan.FromSeconds(1))
                        {
                            player.LastUpdated = DateTime.Now;
                        }
                    }
                }
            }

            return neareastArea;
        }
    }
}

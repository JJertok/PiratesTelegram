using Pirates.Engine.Infrastructures;
using Pirates.Engine.Models;
using Pirates.Engine.Locations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Telegram.ViewModels
{
    class WorldViewModel
    {
        private Player _player;
        public WorldViewModel(Player player)
        {
            _player = player;
        }

        public bool CanMoveLeft()
        {
            return _player.Location.LinkedLocations.ContainsKey(Engine.Enums.LocationDirections.Left);
        }

        public bool CanMoveRight()
        {
            return _player.Location.LinkedLocations.ContainsKey(Engine.Enums.LocationDirections.Right);
        }

        public bool CanMoveUp()
        {
            return _player.Location.LinkedLocations.ContainsKey(Engine.Enums.LocationDirections.Up);
        }

        public bool CanMoveDown()
        {
            return _player.Location.LinkedLocations.ContainsKey(Engine.Enums.LocationDirections.Down);
        }

        public void Move(Engine.Enums.LocationDirections direction)
        {
            _player.MoveTo(direction);
        }

        public ILocation[,] GetNeareastArea()
        {
            var wordCell = _player.Location as Pirates.Engine.Locations.WorldCell;
            if (wordCell == null)
            {
                return null;
            }

            return wordCell.GetArea();
        }
    }
}

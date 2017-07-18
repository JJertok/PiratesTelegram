using System;
using Pirates.Engine.Locations;
using Pirates.Engine.Models;
using System.Collections.Generic;
using System.Linq;
using Pirates.Engine.Infrastructures;

namespace Pirates.Server
{
    /// <summary>
    /// Basic server can be use for manage all players activity and NPC
    /// </summary>
    public class BasicServer
    {
        private World _world;
        private List<Player> _players = new List<Player>();
        public BasicServer(int height, int width)
        {
            _world = new World(height, width);
        }

        public Player GetOrCreateUser(string name)
        {
            var player = _players.Where(x => x.Name == name).FirstOrDefault();
            if (player == null)
            {
                player = new Player(name, _world.GetRandomPosition());
                _players.Add(player);
            }

            return player;
        }
    }
}

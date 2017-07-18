using Pirates.Engine.Enums;
using Pirates.Engine.Infrastructures;
using Pirates.Engine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pirates.Engine.Locations
{

    /// <summary>
    /// This class represent a global world, that contain all players and locations
    /// </summary>
    public class World
    {
        readonly int _width, _height;
        readonly WorldCell[,] _map;
        public World(int width, int height)
        {
            _width = width;
            _height = height;
            _map = new WorldCell[width, height];

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _map[x, y] = new WorldCell(x, y, this);
                }
            }

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (x != 0)
                    {
                        _map[x, y].Left = _map[x - 1, y];
                    }

                    if (y != 0)
                    {
                        _map[x, y].Up = _map[x, y - 1];
                    }

                    if (y != _height - 1)
                    {
                        _map[x, y].Down = _map[x, y + 1];
                    }

                    if (x != _width - 1)
                    {
                        _map[x, y].Right = _map[x + 1, y];
                    }
                }
            }
        }

        /// <summary>
        /// Get rectangle area with center and width and height range
        /// </summary>
        /// <param name="x">Center X</param>
        /// <param name="y">Center Y</param>
        /// <param name="widthRange">range for width</param>
        /// <param name="heightRange">range for height</param>
        /// <returns></returns>
        public ILocation[,] GetArea(int x, int y, int widthRange, int heightRange)
        {
            var x0 = Math.Max(x - widthRange, 0);
            var x1 = Math.Min(x + widthRange, _width);

            var y0 = Math.Max(y - heightRange, 0);
            var y1 = Math.Min(y + heightRange, _height);


            var result = new ILocation[x1 - x0, y1 - y0];

            for (int localX = x0; localX < x1; localX++)
            {
                for (int localY = y0; localY < y1; localY++)
                {
                    result[localX - x0, localY - y0] = _map[localX, localY];
                }
            }

            return result;
        }
        /// <summary>
        /// Get random position in global world
        /// </summary>
        /// <returns>Random location from world</returns>
        public ILocation GetRandomPosition()
        {
            var random = new Random();

            var x = random.Next(0, _width);
            var y = random.Next(0, _height);

            return _map[x, y];
        }
    }
}


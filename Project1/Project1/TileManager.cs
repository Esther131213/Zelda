using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Zelda
{
    internal class TileManager
    {
        int[,] tileMap;

        Texture2D tileset;
        int tileSize = 32;

        void LoadTileMap(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines[0].Split(",").Length;

            tileMap = new int[rows, cols];

            for (int y = 0; y < rows; y++)
            {
                var tiles = lines[y].Split(",");
                for (int x = 0; x < cols; x++)
                {
                    tileMap[x,y] = int.Parse(tiles[x]);
                }
            }
        }

        void Draw(SpriteBatch sp)
        {

        }
    }
}

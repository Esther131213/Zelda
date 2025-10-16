using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Zelda
{
    internal class TileManager
    {
        int[,] tileMap;
        int[,] tiles;

        Texture2D tileset;
        int tileSize = 16;

        public void LoadTileMap(string filePath, Texture2D tileset)
        {
            this.tileset = tileset;
            var lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines.Max(x => x.Length);

            tiles = new int[cols, rows];


            for (int y = 0; y < rows; y++)
            {
                var tiles = lines[y].Split(",");
                for (int x = 0; x < cols; x++)
                {
                    this.tiles[x, y] = int.Parse(new string(lines[y][x], 1));
                    Debug.WriteLine(this.tiles[x, y]);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    int tileId = tiles[x, y];
                    if (tileId >= 0)
                    {
                        var sourceRectangle = new Rectangle(tileId * tileSize,0,tileSize, tileSize);
                        var destinationRectangle = new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize);
                        spriteBatch.Draw(tileset, destinationRectangle, sourceRectangle, Color.White);
                    }
                }
            }
        }
    }
}

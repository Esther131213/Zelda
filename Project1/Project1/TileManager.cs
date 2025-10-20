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
using System.Runtime.InteropServices.WindowsRuntime;
using SharpDX.Direct2D1.Effects;

namespace Zelda
{
    internal class TileManager
    {
        public static int[,] tiles;
        Texture2D tileset;
        readonly int tileSize = 16;
        public Tile[] tileTypes;

        public void LoadTileMap(string filePath, Texture2D tileset)
        {
            this.tileset = tileset;
            var lines = File.ReadAllLines(filePath);
            int rows = lines.Length;
            int cols = lines.Max(x => x.Length);

            tileTypes = new Tile[]
            {
                new Tile(true, new Rectangle(0 * tileSize,0,tileSize, tileSize)),
                new Tile(false, new Rectangle(1 * tileSize,0,tileSize, tileSize)),
                new Tile(false, new Rectangle(2 * tileSize,0,tileSize, tileSize)),
                new Tile(false, new Rectangle(3 * tileSize,0,tileSize, tileSize)),
                new Tile(true, new Rectangle(4 * tileSize,0,tileSize, tileSize)),
            };

            tiles = new int[cols, rows];

            for (int y = 0; y < rows; y++)
            {
                var tileslines = lines[y].Split(",");
                for (int x = 0; x < cols; x++)
                {
                    tiles[x, y] = int.Parse(new string(lines[y][x], 1));
                }
            }
        }

        public Vector2 GetTilePosition(int x, int y)
        {
            return new Vector2(x * tileSize, y * tileSize);
        }

        /*
        public static bool GetTileAtPosition(Vector2 vec)
        {
            return tileTypes[(int)vec.X / 50, (int)vec.Y / 50].isWalkable;
        }
        */

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    int tileId = tiles[x, y];
                    if (tileId >= 0)
                    {
                        var sourceRectangle = tileTypes [tileId].sourceRectangle;
                        var destinationRectangle = new Rectangle((int)GetTilePosition(x,y).X, (int)GetTilePosition(x,y).Y, tileSize, tileSize);
                        spriteBatch.Draw(tileset, destinationRectangle, sourceRectangle, Color.White);
                    }
                }
            }
        }
    }
}

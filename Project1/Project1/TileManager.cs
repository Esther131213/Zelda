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
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel;

namespace Zelda
{
    public class TileManager
    {
        public static Tile[,] tiles;
        Texture2D tileset;
        public readonly int tileSize = 16;
        public Tile[] tileTypes;
        List<string>Map = new List<string>();
        int tileId;
        Rectangle sourceRectangle;

        public static List<string> GetMapInfo(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            List<string> MapInfo = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                MapInfo.Add(line);
            }
            return MapInfo;
        }

        public void LoadTileMap(string filePath, Texture2D tileset)
        {
            this.tileset = tileset;
            Map = GetMapInfo(filePath);
            int rows = Map.Count;
            int cols = Map[0].Length;

            tiles = new Tile[cols, rows];

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    char tileType = Map[y][x];
                    Vector2 tilePos = new Vector2(x * tileSize, y * tileSize);
                    tileId = tileType - '0';
                    sourceRectangle = new Rectangle(tileId * tileSize, 0, tileSize, tileSize);

                    if (tileId == 0)
                    {
                        tiles[x, y] = new Tile(true, sourceRectangle, tileset, tilePos);
                    }
                    else if (tileId == 1)
                    {
                        tiles[x, y] = new Tile(false, sourceRectangle, tileset, tilePos);
                    }
                    else if (tileId == 2)
                    {
                        tiles[x, y] = new Tile(false, sourceRectangle, tileset, tilePos);
                    }
                    else if (tileId == 3)
                    {
                        tiles[x, y] = new Tile(false, sourceRectangle, tileset, tilePos);
                    }
                    else if (tileId == 4)
                    {
                        tiles[x, y] = new Tile(true, sourceRectangle, tileset, tilePos);
                    }
                }
            }
        }

        public Vector2 GetTilePosition(int x, int y)
        {
            return new Vector2(x * tileSize, y * tileSize);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
            {
                for (int x = 0; x < tiles.GetLength(0); x++)
                {
                    tiles[x, y].Draw(spriteBatch);
                }
            }
        }
    }
}

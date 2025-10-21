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

namespace Zelda
{
    public class Tile
    {
        public bool isWalkable = true;
        public Rectangle sourceRectangle;
        Texture2D tex;
        Vector2 pos;
        int tileSize = 16;

        public Tile(bool isWalkable, Rectangle sourceRectangle, Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
            this.isWalkable = isWalkable;
            this.sourceRectangle = sourceRectangle;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), Color.White);
        }
    }
}

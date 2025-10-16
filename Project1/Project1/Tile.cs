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

        public Tile(bool isWalkable, Rectangle sourceRectangle)
        {
            this.isWalkable = isWalkable;
            this.sourceRectangle = sourceRectangle;
        }
    }
}

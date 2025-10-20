using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;


namespace Zelda
{
    internal class Enemies
    {
        new Vector2 pos;
        Texture2D tex;

        public Enemies(Vector2 pos, Texture2D tex)
        {
            this.pos = pos;
            this.tex = tex;
        }
    }
}

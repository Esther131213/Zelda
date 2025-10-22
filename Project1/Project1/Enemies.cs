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
        new Vector2 startPos;
        new Vector2 endPos;
        new Vector2 currentPos;
        Texture2D tex;

        public Enemies(Vector2 startpos, Vector2 endPos, Texture2D tex)
        {
            this.startPos = startpos;
            this.endPos = endPos;
            this.tex = tex;
        }
    }
}

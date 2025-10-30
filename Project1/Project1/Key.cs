using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda
{
    public class Key
    {
        Texture2D tex;
        public Rectangle hitBox;
        bool exists = true;

        public Key(Texture2D tex, Vector2 pos)
        {
            hitBox.X = (int)pos.X;
            hitBox.Y = (int)pos.Y;

            hitBox.Width = tex.Width -5;
            hitBox.Height = tex.Height -5;

            this.tex = tex;
        }

        public void KeyIsTouched()
        {
            exists = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (exists)
            {
                spriteBatch.Draw(tex, hitBox, Color.White);
            }
        }
    }
}

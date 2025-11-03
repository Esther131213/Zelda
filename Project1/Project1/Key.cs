using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1;
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
        public bool pickedUp = false;
        public bool exists = true;

        public Key(Texture2D tex, Vector2 pos)
        {
            hitBox.X = (int)pos.X;
            hitBox.Y = (int)pos.Y;

            hitBox.Width = tex.Width - 5;
            hitBox.Height = tex.Height - 4;

            this.tex = tex;
        }

        public void KeyIsTouched()
        {
            pickedUp = true;
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

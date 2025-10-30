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
    public class Door
    {
        Texture2D tex;
        public Rectangle hitBox;
        bool exists = true;

        public Door(Texture2D tex, Vector2 pos)
        {
            hitBox.X = (int)pos.X;
            hitBox.Y = (int)pos.Y - 2;

            hitBox.Width = tex.Width;
            hitBox.Height = tex.Height + 4;

            this.tex = tex;
        }

        public void DoorIsTouched()
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

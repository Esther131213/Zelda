using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Project1;
using System.Threading;
using System.Security.Cryptography.Pkcs;


namespace Zelda
{
    public class Enemies
    {
        public Vector2 pos;
        public Texture2D tex;
        int direction = 1; //1 = Right, -1 = Left
        public Rectangle hitBox;
        float moveDistance = 16;
        Vector2 nextPos;
        bool canWalk = true;
        int timer;
        public int maxTime = 16;


        public Enemies(Vector2 pos, Texture2D tex)
        {
            this.pos = pos;
            this.tex = tex;
            hitBox.Width = tex.Width;
            hitBox.Height = tex.Height;
        }
        public void MovementCheck()
        {
            if (direction == 1)
            {
                nextPos = new Vector2(pos.X + moveDistance, pos.Y);
            }
            else if(direction == -1)
            {
                nextPos = new Vector2(pos.X - moveDistance, pos.Y);
            }

            if (Game1.GetTileAtPosition(nextPos))
            {
                canWalk = true;
                timer++;
            }
            else if (!Game1.GetTileAtPosition(nextPos))
            {
                canWalk = false;
                timer++;
            }
        }

        public void Update(GameTime gameTime)
        {
            MovementCheck();

            if (canWalk && timer >= maxTime)
            {
                pos = nextPos;
                timer = 0;
            }
            else if(!canWalk && timer >= maxTime)
            {
                direction *= -1;
                timer = 0;
                Debug.WriteLine(pos);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, hitBox, Color.White);
        }
    }
}

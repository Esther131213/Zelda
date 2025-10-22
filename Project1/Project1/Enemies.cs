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


namespace Zelda
{
    internal class Enemies
    {
        new Vector2 pos;
        Texture2D tex;
        int direction; //1 = Right, -1 = Left
        Rectangle hitBox;
        float moveDistance = 16;
        Vector2 nextPos;
        bool canWalk = true;

        public Enemies(Vector2 pos, Texture2D tex, Rectangle hitbox)
        {
            this.pos = pos;
            this.tex = tex;
            this.hitBox = hitbox;
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
            }
            else if (!Game1.GetTileAtPosition(nextPos))
            {
                canWalk = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            MovementCheck();

            if (canWalk)
            {
                pos = nextPos;
            }
            else
            {
                direction *= -1;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, hitBox, Color.White);
        }
    }
}

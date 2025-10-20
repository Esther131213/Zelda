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

namespace Zelda
{
    public class Player
    {
        TileManager tileManager;

        float speed;
        new Vector2 pos;
        Texture2D tex;
        public bool hasKey = false;
        float moveDistance = 16; //Same as tile width and height
        KeyboardState previousKeyState;
        bool canWalk = true;
        Rectangle hitBox;

        Vector2 nextPos;

        public Player(Texture2D tex, Vector2 pos)
        {
            this.tex = tex;
            this.pos = pos;
        }

        public void MovementCheck()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                nextPos = new Vector2(pos.X, pos.Y - moveDistance);

            }
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !previousKeyState.IsKeyDown(Keys.W)) //Moving Up
            {
                MovementCheck();
                if (canWalk)
                {
                    pos.Y -= moveDistance;
                    Debug.WriteLine("Going Up!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && !previousKeyState.IsKeyDown(Keys.A)) //Moving Left
            {
                MovementCheck();
                if (canWalk)
                {
                    pos.X -= moveDistance;
                    Debug.WriteLine("Going Left!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !previousKeyState.IsKeyDown(Keys.S)) //Moving Down
            {
                MovementCheck();
                if (canWalk)
                {
                    pos.Y += moveDistance;
                    Debug.WriteLine("Going Down!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !previousKeyState.IsKeyDown(Keys.D)) //Moving Right
            {
                MovementCheck();
                if (canWalk)
                {
                    pos.X += moveDistance;
                    Debug.WriteLine("Going Right!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else
            {
                previousKeyState = Keyboard.GetState();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}

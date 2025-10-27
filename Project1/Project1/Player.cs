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
using Project1;

namespace Zelda
{
    public class Player
    {
        TileManager tileManager;

        public Vector2 pos;
        public Texture2D tex;
        public bool hasKey = false;
        float moveDistance = 16; //Same as tile width and height
        KeyboardState previousKeyState;
        bool canWalk = true;
        public Rectangle hitBox;
        Vector2 nextPos;
        public int playerHealth;

        public Player(Texture2D tex, Vector2 pos, int playerHealth)
        {
            this.tex = tex;
            this.pos = pos;
            this.playerHealth = playerHealth;
            hitBox.Height = 4;
            hitBox.Width = 4;
        }

        public void PlayerTakeDamage()
        {
            playerHealth--;
            Debug.WriteLine("player health: " + playerHealth);
        }

        public void MovementCheck()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                nextPos = new Vector2(pos.X, pos.Y - moveDistance);
                //Debug.WriteLine(nextPos);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                nextPos = new Vector2(pos.X - moveDistance, pos.Y);
                //Debug.WriteLine(nextPos);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                nextPos = new Vector2(pos.X, pos.Y + moveDistance);
                //Debug.WriteLine(nextPos);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                nextPos = new Vector2(pos.X + moveDistance, pos.Y);
                //Debug.WriteLine(nextPos);
            }

            if (Game1.GetTileAtPosition(nextPos))
            {
                //Debug.WriteLine("Its clear to walk!");
                canWalk = true;
            }
            else if (!Game1.GetTileAtPosition(nextPos))
            {
                //Debug.WriteLine("Its a wall! (probably?)");
                canWalk = false;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !previousKeyState.IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D)) //Moving Up
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.Y -= moveDistance;
                    //Debug.WriteLine("Going Up!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && !previousKeyState.IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S)) //Moving Left
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.X -= moveDistance;
                    //Debug.WriteLine("Going Left!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !previousKeyState.IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D)) //Moving Down
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.Y += moveDistance;
                    //Debug.WriteLine("Going Down!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !previousKeyState.IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S)) //Moving Right
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.X += moveDistance;
                    //Debug.WriteLine("Going Right!");
                }
                previousKeyState = Keyboard.GetState();
            }
            else
            {
                previousKeyState = Keyboard.GetState();
            }
            hitBox.X = (int)pos.X;
            hitBox.Y = (int)pos.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, Color.White);
        }
    }
}

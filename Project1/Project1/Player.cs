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
using System.Threading;

namespace Zelda
{
    public class Player
    {
        TileManager tileManager;

        public Vector2 pos;
        public Texture2D tex;
        public bool hasKey = false;
        int moveDistance = 16; //Same as tile width and height
        KeyboardState previousKeyState;
        public bool canWalk = true;
        public Rectangle hitBox;
        public Vector2 nextPos;
        public int playerHealth;
        public bool touchingDoor = false;
        int timer = 100;
        public Rectangle attackBox;
        public bool isAttacking;

        Color linkColor = Color.White;

        public Player(Texture2D tex, Vector2 pos, int playerHealth)
        {
            this.tex = tex;
            this.pos = pos;
            this.playerHealth = playerHealth;
            hitBox.Height = tex.Height;
            hitBox.Width = tex.Width;
            attackBox.Height = tex.Height;
            attackBox.Width = tex.Width;
        }

        public void PickUpKey()
        {
            hasKey = true;
        }

        public void PlayerTakeDamage()
        {
            playerHealth--;
            Debug.WriteLine("player health: " + playerHealth);
        }

        public void Attacking()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                attackBox.Y = (int)pos.Y - moveDistance;
                attackBox.X = (int)pos.X;
                Debug.WriteLine(attackBox.X + " " + attackBox.Y);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                attackBox.X = (int)pos.X - moveDistance;
                attackBox.Y = (int)pos.Y;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                attackBox.Y = (int)pos.Y + moveDistance;
                attackBox.X = (int)pos.X;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                attackBox.X = (int)pos.X + moveDistance;
                attackBox.Y = (int)pos.Y;
            }
            else
            {
                attackBox.Y = 5000;
                attackBox.X = 5000;
            }
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
            if (Keyboard.GetState().IsKeyDown(Keys.W) && !previousKeyState.IsKeyDown(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D) && (touchingDoor == false || hasKey == true)) //Moving Up
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.Y -= moveDistance;
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && !previousKeyState.IsKeyDown(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S)) //Moving Left
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.X -= moveDistance;
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.S) && !previousKeyState.IsKeyDown(Keys.S) && Keyboard.GetState().IsKeyUp(Keys.A) && Keyboard.GetState().IsKeyUp(Keys.D)) //Moving Down
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.Y += moveDistance;
                }
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D) && !previousKeyState.IsKeyDown(Keys.D) && Keyboard.GetState().IsKeyUp(Keys.W) && Keyboard.GetState().IsKeyUp(Keys.S)) //Moving Right
            {
                MovementCheck();
                if (canWalk == true)
                {
                    pos.X += moveDistance;
                }
                previousKeyState = Keyboard.GetState();
            }//Movement check end, Attack check begins
            else if (Keyboard.GetState().IsKeyDown(Keys.Up) && !previousKeyState.IsKeyDown(Keys.Up))
            {
                isAttacking = true;
                Attacking();
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left) && !previousKeyState.IsKeyDown(Keys.Left))
            {
                isAttacking = true;
                Attacking();
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down) && !previousKeyState.IsKeyDown(Keys.Down))
            {
                isAttacking = true;
                Attacking();
                previousKeyState = Keyboard.GetState();
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right) && !previousKeyState.IsKeyDown(Keys.Right))
            {
                isAttacking = true;
                Attacking();
                previousKeyState = Keyboard.GetState();
            }
            else
            {
                isAttacking = false;
                previousKeyState = Keyboard.GetState();
            }
            hitBox.X = (int)pos.X;
            hitBox.Y = (int)pos.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, pos, linkColor);
        }
    }
}

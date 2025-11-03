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
using Zelda;
using SharpDX.DXGI;
using SharpDX.DirectWrite;

namespace Project1
{
    //To download the editor: dotnet tool install --global dotnet-mgcb-editor
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Enemies enemy1;
        Enemies enemy2;

        Player player;

        Key key;
        Texture2D keyTex;

        Door door;

        Texture2D zelda;
        Texture2D Lockness;

        TileManager tileManager;

        int keyUpOffset = -10;
        int keySideOffset = 3;

        int tileSize = 16;
        int tileAmountHeight = 28;
        int tileAmountWidth = 45;

        bool isTouching = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1286;
            graphics.PreferredBackBufferHeight = 810;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        public enum GameStates
        {
            StartScreen,
            GamePlay,
            GameEnd,
            Looser
        }
        GameStates gamestates = GameStates.StartScreen;

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureHandler.LoadTextures(Content);

            tileManager = new TileManager();
            tileManager.LoadTileMap("tilemap.txt", TextureHandler.TileMap);
            player = new Player(TextureHandler.Link, new Vector2((tileAmountWidth * tileSize) - tileSize, (tileAmountHeight * tileSize) - tileSize), 3);
            enemy1 = new Enemies(new Vector2(tileSize * 2, tileSize * 6), TextureHandler.Link);
            enemy2 = new Enemies(new Vector2(tileSize * 33, tileSize * 12), TextureHandler.Link);
            enemy2.maxTime = 10;

            key = new Key(TextureHandler.Key, new Vector2(tileSize * 41, tileSize * 8));

            door = new Door(TextureHandler.Link, new Vector2(tileSize * 28, tileSize * 3));

            zelda = TextureHandler.Link;

            Lockness = TextureHandler.Linkness;
        }

        public static bool GetTileAtPosition(Vector2 vec)
        {
            int momentTileSize = 16;
            return TileManager.tiles[(int)vec.X / momentTileSize, (int)vec.Y / momentTileSize].isWalkable; 
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Update logic here
            if (gamestates == GameStates.StartScreen)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                {
                    gamestates = GameStates.GamePlay;
                }
            }
            else if (gamestates == GameStates.GamePlay)
            {
                player.Update(gameTime);
                enemy1.Update(gameTime);
                enemy2.Update(gameTime);

                if (player.playerHealth <= 0)
                {
                    gamestates = GameStates.Looser;
                }

                if ((player.hitBox.Intersects(enemy1.hitBox) && isTouching == false) || (player.hitBox.Intersects(enemy2.hitBox) && isTouching == false))
                {
                    Debug.WriteLine("Theyre touching!");
                    player.PlayerTakeDamage();
                    isTouching = true;
                }
                else if((!player.hitBox.Intersects(enemy1.hitBox) && isTouching == true) && (!player.hitBox.Intersects(enemy2.hitBox) && isTouching == true))
                {
                    isTouching = false;
                }

                if (player.hitBox.Intersects(key.hitBox))
                {
                    player.PickUpKey();
                    key.KeyIsTouched();
                }

                if (player.hitBox.Intersects(door.hitBox) && player.hasKey)
                { 
                    player.touchingDoor = true;
                    key.exists = false;
                    door.exists = false;
                    gamestates = GameStates.GameEnd;
                }
                else if (player.hitBox.Intersects(door.hitBox))
                {
                    player.touchingDoor = true;
                }
                else if (!player.hitBox.Intersects(door.hitBox))
                {
                    player.touchingDoor = false;
                }

                if (key.pickedUp == true)
                {
                    key.hitBox.X = (int)player.pos.X + keySideOffset;
                    key.hitBox.Y = (int)player.pos.Y + keyUpOffset;
                }
                else
                {

                }

                if (player.attackBox.Intersects(enemy1.hitBox))
                {
                    enemy1.TakeDamage();
                    Debug.WriteLine("it hit!");
                    player.attackBox.X = 5000;
                    enemy1.hitBox.X = 7000;
                }
                else if (player.attackBox.Intersects(enemy2.hitBox))
                {
                    enemy2.TakeDamage();
                    Debug.WriteLine("it hit!");
                    player.attackBox.X = 5000;
                    enemy2.hitBox.X = 7000;
                }
                else
                {

                }

                if (Keyboard.GetState().IsKeyDown(Keys.Up) || Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Down) || Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    player.tex = TextureHandler.Attacking;
                }
                else
                {
                    player.tex = TextureHandler.Link;
                }
            }
            else if (gamestates == GameStates.GameEnd)
            {

            }

            //Base Update
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkRed);
            base.Draw(gameTime);

            float zoom = 1.75f;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, transformMatrix: Matrix.CreateScale(zoom));

            if (gamestates == GameStates.StartScreen)
            {
                spriteBatch.Draw(TextureHandler.StartTexture, new Vector2(0, 0), Color.White);
                spriteBatch.Draw(Lockness, new Vector2(tileSize * 21, tileSize * 11), Color.White);
                spriteBatch.Draw(TextureHandler.Link, new Vector2(tileSize * 37, tileSize * 21), Color.White);
            }
            else if (gamestates == GameStates.GamePlay)
            {
                tileManager.Draw(spriteBatch);
                enemy1.Draw(spriteBatch);
                enemy2.Draw(spriteBatch);
                door.Draw(spriteBatch);
                spriteBatch.Draw(zelda, new Vector2(tileSize * 28, tileSize * 1), Color.LightGreen);
                player.Draw(spriteBatch);
                key.Draw(spriteBatch);
                spriteBatch.Draw(Lockness, new Vector2(tileSize * 36, tileSize * 15), Color.White);
            }
            else if (gamestates == GameStates.GameEnd)
            {
                tileManager.Draw(spriteBatch);
                spriteBatch.Draw(TextureHandler.Link, new Vector2(tileSize * 21, tileSize * 4), Color.White);
                spriteBatch.Draw(TextureHandler.Link, new Vector2(tileSize * 22, tileSize * 4), Color.LightGreen);
                spriteBatch.Draw(TextureHandler.Key, new Vector2(tileSize * 40, tileSize * 16), Color.White);
                spriteBatch.Draw(Lockness, new Vector2(tileSize * 36, tileSize * 15), Color.White);
            }
            else if (gamestates == GameStates.Looser)
            {

            }

            spriteBatch.End();
        }
    }
}

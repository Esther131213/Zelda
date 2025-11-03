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
            GameEnd
        }
        GameStates gamestates = GameStates.GamePlay;

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

            }
            else if (gamestates == GameStates.GamePlay)
            {
                player.Update(gameTime);
                enemy1.Update(gameTime);
                enemy2.Update(gameTime);

                if (player.playerHealth <= 0)
                {
                    gamestates = GameStates.GameEnd;
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

            }
            spriteBatch.End();
        }
    }
}

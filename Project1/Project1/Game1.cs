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

namespace Project1
{
    //To download the editor: dotnet tool install --global dotnet-mgcb-editor
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Enemies enemy1;
        Enemies enemy2;
        Texture2D enemyTex;

        Player player;
        Texture2D playerTex;

        TileManager tileManager;
        Texture2D tileset;

        int tileSize = 16;
        int tileAmountHeight = 28;
        int tileAmountWidth = 45;

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

        public static bool GetTileAtPosition(Vector2 vec)
        {
            return TileManager.tiles[(int)vec.X/16, (int)vec.Y/16].isWalkable; //16 in this insatnce refers to the width & height of the tile. 
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tileManager = new TileManager();
            tileset = Content.Load<Texture2D>("tileMap");
            tileManager.LoadTileMap("tilemap.txt", tileset);

            playerTex = Content.Load<Texture2D>("testLink(1)");
            player = new Player(playerTex, new Vector2((tileAmountWidth * tileSize) - tileSize, (tileAmountHeight * tileSize) - tileSize));

            enemyTex = playerTex;
            enemy1 = new Enemies(new Vector2(tileSize * 2, tileSize * 6), enemyTex);
            enemy2 = new Enemies(new Vector2(tileSize * 33, tileSize * 12), enemyTex);
            enemy2.maxTime = 10;
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
            }
            else if (gamestates == GameStates.GameEnd)
            {

            }

            //Base Update
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.PaleGreen);
            base.Draw(gameTime);

            float zoom = 1.75f;
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, transformMatrix: Matrix.CreateScale(zoom));

            if (gamestates == GameStates.StartScreen)
            {

            }
            else if (gamestates == GameStates.GamePlay)
            {
                tileManager.Draw(spriteBatch);
                player.Draw(spriteBatch);
                enemy1.Draw(spriteBatch);
                enemy2.Draw(spriteBatch);
            }
            else if (gamestates == GameStates.GameEnd)
            {

            }
            spriteBatch.End();
        }
    }
}

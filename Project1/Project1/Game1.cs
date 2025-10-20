using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using Zelda;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

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

        public enum gameStates
        {
            StartScreen,
            GamePlay,
            GameEnd
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
            player = new Player(playerTex, new Vector2((tileAmountWidth * tileSize) - tileSize , (tileAmountHeight * tileSize) - tileSize));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null, null, Matrix.CreateScale(1.75f));
            tileManager.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

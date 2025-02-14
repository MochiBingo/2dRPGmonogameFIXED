using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dRPGmonogameFIXED
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private player player;
        private int sizeX = 15;
        private int sizeY = 10;
        private bool canWalk;
        private bool isExit;
        public Random rand = new Random();
        public GraphicsDeviceManager Graphics { get { return _graphics; } }
        public SpriteBatch Spritebatch { get { return _spriteBatch; } }

        private Texture2D ground;
        private Texture2D tree;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected void GenerateMap()
        {

        }
        protected override void Initialize()
        {
            player = new player(this, new Vector2(100, 100));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ground = this.Content.Load<Texture2D>("ground1");
            tree = this.Content.Load<Texture2D>("tree1");
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds * 0.01f;

            player.Update(deltaTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            player.Draw();
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
    public class player
    {
        private Texture2D playerSprite;
        protected Game1 game1;
        protected Vector2 position;
        public player(Game1 game, Vector2 initialposition)
        {
            position = initialposition;
            game1 = game;
            playerSprite = game.Content.Load<Texture2D>("player");
        }
        public void Update(float deltaTime)
        {
            KeyboardState kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.W))
            {
                UpdatePosition(0, 1);
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                UpdatePosition(0, -1);
            }
            if (kstate.IsKeyDown(Keys.D))
            {
                UpdatePosition(1, 0);
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                UpdatePosition(-1, 0);
            }
        }
        public void Draw()
        {
            game1.Spritebatch.Draw(playerSprite, new Rectangle((int)position.X, (int)position.Y, playerSprite.Width, playerSprite.Height), Color.White);
        }
        public void UpdatePosition(int x, int y)
        {

        }
    }
}
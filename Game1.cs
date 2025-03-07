﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace _2dRPGmonogameFIXED
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private static SpriteBatch _spriteBatch;
        private player player;
        private map map;
        public Random rand = new Random();
        public GraphicsDeviceManager Graphics { get { return _graphics; } }
        public SpriteBatch Spritebatch { get { return _spriteBatch; } }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        public static Texture2D square;
        private bob mybob;
        private bob mybob2;
        protected override void Initialize()
        {
            square = new Texture2D(GraphicsDevice, 1, 1);
            square.SetData(new[] { Color.White });
            player = new player(this, new Vector2(100, 100));
            map = new map(this);
            mybob = new bob(new Vector2(50, 50), Color.PeachPuff);
            mybob2 = new bob(new Vector2(150, 150), Color.Orange);
            mybob.myWeapon.weaponColor = Color.Green;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            float deltaTime = gameTime.ElapsedGameTime.Milliseconds * 0.01f;

            player.Update(deltaTime);
        }
        public static void EasyDraw(float positionX, float positionY, Color color)
        {
            _spriteBatch.Draw(square, new Rectangle((int)positionX, (int)positionY, 50, 50), color);
        }
        public static void EasyDraw(float positionX, float positionY, float scaleX, float scaleY, Color color)
        {
            _spriteBatch.Draw(square, new Rectangle((int)positionX, (int)positionY, (int)scaleX, (int)scaleY), color);
        }
        public static void EasyDraw(IDrawable drawable)
        {
            _spriteBatch.Draw(square, new Rectangle((int)drawable.Position.X, (int)drawable.Position.Y, (int)drawable.Scale.X, (int)drawable.Scale.Y), drawable.DrawColor);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            player.Draw();
            mybob.Draw(_spriteBatch);
            mybob2.Draw(_spriteBatch);
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
                UpdatePosition(0, -3);
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                UpdatePosition(0, 3);
            }
            if (kstate.IsKeyDown(Keys.D))
            {
                UpdatePosition(3, 0);
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                UpdatePosition(-3, 0);
            }
        }
        public void Draw()
        {
            game1.Spritebatch.Draw(playerSprite, new Rectangle((int)position.X, (int)position.Y, playerSprite.Width, playerSprite.Height), Color.White);
        }
        public void UpdatePosition(int x, int y)
        {
            position += new Vector2(x, y);
        }
    }
    public class map
    {
        private Texture2D ground;
        private Texture2D tree1;
        protected Game1 game1;
        private int sizeX = 15;
        private int sizeY = 10;
        private bool canWalk;
        private bool isExit;

        public map(Game1 game)
        {
            game1 = game;
            ground = game.Content.Load<Texture2D>("ground1");
            tree1 = game.Content.Load<Texture2D>("tree1");
        }
        public void Draw()
        {

        }
        protected void GenerateMap()
        {
        }
    }
    public class bob : IDrawable
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Color DrawColor { get; set; }

        public weapon myWeapon;
        public bob(Vector2 newPosition, Color newColor)
        {
            Position = newPosition;
            DrawColor = newColor;
            Scale = new Vector2(50, 50);
            myWeapon = new weapon();
            myWeapon.owner = this;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.EasyDraw(this);
            Position = new Vector2(Position.X + 1, Position.Y);
            myWeapon.Draw(spriteBatch);
        }
    }
    public class weapon 
    {
        public bob owner;
        public Color weaponColor = Color.Red;
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Game1.square, new Rectangle((int)owner.Position.X + 22, (int)owner.Position.Y, 35, 8), weaponColor);
            spritebatch.Draw(Game1.square, new Rectangle((int)owner.Position.X, (int)owner.Position.Y - 20, 50, 22), weaponColor);
        }
    }
    public interface IDrawable
    {
        public Vector2 Position { get; set; }
        public Vector2 Scale { get; set; }
        public Color DrawColor { get; set; }
    }
}
using Cobalt.Ecs.Systems;
using Cobalt.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Cobalt.Core
{
    /// <summary>
    /// Extends the core MonoGame "Game" class with extensions for Cobalt. Which makes it impossible to unit-test.
    /// </summary>
    // Required to get the GraphicsDevice for loading images from disk.
    public class CobaltGame : Game
    {
        internal static CobaltGame Current { get; private set; }

        private const string JsonConfigFile = "Content/Data/Config.json";
        private readonly int GameWidth;
        private readonly int GameHeight;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        internal IList<ISystem> Systems { get; private set; } = new List<ISystem>();

        static CobaltGame()
        {
            if (System.IO.File.Exists(JsonConfigFile))
            {
                new Config(JsonConfigFile);
            }
        }

        /// <summary>
        /// Creates a new game. Specifies the size of the game at 100% zoom; the user
        /// may elect to resize the game window, it may be scaled on their device, etc.
        /// </summary>
        /// <param name="gameWidth"></param>
        /// <param name="gameHeight"></param>
        public CobaltGame(int gameWidth, int gameHeight) : base()
        {
            CobaltGame.Current = this;

            this.GameWidth = gameWidth;
            this.GameHeight = gameHeight;

            graphics = new GraphicsDeviceManager(this);
            this.SetWindowSize();

            Content.RootDirectory = "Content";

            if (Config.Instance.Get<bool>("showFps") == true)
            {
                Components.Add(new FrameRateCounter(this));
            }

            UseMonogameCodePaths();
            InitializeSystems();
        }
        
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);

            CobaltState.Current.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            CobaltState.Current.Draw(this.spriteBatch);

            base.Draw(gameTime);
        }

        private static void UseMonogameCodePaths()
        {
            // DI stuff
            KeyboardSystem.GenerateKeysCallback(() =>
            {
                var state = Keyboard.GetState();
                var toReturn = new List<Keys>();

                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (state.IsKeyDown(key))
                    {
                        toReturn.Add(key);
                    }
                }

                return toReturn.ToArray();
            });
        }

        private void SetWindowSize()
        {
            this.graphics.PreferredBackBufferWidth = this.GameWidth;
            this.graphics.PreferredBackBufferHeight = this.GameHeight;
            this.graphics.ApplyChanges();
        }

        private void InitializeSystems()
        {
            this.Systems.Add(new KeyboardSystem());
            this.Systems.Add(new VelocitySystem());
            // Must run after velocity system for consistent collisions
            this.Systems.Add(new CollisionSystem());
        }
    }
}

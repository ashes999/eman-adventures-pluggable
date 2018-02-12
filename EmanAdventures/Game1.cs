using Cobalt.Core;
using EmanAdventures.Core.Model;
using EmanAdventures.UI.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace EmanAdventures.UI
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : CobaltGame
    {
        private const int GameWidth = 960;
        private const int GameHeight = 540;
        private readonly World world;

        public Game1() : base(GameWidth, GameHeight)
        {
            this.world = new World(); // Initializes singleton
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            // Shows the starting state
            new WorldMapState();
            //new LocationMapState();
        }
    }
}

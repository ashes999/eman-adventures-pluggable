using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.GameComponents
{
    // Source: https://blogs.msdn.microsoft.com/shawnhar/2007/06/08/displaying-the-framerate/
    public class FrameRateCounter : DrawableGameComponent
    {
        ContentManager content;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;

        int fpsToDisplay = 0; // updated once a second
        int upsToDisplay = 0; // updated once a second
        int drawsSinceLastDisplay = 0;
        int updatesSinceLastDisplay = 0;
        TimeSpan timeSinceLastDisplay = TimeSpan.Zero;


        public FrameRateCounter(Game game)
            : base(game)
        {
            content = new ContentManager(game.Services);
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = content.Load<SpriteFont>("Content/Fonts/Arial");
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            content.Unload();
        }


        public override void Update(GameTime gameTime)
        {
            updatesSinceLastDisplay++;
            timeSinceLastDisplay += gameTime.ElapsedGameTime;

            if (timeSinceLastDisplay > TimeSpan.FromSeconds(1))
            {
                timeSinceLastDisplay -= TimeSpan.FromSeconds(1);

                fpsToDisplay = drawsSinceLastDisplay;
                upsToDisplay = updatesSinceLastDisplay;

                drawsSinceLastDisplay = 0;
                updatesSinceLastDisplay = 0;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            drawsSinceLastDisplay++;

            string fps = string.Format($"{fpsToDisplay} FPS / {upsToDisplay} UPS");

            spriteBatch.Begin();
            
            spriteBatch.DrawString(spriteFont, fps, new Vector2(9, 9), Color.Black); // drop shadow
            spriteBatch.DrawString(spriteFont, fps, new Vector2(8, 8), Color.White);

            spriteBatch.End();
        }
    }
}

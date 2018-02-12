using Cobalt.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Components
{
    /// <summary>
    /// A component for a 2D image (sprite).
    /// </summary>
    public class SpriteComponent : AbstractComponent
    {
        internal Texture2D Texture { get; private set; }

        internal Rectangle Rectangle;

        public SpriteComponent(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            this.Texture = Texture2DLoader.Load(filename);

            this.Rectangle = new Rectangle(0, 0, this.Texture.Width, this.Texture.Height);
        }

        public SpriteComponent(string filename, int sourceX, int sourceY, int width, int height) : this(filename)
        {
            // TODO: throw if x/y/width/height are zero/negative
            this.Rectangle = new Rectangle(sourceX, sourceY, width, height);
        }

        // For unit testing
        internal SpriteComponent(int width, int height)
        {
            this.Rectangle = new Rectangle(0, 0, width, height);
        }

        public Rectangle AsRectangle {  get { return this.Rectangle; } }
    }
}

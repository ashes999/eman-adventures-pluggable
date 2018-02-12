using Cobalt.Core;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.IO
{
    internal static class Texture2DLoader
    {
        public static Texture2D Load(string filename)
        {
            using (var stream = File.Open(filename, FileMode.Open))
            {
                return Texture2D.FromStream(CobaltGame.Current.GraphicsDevice, stream);
            }
        }
    }
}

using EmanAdventures.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace EmanAdventures.Core.IO
{
    public static class TmxMapDeserializer
    {
        public static TmxMap Deserialize(string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
            {
                throw new ArgumentNullException(nameof(filename));
            }

            return new TmxMap(filename);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.Core.Model.Map
{
    /// <summary>
    /// A dynamically-generated map. Each time you generate it, you'll get a different map.
    /// </summary>
    public class GeneratedMap
    {
        public Tile[] Tiles { get; protected set; }
        public string Tileset { get; protected set; }
        
        public GeneratedMap(string tilesetImage, int widthInTiles, int heightInTiles)
        {
            this.Tileset = tilesetImage;
            this.Tiles = new Tile[widthInTiles * heightInTiles];
        }
    }
}

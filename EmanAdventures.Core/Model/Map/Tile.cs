using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.Core.Model.Map
{
    public class Tile
    {
        // All four values are in pixels
        public int X { get; private set; }
        public int Y { get; private set; }
        public int SourceTileX { get; private set; }
        public int SourceTileY { get; private set; }
        public bool IsSolid { get; private set; }

        public Tile(int x, int y, int sourceTileX, int sourceTileY, bool isSolid)
        {
            this.X = x;
            this.Y = y;
            this.SourceTileX = sourceTileX;
            this.SourceTileY = sourceTileY;
            this.IsSolid = isSolid;
        }
    }
}

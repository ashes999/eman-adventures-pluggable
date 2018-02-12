using EmanAdventures.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace EmanAdventures.Core.Model.Map
{
    public class StaticMap
    {
        public Tile[] Tiles { get; private set; }

        // Assumes only one tileset
        public string Tileset { get { return this.mapData.Tilesets[0].Image.Source.Replace('/', '\\'); } }
        public int TileWidth { get { return this.mapData.TileWidth; } }
        public int TileHeight { get { return this.mapData.TileHeight; } }

        // In pixels
        public int Width { get { return this.mapData.Width * this.mapData.TileWidth; } }
        public int Height { get { return this.mapData.Height * this.mapData.TileHeight; } }

        private const string ExitType = "Exit";

        private TmxMap mapData;
        // In pixels
        public int ExitLocationX { get; private set; } = -1;
        public int ExitLocationY { get; private set; } = -1;

        public StaticMap(TmxMap mapData)
        {
            if (mapData == null)
            {
                throw new ArgumentNullException(nameof(mapData));
            }
            else if (mapData.Layers == null || !mapData.Layers.Any())
            {
                throw new ArgumentException("Map doesn't contain any tile layers.");
            }

            this.mapData = mapData;

            // populate this.Tiles with data needed for rendering, exiting, etc.
            this.Tiles = new Tile[this.mapData.Width * this.mapData.Height];
            var solidTileGids = new List<int>();

            foreach (var tileset in this.mapData.Tilesets)
            {
                solidTileGids.AddRange(tileset.Tiles
                    .Where(t => t.Properties.Any(p => p.Key == "Solid" && p.Value == "true"))
                    .Select(tile => tile.Id));
            }

            for (int y = 0; y < this.mapData.Height; y++)
            {
                for (int x = 0; x < this.mapData.Width; x++)
                {
                    // Assumes only one layer of tiles
                    var index = y * this.mapData.Width + x;
                    var sourceTile = this.mapData.Layers.First().Tiles[index];
                    // Assumes only one tileset. And only one row of tiles. And tiles are all non-transparent (Gid > 0).
                    var realGid = (sourceTile.Gid - 1);
                    this.Tiles[index] = new Tile(x * this.TileWidth, y * this.TileHeight, realGid * this.TileWidth, 0, solidTileGids.Contains(realGid));
                }
            }

            // find the one and only exit.
            foreach (var objectLayer in mapData.ObjectGroups)
            {
                foreach (var obj in objectLayer.Objects)
                {
                    if (obj.Type == ExitType)
                    {
                        ExitLocationX = (int)obj.X;
                        ExitLocationY = (int)obj.Y;
                        break;
                    }
                }
            }

            if (ExitLocationX == -1 || ExitLocationY == -1)
            {
                throw new ArgumentException($"Can't find exit object in map; make sure there's an object layer with an object with Type={ExitType}.");
            }
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.Core.Model.Map
{
    /// <summary>
    ///  A special type of generated map where things are generated once, and then locations are fixed.
    /// i.e. we want to persist this generated map, not regenerate it every game.
    /// </summary>
    public class WorldMap : GeneratedMap
    {
        public static WorldMap Instance { get; private set; }

        // Pixels
        public readonly Tuple<int, int> StartingPosition = new Tuple<int, int>(
            ((WidthInTiles / 2) - 1) * World.TileSize,
            ((HeightInTiles / 2) - 1) * World.TileSize);

        public readonly Tuple<int, int> TownPosition;

        public const int TileWidth = World.TileSize;
        public const int TileHeight = World.TileSize;

        // 960x540 with 48x48 tiles; well, a bit wider.
        internal const int WidthInTiles = 20;
        internal const int HeightInTiles = 12;

        // I don't know how else to do this. This is a map of tile-based indicies to locations.
        internal readonly IDictionary<string, Tuple<int, int>> TileData = new Dictionary<string, Tuple<int, int>>()
        {
            { "Ground", new Tuple<int, int>(0, 0) },
            { "Wall", new Tuple<int, int>(1, 0) },
            { "Town", new Tuple<int, int>(2, 0) }
        };


        public WorldMap() : base("Content/Images/Tilesets/World.png", WidthInTiles, HeightInTiles)
        {
            WorldMap.Instance = this;

            // Town position is player position minus two tiles high; x ranges from -1 to +1 tiles.
            var xOffset = World.Instance.SeededRandomGenerator.Next(-1, 1) * World.TileSize;
            TownPosition = new Tuple<int, int>(StartingPosition.Item1 + xOffset, StartingPosition.Item2 - 2 * World.TileSize);

            this.Tiles = new Map.Tile[WidthInTiles * HeightInTiles];

            for (var y = 0; y < HeightInTiles; y++)
            {
                for (var x = 0;  x < WidthInTiles; x++)
                {
                    var index = y * WidthInTiles + x;

                    var type = x * TileWidth == TownPosition.Item1 && y * TileHeight == TownPosition.Item2 ? "Town" : "Ground";
                    var tileData = TileData[type];

                    this.Tiles[index] = new Map.Tile(x * TileWidth, y * TileHeight, tileData.Item1 * TileWidth, tileData.Item2 * TileHeight, false);
                }
            }
        }
    }
}

using Cobalt.Core;
using Cobalt.Ecs;
using Cobalt.Ecs.Components;
using EmanAdventures.Core.Model;
using EmanAdventures.Core.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.UI.States
{
    class WorldMapState : CobaltState
    {
        private World world;

        public WorldMapState() :base()
        {
            this.world = World.Instance;
            var worldMap = world.WorldMap;

            // Create one entity per tile
            for (var i = 0; i < worldMap.Tiles.Length; i++)
            {
                var sourceTile = worldMap.Tiles[i];

                this.Add(new CobaltEntity()
                    .Tile(worldMap.Tileset,
                    sourceTile.SourceTileX, sourceTile.SourceTileY, WorldMap.TileWidth, WorldMap.TileHeight)
                    .Move(sourceTile.X, sourceTile.Y));
            }

            var player = new CobaltEntity("remove me").Sprite("Content/Images/Player.png")
                .MoveToKeyboard(100)
                .Move(world.WorldMap.StartingPosition.Item1, world.WorldMap.StartingPosition.Item2);

            this.Add(player);

            Console.WriteLine($"Moved player to {player.Get<PositionComponent>().AsVector2}");
        }
    }
}

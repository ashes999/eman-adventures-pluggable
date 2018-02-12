using Cobalt.Core;
using Cobalt.Ecs;
using EmanAdventures.Core.IO;
using EmanAdventures.Core.Model;
using EmanAdventures.Core.Model.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.UI.States
{
    class LocationMapState : CobaltState
    {
        private StaticMap currentMap;
        private CobaltEntity player;

        public LocationMapState() : base()
        {
            this.currentMap = new StaticMap(TmxMapDeserializer.Deserialize("Content/Maps/town.tmx"));

            // Create one entity per tile
            for (var i = 0; i < this.currentMap.Tiles.Length; i++)
            {
                var sourceTile = this.currentMap.Tiles[i];

                this.Add(new CobaltEntity()
                    .Tile(this.currentMap.Tileset,
                    sourceTile.SourceTileX, sourceTile.SourceTileY, this.currentMap.TileWidth, this.currentMap.TileHeight)
                    .Move(sourceTile.X, sourceTile.Y));
            }

            this.player = new CobaltEntity().Sprite("Content/Images/Player.png")
                .MoveToKeyboard(100)
                .Move( // Locate just two tiles above the exit.
                    this.currentMap.ExitLocationX, this.currentMap.ExitLocationY - 2 * this.currentMap.TileHeight);

            this.Add(this.player);
        }
    }
}

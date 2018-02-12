using EmanAdventures.Core.Model;
using EmanAdventures.Core.Model.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.Core.UnitTests.Model.Map
{
    [TestFixture]
    class WorldMapTests
    {
        [Test]
        public void ConstructorSetsInstanceToThis()
        {
            new World();

            var w1 = new WorldMap();
            Assert.That(WorldMap.Instance, Is.EqualTo(w1));

            var w2 = new WorldMap();
            Assert.That(WorldMap.Instance, Is.EqualTo(w2));
        }

        [Test]
        public void ConstructorSetsTilesetAndMapSizeAndStartingPosition()
        {
            new World();
            var actual = new WorldMap();
            Assert.That(!string.IsNullOrWhiteSpace(actual.Tileset));
            Assert.That(actual.Tiles.Length, Is.GreaterThan(0));
            Assert.That(actual.StartingPosition.Item1, Is.GreaterThan(0));
            Assert.That(actual.StartingPosition.Item2, Is.GreaterThan(0));
        }

        [Test]
        public void ConstructorSetsTownAdjacentAndTwoTilesAbovePlayer()
        {
            var world = new World();
            var actual = new WorldMap();

            var playerPosition = actual.StartingPosition;
            var townPosition = actual.TownPosition;

            Assert.That(townPosition.Item1 >= playerPosition.Item1 - World.TileSize &&
                townPosition.Item1 <= playerPosition.Item1 + World.TileSize);

            Assert.That(townPosition.Item2 == playerPosition.Item2 - 2 * World.TileSize);
        }

        [Test]
        public void ConstructorGeneratesAllTilesIncludingTown()
        {
            var world = new World();
            var map = new WorldMap();
            var townPosition = map.TownPosition;

            var tiles = map.Tiles;
            var expectedTotalTiles = WorldMap.WidthInTiles * WorldMap.HeightInTiles;
            Assert.That(tiles.Length, Is.EqualTo(expectedTotalTiles));

            var townTile = tiles.Single(t => t.SourceTileX == map.TileData["Town"].Item1 * World.TileSize &&
                t.SourceTileY == map.TileData["Town"].Item2 * World.TileSize);
            Assert.That(townTile, Is.Not.Null);

            var otherTiles = tiles.Where(t => t != townTile);
            Assert.That(otherTiles.Count(), Is.EqualTo(expectedTotalTiles - 1));
            foreach (var tile in otherTiles)
            {
                Assert.That(tile.IsSolid, Is.False);
                Assert.That(tile.SourceTileX, Is.EqualTo(map.TileData["Ground"].Item1 * World.TileSize));
                Assert.That(tile.SourceTileY, Is.EqualTo(map.TileData["Ground"].Item2 * World.TileSize));
            }
        }
    }
}

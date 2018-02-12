using EmanAdventures.Core.Model.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace EmanAdventures.Core.UnitTests.Model.Map
{
    [TestFixture]
    class StaticMapTests
    {
        internal static string PathToEmptyMap = "TestData/Maps/Empty.tmx";
        internal static string PathToEmptyMapWithLayer = "TestData/Maps/EmptyLayer.tmx";
        internal static string PathToMinimalMap = "TestData/Maps/Minimal.tmx";
        internal static string PathToSolidTilesetMap = "TestData/Maps/MapWithSolidTiles.tmx";

        [Test]
        public void ConstructorThrowsIfMapDataIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new StaticMap(null));
        }

        [Test]
        public void ConstructorThrowsIfExitDoesntHaveAnyTileLayers()
        {
            var ex = Assert.Throws<ArgumentException>(() => new StaticMap(new TmxMap(PathToEmptyMap)));
            Assert.That(ex.Message.Contains("tile layers"));
        }

        [Test]
        public void ConstructorThrowsIfExitDoesntExist()
        {
            var ex = Assert.Throws<ArgumentException>(() => new StaticMap(new TmxMap(PathToEmptyMapWithLayer)));
            Assert.That(ex.Message.Contains("Type=Exit"));
        }

        [Test]
        public void ConstructorDoesntThrowIfMapDataIsNotNullAndExitExists()
        {
            new StaticMap(new TmxMap(PathToMinimalMap));
        }

        [Test]
        public void ConstructorSetsSolidTileValueFromTileset()
        {
            var map = new StaticMap(new TmxMap(PathToSolidTilesetMap));
            Assert.That(map.Tiles.Any(t => t.IsSolid));
        }
    }
}

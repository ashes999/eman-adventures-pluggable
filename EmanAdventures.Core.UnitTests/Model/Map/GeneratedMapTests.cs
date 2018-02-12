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
    class GeneratedMapTests
    {
        [Test]
        public void ConstructorInitializesEmptyTilesetDataToSpecifiedWidthAndHeight()
        {
            var expectedWidth = 16;
            var expectedHeight = 10;
            var expectedImage = "assets/images/tiles/town.png";

            var actual = new GeneratedMap(expectedImage, expectedWidth, expectedHeight);

            Assert.That(actual.Tileset, Is.EqualTo(expectedImage));
            Assert.That(actual.Tiles.Length, Is.EqualTo(expectedWidth * expectedHeight));
        }
    }
}

using EmanAdventures.Core.IO;
using EmanAdventures.Core.Model;
using EmanAdventures.Core.UnitTests.Model;
using EmanAdventures.Core.UnitTests.Model.Map;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TiledSharp;

namespace EmanAdventures.Core.UnitTests.IO
{
    [TestFixture]
    class TmxMapDeserializerTests
    {
        [TestCase((string)null)]
        [TestCase("")]
        [TestCase("           ")]
        public static void DeserializeThrowsIfArgumentIsNullOrEmpty(string filename)
        {
            Assert.Throws<ArgumentNullException>(() => TmxMapDeserializer.Deserialize(filename));
        }

        [Test]
        public void DeserializeDoesntThrowIfArgumentIsATmxMap()
        {
            TmxMapDeserializer.Deserialize(StaticMapTests.PathToEmptyMap);
        }
    }
}

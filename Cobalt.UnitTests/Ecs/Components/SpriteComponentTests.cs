using Cobalt.Ecs.Components;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.UnitTests.Ecs.Components
{
    [TestFixture]
    class SpriteComponentTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("     ")]
        public void ConstructorThrowsIfFilenameIsNullOrEmpty(string filename)
        {
            Assert.Throws<ArgumentNullException>(() => new SpriteComponent(filename));
        }
    }
}

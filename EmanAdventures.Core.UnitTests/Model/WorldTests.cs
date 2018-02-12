using EmanAdventures.Core.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmanAdventures.Core.UnitTests.Model
{
    [TestFixture]
    class WorldTests
    {
        [Test]
        public void ConstructorUsesSpecifiedSeed()
        {
            var expected = 12345;
            var world = new World(expected);
            Assert.That(world.Seed, Is.EqualTo(expected));
        }

        [Test]
        public void ConstructorGeneratesRandomPositiveSeedIfParameterIsNull()
        {
            var w1 = new World();
            Assert.That(w1.Seed, Is.GreaterThanOrEqualTo(0));

            var w2 = new World();
            Assert.That(w2.Seed, Is.GreaterThanOrEqualTo(0));

            Assert.That(w1.Seed, Is.Not.EqualTo(w2.Seed));
        }

        [Test]
        public void ConstructorSetsInstanceToThis()
        {
            var w1 = new World();
            Assert.That(World.Instance, Is.EqualTo(w1));

            var w2 = new World();
            Assert.That(World.Instance, Is.EqualTo(w2));
        }
    }
}

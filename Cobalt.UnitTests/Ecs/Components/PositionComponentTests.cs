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
    class PositionComponentTests
    {
        [Test]
        public void XAndYGettersGetValuesSet()
        {
            var expectedX = 1.2182f;
            var expectedY = 3.999f;
            var component = new PositionComponent(expectedX, expectedY);
            Assert.That(component.X, Is.EqualTo(expectedX));
            Assert.That(component.Y, Is.EqualTo(expectedY));
        }

        [Test]
        public void AsVectorGetsPositionAsVector()
        {
            var component = new PositionComponent(13, 39);
            var actual = component.AsVector2;
            Assert.That(actual.X, Is.EqualTo(component.X));
            Assert.That(actual.Y, Is.EqualTo(component.Y));
        }
    }
}

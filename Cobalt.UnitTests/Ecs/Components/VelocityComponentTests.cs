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
    class VelocityComponentTests
    {
        [Test]
        public void ConstructorSetsVelocityComponents()
        {
            var expectedX = 17;
            var expectedY = -19;
            var velocity = new VelocityComponent(expectedX, expectedY);

            Assert.That(velocity.X, Is.EqualTo(expectedX));
            Assert.That(velocity.Y, Is.EqualTo(expectedY));
        }

        [Test]
        public void XYGettersGetSetValues()
        {
            var expectedX = 17;
            var expectedY = -19;
            var velocity = new VelocityComponent(0, 0);

            velocity.X = expectedX;
            velocity.Y = expectedY;
            Assert.That(velocity.X, Is.EqualTo(expectedX));
            Assert.That(velocity.Y, Is.EqualTo(expectedY));
        }
    }
}

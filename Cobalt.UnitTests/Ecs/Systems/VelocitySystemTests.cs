using Cobalt.Ecs;
using Cobalt.Ecs.Components;
using Cobalt.Ecs.Systems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.UnitTests.Ecs.Systems
{
    [TestFixture]
    class VelocitySystemTests
    {
        [Test]
        public void UpdateUpdatesPositionRelativeToElapsedTime()
        {
            var vX = 17;
            var vY = -19;
            var velocity = new VelocityComponent(vX, vY);

            var pX = 400;
            var pY = 128;
            var position = new PositionComponent(pX, pY);

            var elapsed = 0.7f;
            var e = new CobaltEntity();

            e.Set(velocity);
            e.Set(position);

            var system = new VelocitySystem();
            system.Add(e);
            system.Update(elapsed);

            Assert.That(position.X, Is.EqualTo(pX + (elapsed * vX)));
            Assert.That(position.Y, Is.EqualTo(pY + (elapsed * vY)));
        }
    }
}

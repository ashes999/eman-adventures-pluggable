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
    class AbstractSystemTests
    {
        [Test]
        public void AddAddsEntitiesWithRequiredComponents()
        {
            var system = new TestableSystem(new Type[] { typeof(VelocityComponent) });
            var expected = new CobaltEntity().Velocity(1, -1);

            system.Add(new CobaltEntity());
            system.Add(expected);

            Assert.That(system.Entities.Count, Is.EqualTo(1));
            Assert.That(system.Entities.Single(), Is.EqualTo(expected));
        }

        private class TestableSystem : AbstractSystem
        {
            public TestableSystem(IEnumerable<Type> requiredComponents) : base(requiredComponents)
            {
            }

            public override void Update(float elapsedSeconds)
            {
            }
        }
    }
}

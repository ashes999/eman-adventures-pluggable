using Cobalt.Core;
using Cobalt.Ecs;
using Cobalt.Ecs.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.UnitTests.Core
{
    [TestFixture]
    class CobaltStateTests
    {
        [Test]
        public void ConstructorSetsCurrentToLatestInstance()
        {
            var s1 = new CobaltState();
            Assert.That(CobaltState.Current, Is.EqualTo(s1));

            var s2 = new CobaltState();
            Assert.That(CobaltState.Current, Is.EqualTo(s2));
        }

        [Test]
        public void UpdateCallsUpdateOnAllSystems()
        {
            var system = new IntegerSystem();
            var state = new CobaltState(new ISystem[] { system });
            state.Update(CobaltState.MaxUpdatePerTickSeconds / 2);
            Assert.That(system.NumUpdates, Is.EqualTo(1));
        }

        [Test]
        public void UpdateBreaksBiggerUpdatesIntoMultiplePieces()
        {
            var system = new IntegerSystem();
            var state = new CobaltState(new ISystem[] { system });
            state.Update(CobaltState.MaxUpdatePerTickSeconds * 3);
            Assert.That(system.NumUpdates, Is.EqualTo(3));
        }

        private class IntegerSystem : ISystem
        {
            public int NumUpdates = 0;

            public void Add(CobaltEntity entity)
            {
            }

            public void Update(float elapsedSeconds)
            {
                NumUpdates++;
            }
        }

    }
}

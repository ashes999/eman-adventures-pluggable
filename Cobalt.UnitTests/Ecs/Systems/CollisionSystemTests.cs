using Cobalt.Ecs;
using Cobalt.Ecs.Components;
using Cobalt.Ecs.Systems;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Cobalt.UnitTests.Ecs.Systems
{
    [TestFixture]
    class CollisionSystemTests
    {
        private const int TextureWidth = 32;
        private const int TextureHeight = 16;

        [Test]
        public void EntitiesRequirePositionSpriteAndCollisionComponents()
        {
            var e = new CobaltEntity();
            
            var system = new CollisionSystem();
            system.Add(e);
            Assert.That(!system.Entities.Any());

            // Position component is there by default
            e.Set(new SpriteComponent(32, 16));
            system.Add(e);
            Assert.That(!system.Entities.Any());

            e.Set(new CollisionComponent("Player"));
            system.Add(e);
            Assert.That(system.Entities.Single(), Is.EqualTo(e));

            system = new CollisionSystem();
            e.Set(new CollisionComponent("Player", "Wall", (p, w) => { }));
            system.Add(e);
            Assert.That(system.Entities.Single(), Is.EqualTo(e));
        }

        [Test]
        public void UpdateCallsOnCollideWhenCollisionComponentIsSetAndBoundingBoxesOverlap()
        {
            // Move them so that they overlap, and test.
            var wall = new CobaltEntity();
            wall.Move(0, 0);
            wall.Set(new CollisionComponent("Wall"));
            wall.Set(new SpriteComponent(100, 16));

            var player = new CobaltEntity();
            player.Move(50, 10); // X/Y overlaps wall
            player.Set(new SpriteComponent(32, 32));

            var wasCallbackCalled = false;
            player.Set(new CollisionComponent("Player", "Wall", (p, w) => wasCallbackCalled = true));

            var system = new CollisionSystem();
            system.Add(wall);
            system.Add(player);
            Assert.That(system.Entities.Count == 2, "System doesn't have the player and wall entities.");

            system.Update(0.1f);

            Assert.That(wasCallbackCalled, Is.True);

            // Move them so that they don't overlap, and re-test that it didn't trigger the collision resolution action

            wasCallbackCalled = false;
            player.Move(999, 888);
            system.Update(0.1f);

            Assert.That(wasCallbackCalled, Is.False);
        }
    }
}

using Cobalt.Ecs;
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
    class CollisionComponentTests
    {
        [Test]
        public void ConstructorSetsOwnerTag()
        {
            // Used to set something collideable where the other side handles the collision
            // eg. this is a wall and the player will collide against it
            var expectedTag = "Wall";

            var c = new CollisionComponent(expectedTag);

            Assert.That(c.OwnerTag, Is.EqualTo(expectedTag));
            Assert.That(c.TargetTag, Is.Null);
            Assert.That(c.OnCollide, Is.Null);
        }

        [Test]
        public void ConstructorSetsTagsAndCallback()
        {
            // Used to set something collideable where we handle the collision
            // eg. this is a player who will collect a coin
            var expectedOwnerTag = "Player";
            var expectedTargetTag = "Coin";
            Action<CobaltEntity, CobaltEntity> expectedCallback = (player, coin) => { Console.WriteLine("Player gets a coin!"); };

            var c = new CollisionComponent(expectedOwnerTag, expectedTargetTag, expectedCallback);

            Assert.That(c.OwnerTag, Is.EqualTo(expectedOwnerTag));
            Assert.That(c.TargetTag, Is.EqualTo(expectedTargetTag));
            Assert.That(c.OnCollide, Is.EqualTo(expectedCallback));
        }
    }
}

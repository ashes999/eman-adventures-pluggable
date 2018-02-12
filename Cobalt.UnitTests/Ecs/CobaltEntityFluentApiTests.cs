using Cobalt.Ecs;
using Cobalt.Ecs.Components;
using Cobalt.Ecs.Systems;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.UnitTests.Ecs
{
    [TestFixture]
    class CobaltEntityFluentApiTests
    {
        [Test]
        public void MoveSetsPositionComponent()
        {
            var entity = new CobaltEntity();
            var expected = new PositionComponent(171, 201);
            entity.Move(expected.X, expected.Y);

            var actual = entity.Get<PositionComponent>();
            // They're value types, they're two separate instances.
            Assert.That(actual.X, Is.EqualTo(expected.X));
            Assert.That(actual.Y, Is.EqualTo(expected.Y));
        }

        [Test]
        public void MoveOverridesExistingPosition()
        {
            var entity = new CobaltEntity();
            var expected = new PositionComponent(171, 201);
            entity.Move(999, 999);
            entity.Move(expected.X, expected.Y);

            var actual = entity.Get<PositionComponent>();
            // They're value types, they're two separate instances.
            Assert.That(actual.X, Is.EqualTo(expected.X));
            Assert.That(actual.Y, Is.EqualTo(expected.Y));
        }

        [Test]
        public void MoveToKeyboardSetsKeyboardComponent()
        {
            var entity = new CobaltEntity();
            entity.MoveToKeyboard(100);

            var actual = entity.Get<KeyboardComponent>();
            Assert.That(actual, Is.Not.Null);
        }

        [Test]
        public void MoveToKeyboardAddsPositionComponentIfOneDoesntExist()
        {
            var entity = new CobaltEntity();
            entity.MoveToKeyboard(10);

            Assert.That(entity.Has<PositionComponent>());

            var actual = entity.Get<PositionComponent>();
            Assert.That(actual.X, Is.EqualTo(0));
            Assert.That(actual.Y, Is.EqualTo(0));
        }

        [Test]
        public void MoveToKeyboardDoesntOverrideExistingPositionComponent()
        {
            var expectedX = 100;
            var expectedY = 200;

            var entity = new CobaltEntity().Move(expectedX, expectedY);
            entity.MoveToKeyboard(10);

            var actual = entity.Get<PositionComponent>();
            Assert.That(actual.X, Is.EqualTo(expectedX));
            Assert.That(actual.Y, Is.EqualTo(expectedY));
        }

        [Test]
        public void MoveToKeyboardAddsVelocityComponentWithWASDCallbacksIfOneDoesntExist()
        {
            var entity = new CobaltEntity();
            entity.MoveToKeyboard(10);

            Assert.That(entity.Has<VelocityComponent>());
            var actual = entity.Get<VelocityComponent>();
            Assert.That(actual.X, Is.EqualTo(0));
            Assert.That(actual.Y, Is.EqualTo(0));
        }

        [Test]
        public void MoveToKeyboardDoesntOverrideExistingVelocityComponent()
        {
            var expectedX = 100;
            var expectedY = 200;

            var entity = new CobaltEntity();
            var v = new VelocityComponent(expectedX, expectedY);
            entity.Set(v);
            entity.MoveToKeyboard(10);

            var actual = entity.Get<VelocityComponent>();
            Assert.That(actual.X, Is.EqualTo(expectedX));
            Assert.That(actual.Y, Is.EqualTo(expectedY));
        }

        [Test]
        public void MoveToKeyboardOverridesExistingKeyboardComponent()
        {
            // The only way to test this is to change the velocity and see if the new velocity number is used
            // Set velocity to 100, press D, wait one second; we should move 100px to the right.
            var expectedVelocity = 100;

            var entity = new CobaltEntity();

            entity.MoveToKeyboard(1);
            entity.MoveToKeyboard(expectedVelocity);

            KeyboardSystem.GenerateKeysCallback(() =>
            {
                // Press D to move X positively
                return new Keys[] { Keys.D };
            });

            // Simulate keypress
            var keyboardSystem = new KeyboardSystem();
            keyboardSystem.Add(entity);
            keyboardSystem.Update(1);

            var velocitySystem = new VelocitySystem();
            velocitySystem.Add(entity);
            velocitySystem.Update(1);

            Assert.That(entity.Get<PositionComponent>().X, Is.EqualTo(expectedVelocity));
        }

        [Test]
        public void VelocityAddsVelocityComponent()
        {
            var entity = new CobaltEntity();
            var expected = new VelocityComponent(11, 22);
            entity.Velocity(expected.X, expected.Y);

            var actual = entity.Get<VelocityComponent>();
            Assert.That(actual.X, Is.EqualTo(expected.X));
            Assert.That(actual.Y, Is.EqualTo(expected.Y));
        }

        [Test]
        public void VelocityOverrideExistingVelocityComponent()
        {
            var entity = new CobaltEntity();
            var expected = new VelocityComponent(111, 222);
            entity.Velocity(-1, -10);
            entity.Velocity(expected.X, expected.Y);

            var actual = entity.Get<VelocityComponent>();
            Assert.That(actual.X, Is.EqualTo(expected.X));
            Assert.That(actual.Y, Is.EqualTo(expected.Y));
        }
    }
}

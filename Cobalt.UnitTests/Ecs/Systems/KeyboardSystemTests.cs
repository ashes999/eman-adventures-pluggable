using Cobalt.Ecs.Components;
using Cobalt.Ecs.Systems;
using Microsoft.Xna.Framework.Input;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.UnitTests.Ecs.Systems
{
    [TestFixture]
    class KeyboardSystemTests
    {
        private Keys[] pressedKeys;

        [Test]
        public void ConstructorSetsStaticInstance()
        {
            var expected = new KeyboardSystem();
            Assert.That(KeyboardSystem.Instance, Is.EqualTo(expected));
        }

        [Test]
        public void IsPressedReturnsTrueIfKeyIsPressed()
        {
            var system = new KeyboardSystem();
            this.pressedKeys = new[] { Keys.W };

            Assert.IsTrue(system.IsPressed(Keys.W));
            Assert.IsFalse(system.IsPressed(Keys.A));
            Assert.IsFalse(system.IsPressed(Keys.Z));
            Assert.IsFalse(system.IsPressed(Keys.X));
            Assert.IsFalse(system.IsPressed(Keys.J));
        }


        [TestFixtureSetUp]
        public void SetupKeyInputInjection()
        {
            // This technically has side-effects, but no other tests should care
            // (outside of this suite) about keyboard input.
            KeyboardSystem.GenerateKeysCallback(() =>
            {
                return this.pressedKeys;
            });
        }

        [Test]
        public void UpdateTriggersOnPressedCallback()
        {
            var system = new KeyboardSystem();

            // set up callback
            var timesCalled = 0;
            var component = new KeyboardComponent();
            component.OnPress(Keys.F, () => timesCalled++);

            var e = new Cobalt.Ecs.CobaltEntity();
            e.Set(component);
            system.Add(e);

            // update: should be called
            this.pressedKeys = new[] { Keys.F };
            system.Update(1);
            Assert.That(timesCalled == 1);

            // update: shouldn't be called again
            system.Update(1);
            Assert.That(timesCalled == 1);

            // release: shouldn't be called
            this.pressedKeys = new Keys[0];
            system.Update(1);
            Assert.That(timesCalled == 1);

            // presss G instead; shouldn't be called
            this.pressedKeys = new[] { Keys.G };
            system.Update(1);
            Assert.That(timesCalled == 1);
        }

        [Test]
        public void UpdateTriggersOnReleasedCallback()
        {
            var system = new KeyboardSystem();

            // set up callback
            var timesCalled = 0;
            var component = new KeyboardComponent();
            component.OnRelease(Keys.F, () => timesCalled++);

            var e = new Cobalt.Ecs.CobaltEntity();
            e.Set(component);
            system.Add(e);

            // update: shouldn't be called
            this.pressedKeys = new[] { Keys.F };
            system.Update(1);
            Assert.That(timesCalled == 0);

            // release and update: should be called
            this.pressedKeys = new Keys[0];
            system.Update(1);
            Assert.That(timesCalled == 1);

            // update again; shouldn't be called
            system.Update(1);
            Assert.That(timesCalled == 1);

            // press/release G instead; shouldn't be called
            this.pressedKeys = new[] { Keys.G };
            system.Update(1);
            this.pressedKeys = new Keys[0];
            system.Update(1);

            Assert.That(timesCalled == 1);
        }
    }
}

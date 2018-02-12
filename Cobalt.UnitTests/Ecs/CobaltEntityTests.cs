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
    class CobaltEntityTests
    {
        [Test]
        public void ConstructorAddsZeroValuePositionComponent()
        {
            var entity = new CobaltEntity();
            Assert.IsTrue(entity.Has<PositionComponent>());

            var actual = entity.Get<PositionComponent>();
            Assert.That(actual.X, Is.EqualTo(0));
            Assert.That(actual.Y, Is.EqualTo(0));
        }

        [Test]
        public void GetGetsSetComponent()
        {
            var entity = new CobaltEntity();
            var expected = new PositionComponent(1, 2); 
            entity.Set(expected);

            Assert.That(entity.Get<PositionComponent>(), Is.EqualTo(expected));
        }

        [Test]
        public void SetOverwritesPreviousComponentOfThatType()
        {
            var entity = new CobaltEntity();
            var expected = new PositionComponent(1, 2);
            entity.Set(new PositionComponent(9, 9));
            entity.Set(expected);

            Assert.That(entity.Get<PositionComponent>(), Is.EqualTo(expected));
        }

        [Test]
        public void GetThrowsIfComponentIsntSet()
        {
            Assert.Throws<KeyNotFoundException>(() => new CobaltEntity().Get<StringComponent>());
        }

        [Test]
        public void HasReturnsTrueForSetComponents()
        {
            var entity = new CobaltEntity();
            Assert.That(entity.Has<PositionComponent>(), Is.True);
            Assert.That(entity.Has<StringComponent>(), Is.False);

            Assert.That(entity.Has(typeof(PositionComponent)), Is.True);
            Assert.That(entity.Has(typeof(StringComponent)), Is.False);
        }


        private class StringComponent : AbstractComponent { }
    }
}

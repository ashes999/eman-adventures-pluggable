using Cobalt.Ecs.Components;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Systems
{
    class KeyboardSystem : AbstractSystem
    {
        internal static KeyboardSystem Instance;

        // Function we invoke to get data about pressed keys. In production code, uses
        // MonoGame code (via SDL); for unit tests, it's just static content.
        private static Func<Keys[]> GetPressedKeys;

        private Keys[] pressedLastUpdate = new Keys[0];

        internal KeyboardSystem() : base(new Type[] {  typeof(KeyboardComponent) })
        {
            KeyboardSystem.Instance = this;
        }

        override public void Update(float elapsedSeconds)
        {
            var keys = KeyboardSystem.GetPressedKeys();

            foreach (var entity in this.Entities)
            {
                var component = entity.Get<KeyboardComponent>();
                var pressed = keys.Where(k => !pressedLastUpdate.Contains(k) && component.OnPressedCallbacks.ContainsKey(k));
                foreach (var k in pressed)
                {
                    component.OnPressedCallbacks[k]();
                }

                var released = pressedLastUpdate.Where(k => !keys.Contains(k) && component.OnReleasedCallbacks.ContainsKey(k));
                foreach (var k in released)
                {
                    component.OnReleasedCallbacks[k]();
                }
            }

            pressedLastUpdate = keys;
        }

        public bool IsPressed(Keys key)
        {
            return KeyboardSystem.GetPressedKeys().Contains(key);
        }

        // Required to set up; poor man's DI
        internal static void GenerateKeysCallback(Func<Keys[]> callback)
        {
            KeyboardSystem.GetPressedKeys = callback;
        }
    }
}

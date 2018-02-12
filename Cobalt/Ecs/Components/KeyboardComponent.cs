using Cobalt.Ecs.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Components
{
    public class KeyboardComponent : AbstractComponent
    {
        internal Dictionary<Keys, Action> OnPressedCallbacks = new Dictionary<Keys, Action>();
        internal Dictionary<Keys, Action> OnReleasedCallbacks = new Dictionary<Keys, Action>();
        
        public KeyboardComponent OnPress(Keys key, Action callback)
        {
            this.OnPressedCallbacks[key] = callback;
            return this;
        }

        public KeyboardComponent OnRelease(Keys key, Action callback)
        {
            this.OnReleasedCallbacks[key] = callback;
            return this;
        }
    }
}

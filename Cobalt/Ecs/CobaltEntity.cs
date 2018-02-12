using Cobalt.Core;
using Cobalt.Ecs.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs
{
    /// <summary>
    /// An "entity." All entities have coordinates, but you can build them (using
    /// a fluent API) to contain whatever you want -- draw a sprite, respond to
    /// user input, resolve collisions, etc.
    /// </summary>
    public partial class CobaltEntity
    {
        private IDictionary<Type, AbstractComponent> components;

        public CobaltEntity()
        {
            this.components = new Dictionary<Type, AbstractComponent>();
            this.Move(0, 0);
        }

        public void Set(AbstractComponent component)
        {
            this.components[component.GetType()] = component;
        }

        public bool Has<T>() where T : AbstractComponent
        {
            return this.components.ContainsKey(typeof(T));
        }

        public bool Has(Type type)
        {
            return this.components.ContainsKey(type);
        }
        
        public T Get<T>() where T : AbstractComponent
        {
            return (T)this.components[typeof(T)];
        }
    }
}

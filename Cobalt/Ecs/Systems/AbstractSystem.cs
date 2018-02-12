using Cobalt.Ecs.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Systems
{
    abstract class AbstractSystem : ISystem
    {
        internal IList<CobaltEntity> Entities = new List<CobaltEntity>();
        private IEnumerable<Type> requiredComponents;

        public AbstractSystem(IEnumerable<Type> requiredComponents)
        {
            this.requiredComponents = requiredComponents;
        }

        public void Add(CobaltEntity entity)
        {
            foreach (var type in this.requiredComponents)
            {
                if (!entity.Has(type))
                {
                    return;
                }
            }

            this.Entities.Add(entity);
            this.OnAdd(entity);
        }

        public abstract void Update(float elapsedSeconds);

        /// <summary>
        /// Callback that you can override if you need a system to know when an entity was added.
        /// By default, doesn't do anything. For a use-case, see the collision system.
        /// </summary>
        protected virtual void OnAdd(CobaltEntity entity)
        {

        }
    }
}

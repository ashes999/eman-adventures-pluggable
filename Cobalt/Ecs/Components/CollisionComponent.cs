using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Components
{
    /// <summary>
    /// Indicates that an entity is collideable, or collides with something. Polymorphic.
    /// If the MyTag property is set, it's collideable (can collide but something else sets the collision callback).
    /// If the TargetTag property is set, that shows that it collides with something else.
    /// </summary>
    public class CollisionComponent : AbstractComponent
    {
        public readonly string OwnerTag;
        public readonly string TargetTag;
        public Action<CobaltEntity, CobaltEntity> OnCollide;

        /// <summary>
        /// Makes the entity collideable, but doesn't specify what collides with it or what to do.
        /// </summary>
        /// <param name="ownerTag">The parent entity's tag.</param>
        public CollisionComponent(string ownerTag)
        {
            this.OwnerTag = ownerTag;
        }

        public CollisionComponent(string ownerTag, string targetTag, Action<CobaltEntity, CobaltEntity> onCollide) : this(ownerTag)
        {
            this.TargetTag = targetTag;
            this.OnCollide = onCollide;
        }
    }
}

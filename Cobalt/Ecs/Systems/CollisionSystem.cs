using Cobalt.Ecs.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Systems
{
    // TODO: we should support removing dead entities.
    class CollisionSystem : AbstractSystem
    {
        // Denormalized because searching for this in every tick in Update is overkill.
        private IList<CollisionComponent> collisionComponentsWithCallbacks = new List<CollisionComponent>();

        public CollisionSystem() :  base(new Type[] { typeof(PositionComponent), typeof(SpriteComponent), typeof(CollisionComponent) })
        {
        }

        public override void Update(float elapsedSeconds)
        {
            foreach (var set in this.collisionComponentsWithCallbacks)
            {
                // Use case: create wall (component with OwnerTag = "Wall"), create player to collide (OwnerTag = "Player", "TargetTag" = "Wall")
                var owners = this.Entities.Where(e => e.Get<CollisionComponent>().OwnerTag == set.OwnerTag); // find players
                // Find all entities with the second tag, eg. player; NOT entities specifying "collide against the second tag."
                var collideAgainst = this.Entities.Where(e => e.Get<CollisionComponent>().OwnerTag == set.TargetTag); // find walls

                // check for overlap and collide
                foreach (var e1 in owners)
                {
                    foreach (var e2 in collideAgainst)
                    {
                        if (this.BoundingBoxesOverlap(e1, e2)) 
                        {
                            set.OnCollide(e1, e2);
                        }
                    }
                }
            }
        }

        protected override void OnAdd(CobaltEntity entity)
        {
            base.OnAdd(entity);

            var collisionComponent = entity.Get<CollisionComponent>();
            if (collisionComponent.OnCollide != null)
            {
                this.collisionComponentsWithCallbacks.Add(collisionComponent);
            }
        }

        private bool BoundingBoxesOverlap(CobaltEntity e1, CobaltEntity e2)
        {
            var p1 = e1.Get<PositionComponent>();
            var s1 = e1.Get<SpriteComponent>();

            var p2 = e2.Get<PositionComponent>();
            var s2 = e2.Get<SpriteComponent>();


            var r1Left = p1.X;
            var r1Right = p1.X + s1.Rectangle.Width;
            var r1Top = p1.Y;
            var r1Bottom = p1.Y + s1.Rectangle.Height;

            var r2Left = p2.X;
            var r2Right = p2.X + s2.Rectangle.Width;
            var r2Top = p2.Y;
            var r2Bottom = p2.Y + s2.Rectangle.Height;

            // AABB. Adapted from: https://gamedev.stackexchange.com/a/913
            return !(r2Left > r1Right
                || r2Right < r1Left
                || r2Top > r1Bottom
                || r2Bottom < r1Top);
        }
    }
}

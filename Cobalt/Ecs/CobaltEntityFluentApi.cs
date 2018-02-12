using Cobalt.Core;
using Cobalt.Ecs.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs
{
    public partial class CobaltEntity
    {
        /// <summary>
        /// Collide with entities tagged as <c>targetTag</c>, calling <c>onCollide</c> on overlap.
        /// Does not resolve collisions; use CollideResolve for that.
        /// </summary>
        public CobaltEntity Collide(string targetTag, Action<CobaltEntity> onCollide)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Collide with entities tagged as <c>targetTag</c>, resolving the collision.
        /// </summary>
        public CobaltEntity CollideResolve(string targetTag)
        {
            throw new NotImplementedException();
        }

        public CobaltEntity Move(float x, float y)
        {
            this.Set(new PositionComponent(x, y));
            return this;
        }

        public CobaltEntity MoveToKeyboard(float speed)
        {
            if (!this.Has<PositionComponent>())
            {
                this.Move(0, 0);
            }

            if (!this.Has<VelocityComponent>())
            {
                this.Set(new VelocityComponent(0, 0));
            }

            var position = this.Get<PositionComponent>();
            var velocity = this.Get<VelocityComponent>();

            this.Set(new KeyboardComponent()
                .OnPress(Microsoft.Xna.Framework.Input.Keys.W, () => velocity.Y += -speed)
                .OnPress(Microsoft.Xna.Framework.Input.Keys.S, () => velocity.Y += speed)
                .OnPress(Microsoft.Xna.Framework.Input.Keys.A, () => velocity.X += -speed)
                .OnPress(Microsoft.Xna.Framework.Input.Keys.D, () => velocity.X += speed)

                .OnRelease(Microsoft.Xna.Framework.Input.Keys.W, () => velocity.Y -= -speed)
                .OnRelease(Microsoft.Xna.Framework.Input.Keys.S, () => velocity.Y -= speed)
                .OnRelease(Microsoft.Xna.Framework.Input.Keys.A, () => velocity.X -= -speed)
                .OnRelease(Microsoft.Xna.Framework.Input.Keys.D, () => velocity.X -= speed)
            );

            return this;
        }

        public CobaltEntity Velocity(float vx, float vy)
        {
            this.Set(new VelocityComponent(vx, vy));
            return this;
        }

        public CobaltEntity Sprite(string filename)
        {
            this.Set(new SpriteComponent(filename));
            return this;
        }

        public CobaltEntity Tile(string filename, int sourceX, int sourceY, int width, int height)
        {
            this.Set(new SpriteComponent(filename, sourceX, sourceY, width, height));
            return this;
        }
    }
}

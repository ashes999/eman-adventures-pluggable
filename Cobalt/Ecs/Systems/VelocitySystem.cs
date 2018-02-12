using Cobalt.Ecs.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Systems
{
    class VelocitySystem : AbstractSystem
    {
        public VelocitySystem() :  base(new Type[] {  typeof(VelocityComponent) })
        {

        }

        override public void Update(float elapsedSeconds)
        {
            foreach (var entity in this.Entities)
            {
                var velocity = entity.Get<VelocityComponent>();
                var position = entity.Get<PositionComponent>();

                var deltaX = velocity.X * elapsedSeconds;
                var deltaY = velocity.Y * elapsedSeconds;

                position.X += deltaX;
                position.Y += deltaY;
            }            
        }
    }
}

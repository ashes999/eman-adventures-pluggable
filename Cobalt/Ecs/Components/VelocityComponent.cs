using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Components
{
    public class VelocityComponent : AbstractComponent
    {
        private Vector2 data = Vector2.Zero;

        public VelocityComponent(float vx, float vy)
        {
            data.X = vx;
            data.Y = vy;
        }

        public float X
        {
            get
            {
                return this.data.X;
            }
            set
            {
                this.data.X = value;
            }
        }

        public float Y
        {
            get
            {
                return this.data.Y;
            }
            set
            {
                this.data.Y = value;
            }
        }
    }
}

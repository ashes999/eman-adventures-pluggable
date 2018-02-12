using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Components
{
    public class PositionComponent : AbstractComponent
    {
        private Vector2 data = Vector2.Zero;

        public PositionComponent(float x, float y)
        {
            this.data = new Vector2(x, y);
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

        public Vector2 AsVector2
        {
            get
            {
                return this.data;
            }
        }
    }
}

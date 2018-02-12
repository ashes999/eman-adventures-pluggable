using Cobalt.Ecs.Components;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobalt.Ecs.Systems
{
    public interface ISystem
    {
        void Add(CobaltEntity entity);
        void Update(float elapsedSeconds);
    }
}

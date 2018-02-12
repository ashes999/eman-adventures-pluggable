using Cobalt.Ecs;
using Cobalt.Ecs.Components;
using Cobalt.Ecs.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Cobalt.Core
{
    /// <summary>
    /// A state/screen used to locally organize game-related stuff.
    /// </summary>
    public class CobaltState
    {
        public static CobaltState Current { get; private set; }

        // To keep things like collision detection consistent (we use euler integration and with
        // large updates, fast-moving objects can pierce through things), limit the maximum update
        // tick to 150ms. If there's a larger update (eg. 450ms), send it in pieces (eg. 3x 150ms).
        internal const float MaxUpdatePerTickSeconds = 0.15f;

        private List<CobaltEntity> entities = new List<CobaltEntity>();
        private IList<ISystem> systems = new List<ISystem>();
        
        public CobaltState()
        {
            CobaltState.Current = this;
            
            // For unit tests
            if (CobaltGame.Current != null)
            {
                this.systems = CobaltGame.Current.Systems;
            }
        }

        // For unit testing
        internal CobaltState(IList<ISystem> systems) : this()
        {            
            this.systems = systems;
        }

        public void Add(CobaltEntity entity)
        {
            this.entities.Add(entity);

            foreach (var system in this.systems)
            {
                system.Add(entity);
            }
        }

        public virtual void Update(float elapsedSeconds)
        {
            while (elapsedSeconds > 0)
            {
                var amount = Math.Min(MaxUpdatePerTickSeconds, elapsedSeconds);
                elapsedSeconds -= amount;
                foreach (var system in this.systems)
                {
                    system.Update(amount);
                }
            }
        }

        // TODO: move into sprite drawing system
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
             
            // TODO: sort by Z
            foreach (var entity in this.entities)
            {
                if (entity.Has<SpriteComponent>())
                {
                    var position = entity.Get<PositionComponent>();
                    var sprite = entity.Get<SpriteComponent>();
                    spriteBatch.Draw(sprite.Texture, position.AsVector2, sprite.AsRectangle, Color.White);
                }
            }

            spriteBatch.End();
        }
    }
}

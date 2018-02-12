using EmanAdventures.Core.Model.Map;
using System;

namespace EmanAdventures.Core.Model
{
    public class World
    {
        public static World Instance;
        public int Seed { get; private set; }
        public WorldMap WorldMap { get; private set; }
        
        internal readonly Random SeededRandomGenerator;
        internal const int TileSize = 48; // px

        // Used so unit tests don't end up creating two randoms at the same millisecond
        private static Random random = new Random();

        public World(int? seed = null)
        {
            World.Instance = this;

            if (seed != null && seed.HasValue)
            {
                this.Seed = seed.Value;
            }
            else
            {
                this.Seed = random.Next();
            }

            this.SeededRandomGenerator = new Random(this.Seed);

            this.GenerateWorldMap();
        }

        private void GenerateWorldMap()
        {
            this.WorldMap = new WorldMap();
        }
    }
}

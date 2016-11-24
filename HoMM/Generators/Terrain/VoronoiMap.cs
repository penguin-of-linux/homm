using System;
using System.Linq;
using System.Collections.Generic;

namespace HoMM.Generators
{
    class VoronoiMap<TLabel>
    {
        private readonly Dictionary<TLabel, SigmaIndex> labels;
        private MapSize size;

        public VoronoiMap(MapSize size, IEnumerable<TLabel> labels, Random random)
        {
            this.size = size;
            this.labels = labels
                .ToDictionary(x => x, 
                x => new SigmaIndex(random.Next(1, size.Y), random.Next(1, size.X)).AboveDiagonal(size));
        }
        
        public TLabel this[SigmaIndex index]
        {
            get
            {
                return labels
                    .Argmin(kv => kv.Value.ManhattanDistance(index.AboveDiagonal(size)))
                    .Key;
            }
        }
    }
}

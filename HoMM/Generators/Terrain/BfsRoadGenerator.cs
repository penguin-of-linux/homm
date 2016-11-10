using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Generators
{
    public class BfsRoadGenerator : RandomGenerator, ITerrainGenerator
    {
        private TileTerrain roadTile;

        public BfsRoadGenerator(TileTerrain roadTile, Random random)
            : base(random)
        {
            this.roadTile = roadTile;
        }
        
        public ISigmaMap<TileTerrain> Construct(ISigmaMap<MazeCell> maze)
        {
            var road = new HashSet<SigmaIndex>(Graph.BreadthFirstSearch(SigmaIndex.Zero, 
                    s => s.Neighborhood.Where(n => n.IsInside(maze.Size) && maze[n] == MazeCell.Empty),
                    s => s.X == maze.Size.X-1 && s.Y == maze.Size.Y-1))
                .Select(x => x.AboveDiagonal(maze.Size));
            
            return new ArraySigmaMap<TileTerrain>(maze.Size, 
                i => road.Contains(i.AboveDiagonal(maze.Size)) ? roadTile : null);
        }
    }
}

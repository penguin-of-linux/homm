using System;
using System.Linq;

namespace HoMM.Generators
{
    public class DiagonalMazeGenerator : RandomGenerator, IMazeGenerator
    {
        public DiagonalMazeGenerator(Random random) : base(random) { }

        public ISigmaMap<MazeCell> Construct(MapSize size)
        {
            return ArraySigmaMap
                .From(FixConnectivity(SymmetricallyComplete(InitAboveDiagonal(size))));
        }

        private ImmutableSigmaMap<MazeCell> SymmetricallyComplete(ImmutableSigmaMap<MazeCell> maze)
        {
            return maze.Where(s => maze[s] == MazeCell.Empty)
                .Aggregate(maze, (m, s) => m.Insert(s.DiagonalMirror(m.Size), MazeCell.Empty));
        }

        private ImmutableSigmaMap<MazeCell> InitAboveDiagonal(MapSize size)
        {
            // need a local variable here to put it into a closure
            ImmutableSigmaMap<MazeCell> maze = ArraySigmaMap.Solid(size, _ => MazeCell.Wall);

            return Graph.DepthFirstTraverse(new SigmaIndex(0, 0),

                s => s.Neighborhood
                    .Clamp(size)
                    .OrderBy(_ => random.Next()),

                s => s.Neighborhood
                    .Clamp(size)
                    .Where(x => maze[x] == MazeCell.Empty)
                    .Count() > 2

            ).Aggregate(maze, (m, r) => maze = m.Insert(r, MazeCell.Empty));
        }

        private ImmutableSigmaMap<MazeCell> FixConnectivity(ImmutableSigmaMap<MazeCell> maze)
        {
            return IsConnected(maze) ? maze : maze
                .Insert(new SigmaIndex(0, maze.Size.X), MazeCell.Empty)
                .Insert(new SigmaIndex(maze.Size.Y, 0), MazeCell.Empty)
                .Insert(new SigmaIndex(1, maze.Size.X), MazeCell.Empty)
                .Insert(new SigmaIndex(maze.Size.Y, 1), MazeCell.Empty)
                .Insert(new SigmaIndex(0, maze.Size.X - 1), MazeCell.Empty)
                .Insert(new SigmaIndex(maze.Size.Y - 1, 0), MazeCell.Empty);   
        }

        private bool IsConnected(ISigmaMap<MazeCell> maze)
        {
            return Graph.DepthFirstTraverse(new SigmaIndex(0, 0), s => s.Neighborhood
                .Where(n => n.IsInside(maze.Size))
                .Where(n => maze[n] == MazeCell.Empty)
            ).Contains(new SigmaIndex(maze.Size.Y - 1, maze.Size.X - 1));
        }
    }
}

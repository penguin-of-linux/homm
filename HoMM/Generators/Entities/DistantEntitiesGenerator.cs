using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HoMM.Generators
{
    public class EntitiesGenerator : IEntitiesGenerator
    {
        private Random random;
        private Func<Point, TileObject> factory;
        private double minDistance;

        public EntitiesGenerator(Random random, double minDistance, Func<Point, TileObject> factory)
        {
            this.random = random;
            this.factory = factory;
            this.minDistance = minDistance;
        }

        public ISigmaMap<TileObject> SpawnEntities(ISigmaMap<MazeCell> maze)
        {
            var potentialLocations = SigmaIndex.Square(maze.Size)
                .Where(s => maze[s] == MazeCell.Empty)
                .Where(s => s.Length > minDistance)
                .Where(s => s.IsAboveDiagonal(maze.Size))
                .ToArray();

            var tooFar = SigmaIndex.Square(maze.Size)
                .Where(s => s.Length <= minDistance)
                .ToArray();

            var spawnPoints = new HashSet<SigmaIndex>();

            while (potentialLocations.Length != 0)
            {
                var i = random.Next(potentialLocations.Length);

                var spawnPoint = potentialLocations[i];
                spawnPoints.Add(spawnPoint);

                potentialLocations = potentialLocations
                    .Where(s => s.EuclideanDistance(spawnPoint) > minDistance)
                    .ToArray();
            }

            return SparseSigmaMap.From(maze.Size,
                s => spawnPoints.Contains(s.AboveDiagonal(maze.Size)) ? factory(s) : null);
        }
    }
}

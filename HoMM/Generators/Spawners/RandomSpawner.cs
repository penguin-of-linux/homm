using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace HoMM.Generators
{
    public class SpawnerConfig
    {
        public SigmaIndex EmitterLocation { get; }
        public int StartRadius { get; }
        public int EndRadius { get; }
        public double SpawnDensity { get; }
        public double SpawnDistance => 1 / SpawnDensity;

        public SpawnerConfig(SigmaIndex emitter, int startInclusive, int endExclusive, double density)
        {
            if (density > 1 || density <= 0)
                throw new ArgumentException($"{nameof(density)} should be in range (0, 1]");

            EmitterLocation = emitter;
            StartRadius = startInclusive;
            EndRadius = endExclusive;
            SpawnDensity = density;
        }
    }

    public class MinDistanceSpawner : RandomSpawner
    {
        public MinDistanceSpawner(
            Random random,
            SpawnerConfig config, 
            Func<Point, TileObject> factory)
            
            : base(random, config, factory,
                  maze => SigmaIndex.Square(maze.Size)
                    .Where(s => maze[s] == MazeCell.Empty)
                    .Select(s => new { Value = s, Distance = config.EmitterLocation.ManhattanDistance(s) })
                    .Where(s => s.Distance >= config.StartRadius)
                    .Where(s => s.Distance < config.EndRadius)
                    .Select(s => s.Value)
                    .Where(s => s.IsAboveDiagonal(maze.Size)))
        { }
    }

    public class TopologicSpawner : RandomSpawner
    {
        public TopologicSpawner(
            Random random, 
            SpawnerConfig config,
            Func<Point, TileObject> factory)

            : base(random, config, factory,
                  maze => Graph.BreadthFirstTraverse(SigmaIndex.Zero, s => s.Neighborhood
                        .Clamp(maze.Size)
                        .Where(n => maze[n] == MazeCell.Empty))
                    .SkipWhile(x => x.Distance < config.StartRadius)
                    .TakeWhile(x => x.Distance < config.EndRadius)
                    .Select(x => x.Node))
        { }
    }

    public class RandomSpawner : ISpawner
    {
        private readonly Random random;
        private readonly Func<Point, TileObject> factory;
        private readonly SpawnerConfig config;

        private readonly Func<ISigmaMap<MazeCell>, IEnumerable<SigmaIndex>> getSpawnLocations;

        public RandomSpawner(Random random,
            SpawnerConfig config,
            Func<Point, TileObject> factory,
            Func<ISigmaMap<MazeCell>, IEnumerable<SigmaIndex>> spawnLocations)
        {
            this.random = random;
            this.factory = factory;
            this.config = config;
            getSpawnLocations = spawnLocations;
        }

        public ISigmaMap<TileObject> Spawn(ISigmaMap<MazeCell> maze)
        {
            var potentialLocations = getSpawnLocations(maze).ToArray();
            
            var spawnPoints = new HashSet<SigmaIndex>();

            while (potentialLocations.Length != 0)
            {
                var i = random.Next(potentialLocations.Length);

                var spawnPoint = potentialLocations[i];
                spawnPoints.Add(spawnPoint);

                potentialLocations = potentialLocations
                    .Where(s => s.EuclideanDistance(spawnPoint) >= config.SpawnDistance)
                    .ToArray();
            }

            return SparseSigmaMap.From(maze.Size,
                s => spawnPoints.Contains(s.AboveDiagonal(maze.Size)) ? factory(s) : null);
        }
    }
}

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
        public double StartRadius { get; }
        public double EndRadius { get; }
        public double SpawnDensity { get; }
        public double SpawnDistance => 1 / SpawnDensity;

        public SpawnerConfig(SigmaIndex emitter, double startInclusive, double endExclusive, double density)
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
                    .Select((x, i) => new { Distance = i, Node = x.Node })
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
                s => IsSpawnPoint(spawnPoints, s, maze.Size) ? factory(s) : null);
        }

        private bool IsSpawnPoint(HashSet<SigmaIndex> spawns, SigmaIndex index, MapSize size)
        {
            if (!IsBorderIndex(index, size))
                return spawns.Contains(index.AboveDiagonal(size));

            if (index.X >= size.X / 2.0)
                return spawns.Contains(index);

            return index.AboveDiagonal(size) == index 
                ? false
                : spawns.Contains(index.AboveDiagonal(size));
        }

        private bool IsBorderIndex(SigmaIndex index, MapSize size)
        {
            return index.AboveDiagonal(size).ManhattanDistance(SigmaIndex.Zero) > size.X - 2;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HoMM.Generators
{
    public partial class HommMapGenerator : IMapGenerator
    {
        IMazeGenerator mazeGenerator;
        ITerrainGenerator terrainGenerator;
        IEntitiesGenerator[] entitiesGenerators;

        private HommMapGenerator(
            IMazeGenerator mazeGenerator, 
            ITerrainGenerator terrainGenerator,
            params IEntitiesGenerator[] entitiesGenerators)
        {
            if (mazeGenerator == null)
                throw new InvalidOperationException("should select one IMazeGenerator");

            if (terrainGenerator == null)
                throw new InvalidOperationException("should select one ITerrainGenerator");

            this.mazeGenerator = mazeGenerator;
            this.terrainGenerator = terrainGenerator;
            this.entitiesGenerators = entitiesGenerators;
        }

        public Map GenerateMap(int size)
        {
            if (size < 0)
                throw new ArgumentException("Cannot create map of negative size");

            if (size % 2 == 1)
                throw new ArgumentException("Cannot create map of odd size");

            var mapSize = new MapSize(size, size);

            var maze = mazeGenerator.Construct(mapSize);
            var terrainMap = terrainGenerator.Construct(maze);

            var entities = entitiesGenerators
                .Aggregate(SigmaMap.Empty<TileObject>(maze.Size), 
                (m, g) => m.Merge(g.SpawnEntities(maze)));

            var tiles = SigmaIndex.Square(mapSize)
                .Select(s => new Tile(s.X, s.Y, terrainMap[s],
                    maze[s] == MazeCell.Empty ? entities[s] : new Impassable(s)));

            return new Map(size, size, tiles);
        }
    }
}

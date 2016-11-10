using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace HoMM
{
    public class Map : IEnumerable<Tile>
    {
        Tile[,] map;

        public int Height { get { return map.GetLength(0); } }
        public int Width { get { return map.GetLength(1); } }
        public Tile this[int x, int y] { get { return map[x, y]; } }
        public List<Tile> GetNeighbourTiles(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException("out of map bounds!");
            var neighbours = new List<Tile>();
            bool isEvenColumn = y % 2 == 0;
            int yUpper = isEvenColumn ? y : y - 1;
            int yLower = isEvenColumn ? y + 1 : y;

            if (y > 0)
                neighbours.Add(map[y - 1, x]);
            if (yUpper >= 0)
            {
                if (x > 0)
                    neighbours.Add(map[yUpper, x - 1]);
                if (x < Width - 1)
                    neighbours.Add(map[yUpper, x + 1]);
            }
            if (y < Height - 1)
                neighbours.Add(map[y + 1, x]);
            if (yLower < Height)
            {
                if (x > 0)
                    neighbours.Add(map[yLower, x - 1]);
                if (x < Width - 1)
                    neighbours.Add(map[yLower, x + 1]);
            }
            return neighbours;
        }

        public Map(int width, int height)
        {
            map = new Tile[height, width];
        }

        public Map(string filename)
        {
            var input = File.ReadAllLines(filename);
            var height = input.Length;
            var width = input[0].Split().Length;
            map = new Tile[height, width];

            for (int y = 0; y < height; y++)
            {
                var line = input[y].Split().Where(s => s != "").ToArray();
                for (int x = 0; x < width; x++)
                    map[y, x] = MakeTile(x, y, line[x]);
            }
        }

        public Map(int width, int height, IEnumerable<Tile> tiles) 
            : this(width, height)
        {
            foreach (var tile in tiles)
                map[tile.location.X, tile.location.Y] = tile;
        }

        public Tile MakeTile(int x, int y, string s)
        {
            TileTerrain t = InitTerrain(s);
            TileObject obj = InitObject(s, new Point(x, y));
            return new Tile(x, y, t, obj);
        }
        
        private TileTerrain InitTerrain(string s)
        {
            return TileTerrain.Parse(s[0]);
        }

        private TileObject InitObject(string s, Point location)
        {
            switch (s[1])
            {
                case '*':
                    {
                        var resName = Enum.GetNames(typeof(Resource))
                            .SingleOrDefault(res => res[0] == s[2]);
                        var resource = (Resource)Enum.Parse(typeof(Resource), resName == null ? "Rubles" : resName);
                        return new Mine(resource, location);
                    }
                case 'p':
                    {
                        var resName = Enum.GetNames(typeof(Resource))
                            .SingleOrDefault(res => res[0] == s[2]);
                        var resource = (Resource)Enum.Parse(typeof(Resource), resName == null ? "Rubles" : resName);
                        int amount = int.Parse(s.Substring(3));
                        return new ResourcePile(resource, amount, location);
                    }
                case 'M':
                    {
                        return CreateNeutralArmyFromString(s, location);
                    }
                case '-':
                    return null;
                default:
                    throw new ArgumentException("Unknown object!");
            }
        }

        private NeutralArmy CreateNeutralArmyFromString(string s, Point location)
        {
            var monsterTypeName = Enum.GetNames(typeof(UnitType))
                .SingleOrDefault(res => res[0] == s[2]);
            var unitType = (UnitType)Enum.Parse(typeof(UnitType), monsterTypeName);
            int amount = int.Parse(s.Substring(3).Split('.')[0]);
            Mine guardedMine = null;
            if (s.Substring(3).Split('.').Count() > 1)
            {
                var coords = s.Substring(3).Split('.')[1];
                var x = int.Parse(coords.Take(2).ToString());
                var y = int.Parse(coords.Substring(2));
                if (!(map[x, y].tileObject is Mine))
                    throw new ArgumentException("Coords do not");
                guardedMine = (Mine)map[x, y].tileObject;
            }
            return new NeutralArmy(UnitFactory.CreateFromUnitType(unitType), amount, guardedMine, location);
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            foreach (var tile in map)
                yield return tile;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

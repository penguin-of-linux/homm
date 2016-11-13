using System.Drawing;

namespace HoMM
{
    public class Tile
    {
        public TileObject tileObject;
        public TileTerrain tileTerrain;
        public readonly Point location;

        public Tile(Point location, TileTerrain t, TileObject obj)
        {
            this.location = location;
            tileTerrain = t;
            tileObject = obj;
        }

        public Tile(int x, int y, TileTerrain t, TileObject obj) : this(new Point(x, y), t, obj)
        {
        }
    }
}

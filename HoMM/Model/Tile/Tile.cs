namespace HoMM
{
    public class Tile
    {
        public TileObject tileObject;
        public TileTerrain tileTerrain;
        public readonly Vector2i location;

        public Tile(Vector2i location, TileTerrain t, TileObject obj)
        {
            this.location = location;
            tileTerrain = t;
            tileObject = obj;
        }

        public Tile(int x, int y, TileTerrain t, TileObject obj) : this(new Vector2i(x, y), t, obj)
        {
        }
    }
}

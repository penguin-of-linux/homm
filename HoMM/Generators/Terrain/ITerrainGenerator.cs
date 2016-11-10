namespace HoMM.Generators
{
    public interface ITerrainGenerator
    {
        ISigmaMap<TileTerrain> Construct(ISigmaMap<MazeCell> maze);
    }
}

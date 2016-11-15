using System;

namespace HoMM.Generators
{
    class ArraySigmaMap<TCell> : ImmutableSigmaMap<TCell>
    {
        private TCell[,] cells;

        public override TCell this[SigmaIndex index]
        {
            get { return cells[index.Y, index.X]; }
        }

        public override MapSize Size {
            get { return new MapSize(cells.GetLength(0), cells.GetLength(1)); }
        }

        public ArraySigmaMap(MapSize size, Func<SigmaIndex, TCell> cellsFactory)
        {
            cells = new TCell[size.Y, size.X];

            foreach (var index in SigmaIndex.Square(size))
                cells[index.Y, index.X] = cellsFactory(index);
        }
    }

    static class ArraySigmaMap
    {
        public static ArraySigmaMap<TCell> Solid<TCell>(MapSize size, Func<SigmaIndex, TCell> cellsFactory)
        {
            return new ArraySigmaMap<TCell>(size, cellsFactory);
        }

        public static ArraySigmaMap<TCell> From<TCell>(ISigmaMap<TCell> source)
        {
            return Solid(source.Size, index => source[index]);
        }
    }
}

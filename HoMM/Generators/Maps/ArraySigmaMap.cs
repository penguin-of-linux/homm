using System;

namespace HoMM.Generators
{
    public class ArraySigmaMap<TCell> : ImmutableSigmaMap<TCell>
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

        public static ArraySigmaMap<TCell> Solid(MapSize size, Func<SigmaIndex, TCell> cellsFactory)
        {
            return new ArraySigmaMap<TCell>(size, cellsFactory);
        }

        public static ArraySigmaMap<TCell> From(ISigmaMap<TCell> source)
        {
            var destination = Solid(source.Size, _ => default(TCell));

            foreach (var index in source)
                destination.cells[index.Y, index.X] = source[index];

            return destination;
        }
    }
}

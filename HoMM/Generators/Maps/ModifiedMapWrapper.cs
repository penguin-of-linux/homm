namespace HoMM.Generators
{
    sealed class ModifiedMapWrapper<TCell> : ImmutableSigmaMap<TCell>
    {
        public ISigmaMap<TCell> ParentMaze { get; private set; }
        public SigmaIndex ModifiedLocation { get; private set; }
        public TCell ModifiedCell { get; private set; }

        public override MapSize Size { get { return ParentMaze.Size; } }

        public override TCell this[SigmaIndex index]
        {
            get { return ModifiedLocation.Equals(index) ? ModifiedCell : ParentMaze[index]; }
        }

        public ModifiedMapWrapper(ISigmaMap<TCell> parent, SigmaIndex modLocation, TCell modCell)
        {
            ParentMaze = parent;
            ModifiedLocation = modLocation;
            ModifiedCell = modCell;
        } 
    }
}

using System.Collections.Generic;

namespace HoMM.Generators
{
    public interface ISigmaMap<TCell> : IEnumerable<SigmaIndex>
    {
        MapSize Size { get; }
        TCell this[SigmaIndex location] { get; }
    }
}

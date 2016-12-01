using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;
using HoMM.Generators;
using HoMM.HommEngine;

namespace HoMM.World
{
    sealed class HommWorld : World<HommWorldState>
    {
        public IHommEngine HommEngine { get; private set; }
        public ICommonEngine CommonEngine { get; private set; }
        public Map Map { get; private set; }
        public Random Random { get; private set; }

        public override void CreateWorld()
        {
            Random = new Random(WorldState.Seed);
            HommEngine = GetEngine<IHommEngine>();
            CommonEngine = GetEngine<ICommonEngine>();
            Map = MapHelper.CreateMap(Random);
        }
    }
}

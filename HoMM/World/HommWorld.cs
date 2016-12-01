using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CVARC.V2;

namespace HoMM.World
{
    sealed class HommWorld : World<HommWorldState>
    {
        public IHommEngine HommEngine { get; private set; }
        public ICommonEngine CommonEngine { get; private set; }

        public override void CreateWorld()
        {
            HommEngine = GetEngine<IHommEngine>();
            CommonEngine = GetEngine<ICommonEngine>();

            throw new NotImplementedException();
        }
    }
}

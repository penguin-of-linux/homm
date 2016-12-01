using CVARC.V2;
using HoMM.Units.HexagonalMovementUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace HoMM.Commands
{
    [Serializable]
    [DataContract]
    class HommCommand : ICommand, IHexMovCommand
    {
        [DataMember]
        public HexMovement Movement { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HoMM.Units.HexagonalMovementUnit
{
    [Serializable]
    abstract class HexMovement
    {
        public abstract Vector2i Turn(Vector2i location);
    }


    [Serializable]
    class Wait : HexMovement
    {
        public override Vector2i Turn(Vector2i location) => location;
    }

    [Serializable]
    class Movement : HexMovement
    {
        public Direction Direction { get; }

        public Movement(Direction direction)
        {
            Direction = direction;
        }

        public override Vector2i Turn(Vector2i location)
        {
            var isEven = location.Y % 2 == 0;

            switch (Direction)
            {
                case Direction.Up:
                    return new Vector2i(location.X, location.Y - 1);
                case Direction.Down:
                    return new Vector2i(location.X, location.Y + 1);
                case Direction.LeftUp:
                    return new Vector2i(location.X - 1, isEven ? location.Y - 1 : location.Y);
                case Direction.LeftDown:
                    return new Vector2i(location.X - 1, isEven ? location.Y : location.Y + 1);
                case Direction.RightUp:
                    return new Vector2i(location.X + 1, isEven ? location.Y - 1 : location.Y);
                case Direction.RightDown:
                    return new Vector2i(location.X + 1, isEven ? location.Y : location.Y + 1);
            }

            throw new ArgumentException($"{nameof(Direction)} contains invalid value!");
        }
    }

}

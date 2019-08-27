using HexGen;

namespace ExtensionMethods
{
    public static class HexDirectionExtenstions
    {
        public static HexDirection GetOpposite(this HexDirection direction)
        {
            if ((int)direction < 3)
                return direction + 3;
            else
                return direction - 3;
        }
    }
}

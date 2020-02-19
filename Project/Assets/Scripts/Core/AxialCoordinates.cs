
namespace HexGen
{
    public struct AxialCoordinates
    {
        public int q;
        public int r;

        public AxialCoordinates(int q, int r)
        {
            this.q = q;
            this.r = r;
        }

        public override string ToString()
        {
            return $"{q} {r}";
        }
    }
}

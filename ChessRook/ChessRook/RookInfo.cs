
namespace ChessRook
{
    public class RookInfo
    {
        private int _fullHeight;
        private int _upperBaseHeight;
        private int _lowerBaseHeight;
        private int _upperBaseDiameter;
        private int _lowerBaseDiameter;

        public int FullHeight { get { return _fullHeight; } set { _fullHeight = value; } }
        public int UpperBaseHeight { get { return _upperBaseHeight; } set { _upperBaseHeight = value; } }
        public int LowerBaseHeight { get { return _lowerBaseHeight; } set { _lowerBaseHeight = value; } }
        public int UpperBaseDiameter { get { return _upperBaseDiameter; } set { _upperBaseDiameter = value; } }
        public int LowerBaseDiameter { get { return _lowerBaseDiameter; } set { _lowerBaseDiameter = value; } }

        public RookInfo() { }
    }
}

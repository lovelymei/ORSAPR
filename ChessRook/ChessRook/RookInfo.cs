
namespace ChessRook
{
    public class RookInfo
    {
        private int _fullHeight;
        private int _upperBaseHeight;
        private int _lowerBaseHeight;
        private int _upperBaseDiameter;
        private int _lowerBaseDiameter;

        public int FullHeight { 
            get 
            { 
                return _fullHeight;
            } 
            set 
            { 
                if ((value < 10000) && (value > 10))
                {
                    _fullHeight = value;
                }
                else
                {
                   
                }
            } 
        }

        public int UpperBaseHeight { 
            get
            { 
                return _upperBaseHeight;
            } 
            set 
            {
                if ((value < 150) && (value > 2))
                {
                    _upperBaseHeight = value;
                }
            } 
        }
        public int LowerBaseHeight { 
            get 
            { 
                return _lowerBaseHeight; 
            } 
            set 
            {
                if ((value < 100) && (value > 3))
                {
                    _lowerBaseHeight = value;
                }
            } 
        }

        public int UpperBaseDiameter 
        {
            get 
            { 
                return _upperBaseDiameter; 
            } 
            set 
            {
                if ((value < 100) && (value > 3))
                {
                    _upperBaseDiameter = value;
                }
            } 
        }

        public int LowerBaseDiameter 
        { 
            get 
            { 
                return _lowerBaseDiameter; 
            } 
            set 
            {
                if ((value < 500) && (value > 5))
                {
                    _lowerBaseDiameter = value;
                }
            } 
        }

        public RookInfo() { }
    }
}

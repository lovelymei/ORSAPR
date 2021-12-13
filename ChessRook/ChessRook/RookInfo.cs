
namespace ChessRook
{
    public class RookInfo
    {
        private int _fullHeight;
        private int _upperBaseHeight;
        private int _lowerBaseHeight;
        private int _upperBaseDiameter;
        private int _lowerBaseDiameter;

        /// <summary>
        /// свойство параметра "Полная высота фигуры"
        /// </summary>
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

        /// <summary>
        /// свойство параметра "высота верхнего основания"
        /// </summary>
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

        /// <summary>
        /// свойство параметра "высота нижнего основания"
        /// </summary>
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

        /// <summary>
        /// свойство параметра "диаметр верхнего основания"
        /// </summary>
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

        /// <summary>
        /// свойство параметра "диаметр нижнего основания"
        /// </summary>
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

        /// <summary>
        /// Конструктор
        /// </summary>
        public RookInfo() { }
    }
}

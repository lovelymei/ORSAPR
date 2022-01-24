
namespace Rook
{
    /// <summary>
    /// Данные для построения ладьи
    /// </summary>
    public class RookInfo
    {
        #region DiapasonConst
        /// <summary>
        /// минимальная полная высота ладьи
        /// </summary>
        public readonly int FULL_HEIGHT_MIN = 15;

        /// <summary>
        /// максимальная полная высота ладьи
        /// </summary>
        public readonly int FULL_HEIGHT_MAX = 600;

        /// <summary>
        /// минимальная высота верхнего основания
        /// </summary>
        public readonly int UPPER_BASE_HEIGHT_MIN = 4;

        /// <summary>
        /// максимальная высота верхнего основания
        /// </summary>
        public readonly int UPPER_BASE_HEIGHT_MAX = 90;

        /// <summary>
        /// минимальная высота нижнего основания
        /// </summary>
        public readonly int LOWER_BASE_HEIGHT_MIN = 5;

        /// <summary>
        /// максимальная высота нижнего основания
        /// </summary>
        public readonly int LOWER_BASE_HEIGHT_MAX = 100;

        /// <summary>
        /// минимальный диаметр нижнего основания
        /// </summary>
        public readonly int LOWER_BASE_DIAMETER_MIN = 5;

        /// <summary>
        /// максимальный диаметр нижнего основания
        /// </summary>
        public readonly int LOWER_BASE_DIAMETER_MAX = 500;

        /// <summary>
        /// минимальный диаметр верхнего основания
        /// </summary>
        public readonly int UPPER_BASE_DIAMETER_MIN = 3;

        /// <summary>
        /// максимальный диаметр верхнего основания
        /// </summary>
        public readonly int UPPER_BASE_DIAMETER_MAX = 100;

        #endregion

        /// <summary>
        /// полная высота фигуры
        /// </summary>
        private int _fullHeight;

        /// <summary>
        /// высота верхнего основания
        /// </summary>
        private int _upperBaseHeight;

        /// <summary>
        /// высота нижнего основания
        /// </summary>
        private int _lowerBaseHeight;

        /// <summary>
        /// диаметр верхнего основания
        /// </summary>
        private int _upperBaseDiameter;

        /// <summary>
        /// диаметр нижнего основания 
        /// </summary>
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
                if (Validation(value, FULL_HEIGHT_MIN, FULL_HEIGHT_MAX))
                {
                    _fullHeight = value;
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
                if (Validation(value, UPPER_BASE_HEIGHT_MIN, UPPER_BASE_HEIGHT_MAX))
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
                if (Validation(value, LOWER_BASE_HEIGHT_MIN, LOWER_BASE_HEIGHT_MAX))
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
                if (Validation(value,UPPER_BASE_DIAMETER_MIN,UPPER_BASE_DIAMETER_MAX))
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
                if (Validation(value, LOWER_BASE_DIAMETER_MIN, LOWER_BASE_DIAMETER_MAX))
                {
                    _lowerBaseDiameter = value;
                }
            } 
        }

        /// <summary>
        /// Скруглять ли грани ладьи
        /// </summary>
        public bool HasFillet { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public RookInfo() { }

        /// <summary>
        /// Валидация на то, входит ли значение в заданный диапазон
        /// </summary>
        /// <param name="value"> Проверяемое значение </param>
        /// <param name="min"> Минимальное значение </param>
        /// <param name="max"> Максимальное значение </param>
        /// <returns></returns>
        public bool Validation(int value, int min, int max)
        {
            return (value >= min) && (value <= max);
        }
    }
}

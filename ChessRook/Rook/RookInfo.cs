
namespace Rook
{
    //TODO:
    /// <summary>
    /// Данные для построения ладьи
    /// </summary>
    public class RookInfo
    {
        //TODO: XML
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
                if (Validation(value, 10, 10000))
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
                if (Validation(value, 2, 150))
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
                //TODO: пересмотреть диапазоны
                if (Validation(value, 2, 150))
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
                if (Validation(value,3,100))
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
                if (Validation(value, 5, 500))
                {
                    _lowerBaseDiameter = value;
                }
            } 
        }

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
            if ((value >= min) && (value <= max))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

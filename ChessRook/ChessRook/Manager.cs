
using Kompas6API5;

namespace ChessRook
{
    public class Manager
    {
        /// <summary>
        /// Объект ModelCreator
        /// </summary>
        private ModelCreator _createModel;

        public Manager() { }

        /// <summary>
        /// Инициализация компонента (и создание 2д-эскиза)
        /// </summary>
        /// <param name="rookInfo"> Данные ладьи </param>
        public void InitializeComponent(RookInfo rookInfo)
        {
            _createModel = new ModelCreator(rookInfo);
            _createModel.CreateSketch(rookInfo);

        }
        /// <summary>
        /// Валидация на то, входит ли значение в заданный диапазон
        /// </summary>
        /// <param name="value"> Проверяемое значение </param>
        /// <param name="min"> Минимальное значение </param>
        /// <param name="max"> Максимальное значение </param>
        /// <returns></returns>
        public bool Validation(int value,int min,int max)
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

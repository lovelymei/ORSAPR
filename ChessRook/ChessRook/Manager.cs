using Rook;

namespace KompasApi
{
        //TODO: RSDN
    /// <summary>
    /// 
    /// </summary>
    public class Manager
    {
        /// <summary>
        /// Объект ModelCreator
        /// </summary>
        private ModelCreator _createModel;

        /// <summary>
        /// конструктор
        /// </summary>
        public Manager() { }

        /// <summary>
        /// Инициализация компонента (и создание 2д-эскиза)
        /// </summary>
        /// <param name="rookInfo"> Данные ладьи </param>
        public void InitializeComponent(RookInfo rookInfo)
        {
            _createModel = new ModelCreator(rookInfo);
            _createModel.CreateRook();
        }
    }
}

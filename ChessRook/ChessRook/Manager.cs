
using Kompas6API5;

namespace ChessRook
{
    public class Manager
    {
        private ModelCreator _createModel;

        public Manager() { }

        public void InitializeComponent(RookInfo rookInfo)
        {
            _createModel = new ModelCreator(rookInfo);
            _createModel.CreateSketch(rookInfo);

        }

        public void Validation(RookInfo rookInfo)
        {

        }
    }
}

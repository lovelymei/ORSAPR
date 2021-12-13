
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

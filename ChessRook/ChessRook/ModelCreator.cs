using Kompas6API5;
using Kompas6Constants3D;

namespace ChessRook
{
    class ModelCreator
    {
        private RookInfo _rookInfo;
        private ksDocument3D _document3D;
        private ksDocument2D _document2D;
        private ksPart _part;
        private ksEntity _plane;
        private ksEntity _sketch;
        private ksSketchDefinition _sketchDefinition;
        private KompasConnector _kompas;
        private Point _point;

        public ModelCreator(RookInfo rookInfo)
        {
            _rookInfo = new RookInfo
            {
                FullHeight = rookInfo.FullHeight,
                LowerBaseDiameter = rookInfo.LowerBaseDiameter,
                LowerBaseHeight = rookInfo.LowerBaseHeight,
                UpperBaseDiameter = rookInfo.UpperBaseDiameter,
                UpperBaseHeight = rookInfo.UpperBaseHeight
            };
            _point = new Point();
        }

        private void CreateDocument()
        {

            _document3D = _kompas.KompasObject.Document3D();
            _document3D.Create(false, true);
            _document2D = _kompas.KompasObject.Document2D();
            _part = (ksPart)_document3D.GetPart((int)Part_Type.pTop_Part);
        }

        private void DrawLine(int x, int y)
        {
            _document2D.ksLineSeg(_point.x, _point.y, _point.x + x, _point.y+y, 1);
            _point.x += x;
            _point.y += y;
        }
        
        public void CreateSketch(RookInfo rook)
        {
            _kompas = new KompasConnector();
            CreateDocument();

            _plane = _part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            _sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = _sketch.GetDefinition();
            _sketchDefinition.SetPlane(_plane);

            _sketch.Create();

            _document2D = _sketchDefinition.BeginEdit();
            //самая нижняя горизонтальная линия
            DrawLine(_rookInfo.LowerBaseDiameter / 2, 0);
            DrawLine(0, _rookInfo.LowerBaseHeight);
            DrawLine(-_rookInfo.LowerBaseDiameter / 10, 0);
            
            var nextPoint = new Point()
            {
                x = 2 * _rookInfo.UpperBaseDiameter / 5,
                y = _rookInfo.FullHeight - 2 * _rookInfo.UpperBaseHeight,
            };
            var changePoint = new Point()
            {
                x = nextPoint.x - _point.x,
                y = nextPoint.y - _point.y
            };

            DrawLine(changePoint.x, changePoint.y);
            DrawLine(_rookInfo.UpperBaseDiameter/10, 0);
            DrawLine(0, _rookInfo.UpperBaseHeight);
            DrawLine(-_point.x, 0);
        }

    }
}

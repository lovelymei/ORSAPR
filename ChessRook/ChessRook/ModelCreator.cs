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
        }

        private void CreateDocument()
        {
            
            _document3D = _kompas.KompasObject.Document3D();
            _document3D.Create(false, true);
            _document2D = _kompas.KompasObject.Document2D();
            _part = (ksPart)_document3D.GetPart((int)Part_Type.pTop_Part); 
        }

        public void CreateSketch(RookInfo rook)
        {
            _kompas = new KompasConnector();
            CreateDocument();
            //плоскость XOZ
            _plane = _part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            _sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = _sketch.GetDefinition();
            _sketchDefinition.SetPlane(_plane);

            _sketch.Create();

            _document2D = _sketchDefinition.BeginEdit();

            _document2D.ksLineSeg(0, 0, _rookInfo.LowerBaseDiameter / 2, 0, 1);
            //высота нижнего основания
            _document2D.ksLineSeg(
                _rookInfo.LowerBaseDiameter / 2, 
                0, 
                _rookInfo.LowerBaseDiameter / 2, 
                _rookInfo.LowerBaseHeight, 
                1);

            //отступ снизу
            _document2D.ksLineSeg(
                _rookInfo.LowerBaseDiameter / 2,
                _rookInfo.LowerBaseHeight,
                2 * _rookInfo.LowerBaseDiameter / 5,
                _rookInfo.LowerBaseHeight,
                1
                );

            //отступ сверху
            _document2D.ksLineSeg(
                _rookInfo.UpperBaseDiameter / 2,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight * 2,
                2 * _rookInfo.UpperBaseDiameter / 5,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight * 2,
                1
                );
            //верхнее основание
            _document2D.ksLineSeg(
                0,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight,
                _rookInfo.UpperBaseDiameter / 2,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight,
                1);

            //высота верхнего основания
            _document2D.ksLineSeg(
                _rookInfo.UpperBaseDiameter / 2,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight, 
                _rookInfo.UpperBaseDiameter / 2,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight*2, 
                1);

            //дуга  через три точки
            _document2D.ksArcBy3Points(
                2 * _rookInfo.UpperBaseDiameter / 5,
                _rookInfo.FullHeight - _rookInfo.UpperBaseHeight * 2,
                (2 * _rookInfo.LowerBaseDiameter / 5 - 2 * _rookInfo.UpperBaseDiameter / 5)/2 + 2 * _rookInfo.LowerBaseDiameter / 5,
                (_rookInfo.FullHeight - _rookInfo.UpperBaseHeight * 2 - _rookInfo.LowerBaseHeight)/2 + _rookInfo.LowerBaseHeight,
                2 * _rookInfo.LowerBaseDiameter / 5,
                _rookInfo.LowerBaseHeight,
                1
                );
        }


    }
}

using Kompas6API5;
using Kompas6Constants3D;

namespace ChessRook
{
    class ModelCreator
    {
        private RookInfo _rookInfo;

        public ksDocument3D Document3D;
        private ksDocument2D _document2D;
        private ksPart _part;
        private ksEntity _plane;
        private ksEntity _sketch;
        private ksSketchDefinition _sketchDefinition;

        ModelCreator(RookInfo rookInfo)
        {
            _rookInfo.FullHeight = rookInfo.FullHeight;
            _rookInfo.LowerBaseDiameter = rookInfo.LowerBaseDiameter;
            _rookInfo.LowerBaseHeight = rookInfo.LowerBaseHeight;
            _rookInfo.UpperBaseDiameter = rookInfo.UpperBaseDiameter;
            _rookInfo.UpperBaseHeight = rookInfo.UpperBaseHeight;
        }

        public void CreateSketch(RookInfo rook)
        {
            //плоскость XOZ
            _plane = _part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            _sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            _sketchDefinition = _sketch.GetDefinition();
            _sketchDefinition.SetPlane(_plane);

            _sketch.Create();

            _document2D = _sketchDefinition.BeginEdit();

            //провести дугу (x4,y4) - (x2,y1)
            //x4 - 2 * _rookInfo.UpperBaseDiameter / 5
            //y4 - _rookInfo.FullHeight - _rookInfo.UpperBaseHeight * 2
            //x2 - 2 * _rookInfo.LowerBaseDiameter / 5
            //y1 - _rookInfo.LowerBaseHeight
            //нижнее основание (x1,y1,x2,y2)
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

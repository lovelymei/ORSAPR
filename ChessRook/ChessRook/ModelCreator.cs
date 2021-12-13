﻿using Kompas6API5;
using Kompas6Constants3D;

namespace ChessRook
{
        //TODO: RSDN
    class ModelCreator
    {
        //TODO: Опустить поля в методы
        /// <summary>
        /// данные ладьи
        /// </summary>
        private RookInfo _rookInfo;

        /// <summary>
        /// 3д-документ
        /// </summary>
        private ksDocument3D _document3D;

        /// <summary>
        /// 2д-документ
        /// </summary>
        private ksDocument2D _document2D;

        /// <summary>
        /// Интерфейс 
        /// </summary>
        private ksPart _part;

        /// <summary>
        /// Текущая плоскость
        /// </summary>
        private ksEntity _plane;

        /// <summary>
        /// Текущий эскиз
        /// </summary>
        private ksEntity _sketch;

        /// <summary>
        /// 
        /// </summary>
        private ksSketchDefinition _sketchDefinition;

        /// <summary>
        /// Объект KompasConnector
        /// </summary>
        private KompasConnector _kompas;

        /// <summary>
        /// Объект Point
        /// </summary>
        private Point _point;

        //TODO:
        private ksEntity _rotatedEntity;

        private ksRotatedParam _rotatedParam;

        private ksBaseRotatedDefinition _rotateDefinition;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="rookInfo"> Данные ладьи </param>
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

        /// <summary>
        /// Создание документа
        /// </summary>
        private void CreateDocument()
        {
            _document3D = _kompas.KompasObject.Document3D();
            _document3D.Create(false, true);
            _document2D = _kompas.KompasObject.Document2D();
            _part = (ksPart)_document3D.GetPart((int)Part_Type.pTop_Part);
        }

        /// <summary>
        /// Отрисовка линии 
        /// </summary>
        /// <param name="x">Координата x</param>
        /// <param name="y">Координата y</param>
        private void DrawLine(int x, int y)
        {
            _document2D.ksLineSeg(_point.X, _point.Y, _point.X + x, _point.Y+y, 1);
            _point.X += x;
            _point.Y += y;
        }
        
        /// <summary>
        /// Создание эскиза
        /// </summary>
        /// <param name="rook"></param>
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
                X = 2 * _rookInfo.UpperBaseDiameter / 5,
                Y = _rookInfo.FullHeight - 2 * _rookInfo.UpperBaseHeight,
            };
            var changePoint = new Point()
            {
                X = nextPoint.X - _point.X,
                Y = nextPoint.Y - _point.Y
            };

            DrawLine(changePoint.X, changePoint.Y);
            DrawLine(_rookInfo.UpperBaseDiameter/10, 0);
            DrawLine(0, _rookInfo.UpperBaseHeight);
            DrawLine(-_point.X, 0);

            /*
            //ось вращения 
            _document2D.ksLineSeg(0, -20, 0, 20, 3);
            //вращение фигуры - вокруг оси Y 
            _rotatedEntity = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseRotated);
            //интерфейс базовой операции вращения
            _rotateDefinition = (ksBaseRotatedDefinition)_rotatedEntity.GetDefinition();

            _rotatedParam = (RotatedParam)_rotateDefinition.RotatedParam();

            //направление вращения - в обе стороны
            _rotatedParam.direction = (int)Direction_Type.dtBoth;
            _rotatedParam.toroidShape = false;

            _rotateDefinition.SetThinParam(true, (int)Direction_Type.dtBoth, 1, 1);//тонкая стенка в обе стороны
            _rotateDefinition.SetSideParam(true, 180);
            _rotateDefinition.SetSketch(_sketch);
            _rotatedEntity.Create();
            */
        }
    }
}

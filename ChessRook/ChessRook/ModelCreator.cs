using Kompas6API5;
using Kompas6Constants3D;
using Rook;

namespace ChessRook
{
        //TODO: RSDN
    /// <summary>
    /// Создание модели ладьи 
    /// </summary>
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
        /// Объект KompasConnector
        /// </summary>
        private KompasConnector _kompas;

        /// <summary>
        /// Объект Point
        /// </summary>
        private Point _point;

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
            ksEntity plane;
            ksEntity sketch;
            ksSketchDefinition sketchDefinition;
            
            _kompas = new KompasConnector();
             CreateDocument();

            plane = _part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            sketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            sketchDefinition = sketch.GetDefinition();
            sketchDefinition.SetPlane(plane);

            sketch.Create();

            _document2D = sketchDefinition.BeginEdit();

            DrawLine(_rookInfo.UpperBaseDiameter/2,0);
            DrawLine(0,_rookInfo.UpperBaseHeight);
            DrawLine(-_rookInfo.UpperBaseDiameter / 10, 0);
            //диагонль
            var nextPoint = new Point()
            {
                X = 2 * _rookInfo.LowerBaseDiameter / 5,
                Y = _rookInfo.FullHeight -_rookInfo.LowerBaseHeight - _rookInfo.UpperBaseHeight,
            };
            var changePoint = new Point()
            {
                X = nextPoint.X - _point.X,
                Y = nextPoint.Y - _point.Y
            };

            DrawLine(changePoint.X, changePoint.Y);
            DrawLine(_rookInfo.LowerBaseDiameter / 10, 0);
            DrawLine(0, _rookInfo.LowerBaseHeight);
            DrawLine(-_rookInfo.LowerBaseDiameter/2,0);

            //ось вращения, 3 - тип линии
            _document2D.ksLineSeg(0, 0,0, _rookInfo.FullHeight, 3);
            sketchDefinition.EndEdit();
            
            Rotate(sketch);
            CreateBattlement();
            Extrude(sketch);
             
            _document3D.drawMode = (int)ViewMode.vm_Shaded;
            _document3D.shadedWireframe = true;
        }

        /// <summary>
        /// Создание эскиза верхнего элемента
        /// </summary>
        private void CreateBattlement()
        {
            //новый скетч для зубчиков ладьи 
            ksEntity battlePlane;
            ksEntity battleSketch;
            ksSketchDefinition battleSketchDefinition;

            battlePlane = _part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            battleSketch = _part.NewEntity((short)Obj3dType.o3d_sketch);
            battleSketchDefinition = battleSketch.GetDefinition();
            battleSketchDefinition.SetPlane(battlePlane);

            battleSketch.Create();

            _document2D = battleSketchDefinition.BeginEdit();

            //центр окружности, на которой будут отрисовываться зубчики
            var center = new Point();

            _document2D.ksCircle(center.X, center.X, _rookInfo.UpperBaseDiameter / 2, 1);
            _document2D.ksCircle(center.X, center.X, _rookInfo.UpperBaseDiameter / 2.2, 1);

            battleSketchDefinition.EndEdit();
            Extrude(battleSketch);

        }

        /// <summary>
        /// Выдавливание
        /// </summary>
        /// <param name="sketch"></param>
        private void Extrude(ksEntity sketch)
        {
            int depth = _rookInfo.UpperBaseDiameter / 10;
            var entityExtrude = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_bossExtrusion);
            // интерфейс базовой операции выдавливания
            var entityExtrudeDefinition = (ksBossExtrusionDefinition)entityExtrude.GetDefinition();
            // интерфейс структуры параметров выдавливания
            ksExtrusionParam extrudeParameters = (ksExtrusionParam)entityExtrudeDefinition.ExtrusionParam();
            extrudeParameters.direction = (short)Direction_Type.dtNormal;
            // интерфейс структуры параметров тонкой стенки
            ksThinParam thinParam = (ksThinParam)entityExtrudeDefinition.ThinParam();

            entityExtrudeDefinition.SetSketch(sketch);
            // тип выдавливания (строго на глубину)
            extrudeParameters.typeNormal = (short)End_Type.etBlind;
            // глубина выдавливания
            extrudeParameters.depthNormal = -depth;
            // тонкая стенка в два направления
            thinParam.thin = false;
            //Толщина стенки в обратном направлении
            thinParam.reverseThickness = 0;

            //Направление формирования тонкой стенки
            thinParam.thinType = (short)Direction_Type.dtReverse;

            entityExtrude.Create();
        }

        /// <summary>
        /// Выдавливание вращением
        /// </summary>
        /// <param name="sketch"></param>
        public void Rotate(ksEntity sketch)
        {
            ksEntity rotatedEntity;
            ksBaseRotatedDefinition rotateDefinition;

            //интерфейс объекта "операция выдавливания вращением"
            rotatedEntity = (ksEntity)_part.NewEntity((short)Obj3dType.o3d_baseRotated);

            //интерфейс параметров операции  "выдавливание вращением"
            rotateDefinition = (ksBaseRotatedDefinition)rotatedEntity.GetDefinition();
            rotateDefinition.directionType = (short)Direction_Type.dtBoth;
            rotateDefinition.SetSideParam(true, 360);
            rotateDefinition.SetSketch(sketch);
            rotatedEntity.Create();
        }
    }
}

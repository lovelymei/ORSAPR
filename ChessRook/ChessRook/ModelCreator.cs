using Kompas6API5;
using Kompas6Constants3D;
using Rook;

namespace KompasApi
{
    /// <summary>
    /// Создание модели ладьи 
    /// </summary>
    public class ModelCreator
    {
        /// <summary>
        /// Объект KompasConnector
        /// </summary>
        private KompasConnector _kompas;

        /// <summary>
        /// Объект Point
        /// </summary>
        private Point _point;

        //TODO: Несоответствие XML-комментария сигнатуре метода 
        /// <summary>
        /// Конструктор
        /// </summary>
        public ModelCreator() { }

        /// <summary>
        /// Создание документа
        /// </summary>
        private void CreateDocument()
        {
            if (_kompas == null)
            {
                _kompas = new KompasConnector();
            }
            else
            {
                _kompas.OpenKompas3D();
            }
        }

        /// <summary>
        /// Отрисовка линии 
        /// </summary>
        /// <param name="x">Координата x</param>
        /// <param name="y">Координата y</param>
        private void DrawLine(int x, int y)
        {
            _kompas.Document2D.ksLineSeg(_point.X, _point.Y, _point.X + x, _point.Y+y, 1);
            _point.X += x;
            _point.Y += y;
        }
        
        //TODO: Несоответствие XML-комментария сигнатуре метода 
        /// <summary>
        /// Создание модели ладьи
        /// </summary>
        /// <param name="rookInfo"> Объект класса RookInfo</param>
        public void CreateRook(RookInfo rookInfo)
        {
            CreateDocument();
            _point = new Point();
            CreateBase(rookInfo);
            CreateBattlement(rookInfo.UpperBaseHeight,rookInfo.UpperBaseDiameter);
            if (rookInfo.HasFillet)
            {
                //фаска на нижнем основании
                var x = 0;
                var y = (rookInfo.FullHeight - rookInfo.UpperBaseHeight - rookInfo.LowerBaseHeight);
                var z = rookInfo.LowerBaseDiameter / 2;
                CreateFillet(1, x, y, z);
                //на верхнем основании
                y = rookInfo.UpperBaseHeight;
                z = rookInfo.UpperBaseDiameter / 2;
                CreateFillet(1, x, y, z);
            }
            _kompas.Document3D.drawMode = (int)ViewMode.vm_Shaded;
            _kompas.Document3D.shadedWireframe = true;
        }

        /// <summary>
        /// Создание основной части ладьи
        /// </summary>
        private void CreateBase(RookInfo rookInfo)
        {
            //TODO: RSDN
            ksEntity plane;
            ksSketchDefinition sketchDefinition;

            plane = _kompas.Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            ksEntity sketch = _kompas.Part.NewEntity((short)Obj3dType.o3d_sketch);
            sketchDefinition = sketch.GetDefinition();
            sketchDefinition.SetPlane(plane);

            sketch.Create();

            _kompas.Document2D = sketchDefinition.BeginEdit();

            DrawLine(rookInfo.UpperBaseDiameter / 2, 0);
            DrawLine(0, rookInfo.UpperBaseHeight);
            DrawLine(-rookInfo.UpperBaseDiameter / 10, 0);
            //диагональ
            var nextPoint = new Point()
            {
                X = 2 * rookInfo.LowerBaseDiameter / 5,
                Y = rookInfo.FullHeight - rookInfo.LowerBaseHeight - rookInfo.UpperBaseHeight,
            };
            var changePoint = new Point()
            {
                X = nextPoint.X - _point.X,
                Y = nextPoint.Y - _point.Y
            };

            DrawLine(changePoint.X, changePoint.Y);
            DrawLine(rookInfo.LowerBaseDiameter / 10, 0);
            DrawLine(0, rookInfo.LowerBaseHeight);
            DrawLine(-rookInfo.LowerBaseDiameter / 2, 0);

            //ось вращения, 3 - тип линии
            _kompas.Document2D.ksLineSeg(0, 0, 0, rookInfo.FullHeight, 3);

            sketchDefinition.EndEdit();

            Rotate(sketch);
        }

        /// <summary>
        /// Создание верхнего элемента
        /// </summary>
        private void CreateBattlement(int upperBaseHeight,int upperBaseDiameter)
        {
            //TODO: RSDN
            ksEntity battlePlane;
            ksEntity battleSketch;
            ksSketchDefinition battleSketchDefinition;

            battlePlane = _kompas.Part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            battleSketch = _kompas.Part.NewEntity((short)Obj3dType.o3d_sketch);
            battleSketchDefinition = battleSketch.GetDefinition();
            battleSketchDefinition.SetPlane(battlePlane);

            battleSketch.Create();

            _kompas.Document2D = battleSketchDefinition.BeginEdit();

            //центр окружности, на которой будут отрисовываться зубчики
            var center = new Point();

            _kompas.Document2D.ksCircle(center.X, center.X, upperBaseDiameter / 2, 1);
            _kompas.Document2D.ksCircle(center.X, center.X, upperBaseDiameter / 2.2, 1); 

            battleSketchDefinition.EndEdit();
            Extrude(battleSketch, upperBaseHeight);
        }

        /// <summary>
        /// Выдавливание
        /// </summary>
        /// <param name="sketch"></param>
        private void Extrude(ksEntity sketch, int upperBaseHeight)
        {
            int depth = upperBaseHeight;
            var extrudeEntity = (ksEntity)_kompas.Part
                .NewEntity((short)Obj3dType.o3d_bossExtrusion);
            // интерфейс базовой операции выдавливания
            var extrudeDefinition = (ksBossExtrusionDefinition)extrudeEntity
                .GetDefinition();
            // интерфейс структуры параметров выдавливания
            ksExtrusionParam extrudeParameters = (ksExtrusionParam)extrudeDefinition
                .ExtrusionParam();
            extrudeParameters.direction = (short)Direction_Type.dtNormal;
            // интерфейс структуры параметров тонкой стенки
            ksThinParam thinParam = (ksThinParam)extrudeDefinition.ThinParam();

            extrudeDefinition.SetSketch(sketch);
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

            extrudeEntity.Create();
        }

        /// <summary>
        /// Выдавливание вращением
        /// </summary>
        /// <param name="sketch"></param>
        private void Rotate(ksEntity sketch)
        {
            //TODO: RSDN
            ksEntity rotatedEntity;
            ksBaseRotatedDefinition rotateDefinition;

            //интерфейс объекта "операция выдавливания вращением"
            rotatedEntity = (ksEntity)_kompas.Part.NewEntity((short)Obj3dType.o3d_baseRotated);

            //интерфейс параметров операции  "выдавливание вращением"
            rotateDefinition = (ksBaseRotatedDefinition)rotatedEntity.GetDefinition();
            rotateDefinition.directionType = (short)Direction_Type.dtBoth;
            rotateDefinition.SetSideParam(true, 360);
            rotateDefinition.SetSketch(sketch);
            rotatedEntity.Create();
        }

        /// <summary>
        /// Создание фаски на выбранном ребре
        /// </summary>
        /// <param name="radiusCrossTie">Радиус</param>
        /// <param name="x">X-координата точки на ребре</param>
        /// <param name="y">Y-координата точки на ребре</param>
        /// <param name="z">Z-координата точки на ребре</param>
        private void CreateFillet(int radiusCrossTie, int x,
            int y, int z)
        {
            var filletEntity = (ksEntity)_kompas.Part.NewEntity((short)Obj3dType.o3d_fillet);
            var filletDef = (ksFilletDefinition)filletEntity.GetDefinition();
            filletDef.radius = radiusCrossTie;
            filletDef.tangent = true;
            ksEntityCollection iArray = filletDef.array();
            ksEntityCollection iCollection = _kompas.Part.EntityCollection((short)Obj3dType.o3d_edge);

            iCollection.SelectByPoint(x, y, z);
            var iEdge = iCollection.Last();
            iArray.Add(iEdge);
            filletEntity.Create();
        }
    }
}

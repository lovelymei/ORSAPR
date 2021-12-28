using Kompas6API5;
using Kompas6Constants3D;
using System;
using System.Runtime.InteropServices;

namespace KompasApi
{
    /// <summary>
    /// класс для подключения к КОМПАС 3D
    /// </summary>
    public class KompasConnector
    {
        /// <summary>
        /// Объект KompasObject
        /// </summary>
        private KompasObject _kompasObject;

        /// <summary>
        /// 3д-документ
        /// </summary>
        public ksDocument3D Document3D;

        /// <summary>
        /// 2д-документ
        /// </summary>
        public ksDocument2D Document2D;

        /// <summary>
        /// Интерфейс ksPart
        /// </summary>
        public ksPart Part;

        /// <summary>
        /// свойство для _kompasObject
        /// </summary>
        public KompasObject KompasObject
        {
            get { return _kompasObject; }
            set { _kompasObject = value; }
        }

        /// <summary>
        /// Открытие КОМПАС 3Д
        /// </summary>
        public void OpenKompas3D()
        {

            if (_kompasObject == null)
            {
                var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompasObject = (KompasObject)Activator.CreateInstance(kompasType);
            }

            if (_kompasObject != null)
            {
                var retry = true;
                short tried = 0;
                while (retry)
                {
                    try
                    {
                        tried++;
                        _kompasObject.Visible = true;
                        retry = false;
                    }
                    catch (COMException)
                    {
                        var kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                        _kompasObject =
                            (KompasObject)Activator.CreateInstance(kompasType);

                        if (tried > 3)
                        {
                            retry = false;
                        }
                    }
                }
                _kompasObject.ActivateControllerAPI();
                Document3D = _kompasObject.Document3D();
                Document3D.Create(false, true);
                Document2D = _kompasObject.Document2D();
                Part = (ksPart)Document3D.GetPart((int)Part_Type.pTop_Part);
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public KompasConnector() 
        {
            OpenKompas3D();
        }
    }
}

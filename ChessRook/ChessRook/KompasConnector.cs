using Kompas6API5;
using System;
namespace KompasApi
{
    //TODO: RSDN
    public class KompasConnector
    {
        /// <summary>
        /// Объект KompasObject
        /// </summary>
        private KompasObject _kompasObject;

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
                Type t = Type.GetTypeFromProgID("KOMPAS.Application.5");
                _kompasObject = (KompasObject)Activator.CreateInstance(t);
            }

            if (_kompasObject != null)
            {
                _kompasObject.Visible = true;
                _kompasObject.ActivateControllerAPI();
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

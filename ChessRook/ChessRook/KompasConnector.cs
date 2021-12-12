using Kompas6API5;
using System;
using System.Runtime.InteropServices;

namespace ChessRook
{
    public class KompasConnector
    {

        private KompasObject _kompasObject;

        public KompasObject KompasObject
        {
            get { return _kompasObject; }
            set { _kompasObject = value; }
        }

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


        public KompasConnector() 
        {
            OpenKompas3D();


        }
    }
}

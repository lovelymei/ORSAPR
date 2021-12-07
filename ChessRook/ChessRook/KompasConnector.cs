using Kompas6API5;
using System;
using System.Runtime.InteropServices;

namespace ChessRook
{
    class KompasConnector
    {

        private KompasObject _kompasObject;

        public KompasObject KompasObject
        {
            get { return _kompasObject; }
            set { _kompasObject = value; }
        }

        public void OpenKompas3D()
        {
            if (!IsActiveKompas3D(out var kompasObject))
            {
                if (!CreateKompasInstance(out kompasObject))
                {
                    throw new ArgumentException("Не получилось создать новый экземпляр КОМПАС 3Д");
                }
            }
            kompasObject.Visible = true;
            kompasObject.ActivateControllerAPI();
            _kompasObject = kompasObject;
        }

        private bool IsActiveKompas3D(out KompasObject kompasObject)
        {
            try
            {
                kompasObject = (KompasObject)Marshal.GetActiveObject("KOMPAS.Application.5");
                return true;
            }
            catch (COMException)
            {
                kompasObject = null;
                return false;
            }
        }

        private bool CreateKompasInstance(out KompasObject kompasObject)
        {
            try
            {
                var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
                kompasObject = (KompasObject)Activator.CreateInstance(type);
                return true;
            }
            catch(COMException)
            {
                kompasObject = null;
                return false;
            }
        }

        public KompasConnector() { }
    }
}

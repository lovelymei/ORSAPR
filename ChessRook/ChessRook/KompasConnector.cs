using Kompas6API5;
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

        public KompasConnector() { }
    }
}

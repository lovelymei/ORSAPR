namespace ChessRook
{
    class ModelCreator
    {
        //TODO: изменить диаграмму 1. ksDocument3DNotify7 поменять на ksDocument3D
        //2. из ModelCreator удалить kompasObject, если он окажется ненужным все-таки
        //TODO: разобраться с ksDocument3DNotify7 - кажется, надо что-то другое
        //private ksDocument3DNotify7 _document3D;

        private RookInfo _rookInfo;
        public ksDocument3D Document3D;

        //что нужно подключить, чтобы это заработало?
        //private KompasObject _kompasObject;

        ModelCreator(RookInfo rookInfo)
        {
            _rookInfo.FullHeight = rookInfo.FullHeight;
            _rookInfo.LowerBaseDiameter = rookInfo.LowerBaseDiameter;
            _rookInfo.LowerBaseHeight = rookInfo.LowerBaseHeight;
            _rookInfo.UpperBaseDiameter = rookInfo.UpperBaseDiameter;
            _rookInfo.UpperBaseHeight = rookInfo.UpperBaseHeight;
        }
    }
}

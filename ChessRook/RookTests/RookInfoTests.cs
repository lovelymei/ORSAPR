using NUnit.Framework;
using Rook;

namespace RookTests
{
    //TODO: RSDN
    [TestFixture]
    public class RookInfoTests
    {
        [Test(Description ="Тест геттера и сеттера класса RookInfo свойства FullHeight")]
        public void GetFullHeight_ResultCorrect()
        {
            var expected = 10;
            var rook = new RookInfo { FullHeight = expected };

            Assert.AreEqual(expected, rook.FullHeight);
        }

        [Test(Description = "Тест геттера и сеттера класса RookInfo свойства UpperBaseHeight")]
        public void GetUpperBaseHeight_ResultCorrect()
        {
            var expected = 10;
            var rook = new RookInfo { UpperBaseHeight = expected };

            Assert.AreEqual(expected, rook.UpperBaseHeight);
        }

        [Test(Description = "Тест геттера и сеттера класса RookInfo свойства LowerBaseHeight")]
        public void GetLowerBaseHeight_ResultCorrect()
        {
            var expected = 10;
            var rook = new RookInfo { LowerBaseHeight = expected };

            Assert.AreEqual(expected, rook.LowerBaseHeight);
        }

        [Test(Description = "Тест геттера и сеттера класса RookInfo свойства LowerBaseDiameter")]
        public void GetLowerBaseDiameter_ResultCorrect()
        {
            var expected = 10;
            var rook = new RookInfo { LowerBaseDiameter = expected };

            Assert.AreEqual(expected, rook.LowerBaseDiameter);
        }

        [Test(Description = "Тест геттера и сеттера класса RookInfo свойства UpperBaseDiameter")]
        public void GetUpperBaseDiameter_ResultCorrect()
        {
            var expected = 10;
            var rook = new RookInfo { UpperBaseDiameter = expected };

            Assert.AreEqual(expected, rook.UpperBaseDiameter);
        }

        [Test(Description = "Тест сеттера класса RookInfo свойства FullHeight")]
        public void SetFullHeight_NotSetted()
        {
            int expected = default;
            var rook = new RookInfo();
            var wrongValue = 1;

            rook.FullHeight = wrongValue;

            Assert.AreEqual(expected, rook.FullHeight);
        }

        [Test(Description = "Тест сеттера класса RookInfo свойства UpperBaseHeight")]
        public void SetUpperBaseHeight_NotSetted()
        {
            int expected = default;
            var rook = new RookInfo();
            var wrongValue = 1;

            rook.UpperBaseHeight = wrongValue;

            Assert.AreEqual(expected, rook.UpperBaseHeight);
        }

        [Test(Description = "Тест сеттера класса RookInfo свойства LowerBaseHeight")]
        public void SetLowerBaseHeight_NotSetted()
        {
            int expected = default;
            var rook = new RookInfo();
            var wrongValue = 1;

            rook.LowerBaseHeight = wrongValue;

            Assert.AreEqual(expected, rook.LowerBaseHeight);
        }

        [Test(Description = "Тест сеттера класса RookInfo свойства LowerBaseDiameter")]
        public void SetLowerBaseDiameter_NotSetted()
        {
            int expected = default;
            var rook = new RookInfo();
            var wrongValue = 1;

            rook.LowerBaseDiameter = wrongValue;

            Assert.AreEqual(expected, rook.LowerBaseDiameter);
        }

        [Test(Description = "Тест сеттера класса RookInfo свойства UpperBaseDiameter")]
        public void SetUpperBaseDiameter_NotSetted()
        {
            int expected = default;
            var rook = new RookInfo();
            var wrongValue = 1;

            rook.UpperBaseDiameter = wrongValue;

            Assert.AreEqual(expected, rook.UpperBaseDiameter);
        }

        [Test(Description = "Тест метода Validation класса RookInfo. Он должен вернуть true")]
        public void Validation_ReturnsTrue()
        {
            int value = 10;
            int max = 100;
            int min = 5;

            var rook = new RookInfo();

            var actual = rook.Validation(value, min, max);
            Assert.IsTrue(actual);
        }

        [Test(Description = "Тест метода Validation класса RookInfo. Он должен вернуть false")]
        public void Validation_ReturnsFalse()
        {
            int value = 700;
            int max = 100;
            int min = 5;

            var rook = new RookInfo();

            var actual = rook.Validation(value, min, max);
            Assert.IsFalse(actual);
        }
    }
}

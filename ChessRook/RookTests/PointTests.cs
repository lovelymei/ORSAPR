using NUnit.Framework;
using Rook;

namespace RookTests
{
    //TODO: RSDN
    [TestFixture]
    public class PointTests
    {
        [Test(Description = "Тест геттера и сеттера класса RookInfo свойства X")]
        public void GetX_ResultCorrect()
        {
            var expected = 10;
            var point = new Point { X = expected };

            Assert.AreEqual(expected, point.X);
        }

        [Test(Description = "Тест геттера и сеттера класса RookInfo свойства Y")]
        public void GetY_ResultCorrect()
        {
            var expected = 10;
            var point = new Point { Y = expected };

            Assert.AreEqual(expected, point.Y);
        }
    }
}

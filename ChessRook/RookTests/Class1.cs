using KompasApi;
using NUnit.Framework;
using Rook;
using System;
using System.Diagnostics;

namespace RookTests
{
    [TestFixture]
    class Class1
    {
        [Test]
        public void Test1()
        {
            var rook = new RookInfo();

            rook.FullHeight = 40;
            rook.LowerBaseDiameter = 20;
            rook.UpperBaseDiameter = 10;
            rook.UpperBaseHeight = 5;
            rook.LowerBaseHeight = 5;
            rook.HasFillet = false;

            var creator = new ModelCreator();
            var timer = new Stopwatch();
            var count = 1;
            while (true)
            {
                timer.Start();

                creator.CreateRook(rook);

                timer.Stop();

                TimeSpan timeTaken = timer.Elapsed;
                string info = $"Time taken (model {count}): " + timeTaken.ToString(@"m\:ss\.fff");
                long memory = GC.GetTotalMemory(true);
                Console.WriteLine(info);
                Console.WriteLine(memory);
                count++;
                if (count == 100)
                    return;
            }
            
        }
    }
}

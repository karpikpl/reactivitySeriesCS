using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace ReactivitySeries
{
    [TestFixture]
    public class BigTest
    {
        [Test]
        [Repeat(1)]
        public void BigTest_Should_WorkInResonableTime()
        {
            // Arrange
            const int testSize = 1000;
            Solver solver = new Solver();
            Random rand = new Random();
            List<string> input = new List<string>(testSize);
            StringBuilder expected = new StringBuilder("0 ");
            for (int i = 0; i < testSize - 1; i++)
            {
                input.Add(String.Format("{0} {1}", i, i + 1));
                for (int j = i + 2; j < testSize - 2; j++)
                {
                    input.Add(String.Format("{0} {1}", i, j));
                }

                expected.Append(i + 1).Append(" ");
            }
            StringBuilder sb = new StringBuilder().AppendFormat("{0} {1}\n", testSize, input.Count);
            foreach (var experiment in input.OrderBy(d => rand.Next()))
            {
                sb.AppendLine(experiment);
            }
            string expectedResult = expected.ToString().TrimEnd() + "\r\n";

            // Act
            //Debug.WriteLine(sb.ToString());
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            using (var outMs = new MemoryStream())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                solver.Solve(ms, outMs);
                sw.Stop();
                Debug.WriteLine("Solved in {0}", sw.Elapsed);
                outMs.Position = 0;
                var result = new StreamReader(outMs).ReadToEnd();

                // Assert
                Assert.That(result, Is.EqualTo(expectedResult));
            }
        }


    }
}

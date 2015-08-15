using System.IO;
using System.Text;
using NUnit.Framework;

namespace ReactivitySeries
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Solver_Should_GiveCorrectAnswer()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"2 1
0 1";
            const string expected = @"0 1
";

            // Act
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            using (var outMs = new MemoryStream())
            {
                solver.Solve(ms, outMs);
                outMs.Position = 0;
                var result = new StreamReader(outMs).ReadToEnd();

                // Assert
                Assert.That(result, Is.EqualTo(expected));
            }
        }

        [Test]
        public void Solver_Should_GiveCorrectAnswer2()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"4 4
0 1
0 2
2 3
1 3";
            const string expected = @"back to the lab
";

            // Act
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(input)))
            using (var outMs = new MemoryStream())
            {
                solver.Solve(ms, outMs);
                outMs.Position = 0;
                var result = new StreamReader(outMs).ReadToEnd();

                // Assert
                Assert.That(result, Is.EqualTo(expected));
            }
        }
    }
}

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
        public void Solver_Should_GiveCorrectAnswer7()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"2 1
1 0";
            const string expected = @"1 0
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

        [Test]
        public void Solver_Should_GiveCorrectAnswer3()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"4 5
0 1
0 2
2 3
1 3
2 1";
            const string expected = @"0 2 1 3
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
        public void Solver_Should_GiveCorrectAnswer4()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"6 5
4 5
1 2
0 1
3 4
2 3";
            const string expected = "0 1 2 3 4 5\r\n";

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
        public void Solver_Should_GiveCorrectAnswer5()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"3 2
0 1
1 2";
            const string expected = @"0 1 2
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
        public void Solver_Should_GiveCorrectAnswer6()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"5 10
5 4
5 2
5 3
1 4
1 2
1 3
4 3
2 3
4 2
5 1";
            const string expected = @"5 1 4 2 3
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
        public void Solver_Should_GiveCorrectAnswer8()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"1 0";
            const string expected = @"0
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
        public void Solver_Should_GiveCorrectAnswer9()
        {
            // Arrange
            Solver solver = new Solver();
            const string input =
@"2 0";
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

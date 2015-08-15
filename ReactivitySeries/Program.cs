using System;

namespace ReactivitySeries
{
    class Program
    {
        static void Main(string[] args)
        {
            Solver solver = new Solver();
            solver.Solve(Console.OpenStandardInput(), Console.OpenStandardOutput());
        }
    }
}

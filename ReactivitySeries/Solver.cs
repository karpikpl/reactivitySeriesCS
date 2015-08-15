using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kattis.IO;

namespace ReactivitySeries
{
    class Solver
    {
        private readonly List<LinkedList<int>> metals = new List<LinkedList<int>>();
        private readonly List<Tuple<int, int>> cache = new List<Tuple<int, int>>();

        public void Solve(Stream inStream, Stream outStream)
        {
            Scanner scanner = new Scanner(inStream);

            StreamWriter writer = new StreamWriter(outStream);

            int metalsCount = scanner.NextInt();
            int experimentsCount = scanner.NextInt();
            int a, b;

            if (metalsCount == 2)
            {
                var result = SolutionFor2(scanner);
                writer.WriteLine(result);
                writer.Flush();
                return;
            }

            if (experimentsCount < metalsCount - 1)
            {
                writer.WriteLine("back to the lab");
                writer.Flush();
                return;
            }

            for (int i = 0; i < experimentsCount; i++)
            {
                // read the experiment
                a = scanner.NextInt();
                b = scanner.NextInt();

                if (ProcessExperiment(a, b, metalsCount))
                {
                    var solution = metals.First(m => m.Count == metalsCount);

                    var last = solution.Last.Value;
                    foreach (var metal in solution)
                    {
                        if (metal != last)
                        {
                            writer.Write(metal);
                            writer.Write(" ");
                        }
                        else
                        {
                            writer.Write(last);
                            writer.Write("\r\n");
                        }

                    }

                    writer.Flush();
                    return;
                }
            }

            writer.WriteLine("back to the lab");

            writer.Flush();
        }

        private string SolutionFor2(Scanner scanner)
        {
            return scanner.Next() + " " + scanner.Next();
        }

        private bool ProcessExperiment(int a, int b, int expectedLength)
        {
            foreach (var tuple in cache)
            {
                if (tuple.Item1 == b)
                {
                    // a , b == Item1, Item 2
                    LinkedList<int> newChain = new LinkedList<int>();
                    newChain.AddFirst(a);
                    newChain.AddLast(b);
                    newChain.AddLast(tuple.Item2);

                    if (MatchNewChain(newChain, expectedLength))
                        return true;
                    metals.Add(newChain);
                }
                if (tuple.Item2 == a)
                {
                    // Item1, Item2 == a, b
                    LinkedList<int> newChain = new LinkedList<int>();
                    newChain.AddFirst(tuple.Item1);
                    newChain.AddLast(a);
                    newChain.AddLast(b);

                    if (MatchNewChain(newChain, expectedLength))
                        return true;
                    metals.Add(newChain);
                }
            }

            cache.Add(new Tuple<int, int>(a, b));
            return false;
        }

        private bool MatchNewChain(LinkedList<int> newChain, int expectedLength)
        {
            if (newChain.Count == expectedLength)
            {
                metals.Add(newChain);
                return true;
            }

            var middle = newChain.First.Next.Value;

            foreach (var metal in metals)
            {
                if (metal.First.Value == middle && metal.First.Next.Value == newChain.Last.Value)
                {
                    // match
                    metal.AddFirst(newChain.First.Value);

                    if (metal.Count == expectedLength)
                        return true;
                }
                if (metal.Last.Value == middle && metal.Last.Previous.Value == newChain.First.Value)
                {
                    // match
                    metal.AddLast(newChain.Last.Value);

                    if (metal.Count == expectedLength)
                        return true;
                }
            }

            return false;
        }
    }
}

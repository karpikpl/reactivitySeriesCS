using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Kattis.IO;

namespace ReactivitySeries
{
    class Solver
    {
        private readonly List<LinkedList<int>> _metals = new List<LinkedList<int>>();
        private readonly List<Tuple<int, int>> _cache = new List<Tuple<int, int>>();

        public void Solve(Stream inStream, Stream outStream)
        {
            Scanner scanner = new Scanner(inStream);

            StreamWriter writer = new StreamWriter(outStream);

            int metalsCount = scanner.NextInt();
            int experimentsCount = scanner.NextInt();

            if (experimentsCount < metalsCount - 1)
            {
                writer.WriteLine("back to the lab");
                writer.Flush();
                return;
            }

            if (metalsCount == 2)
            {
                var result = SolutionFor2(scanner);
                writer.WriteLine(result);
                writer.Flush();
                return;
            }

            if (metalsCount == 1)
            {
                writer.WriteLine(0);
                writer.Flush();
                return;
            }

            for (int i = 0; i < experimentsCount; i++)
            {
                // read the experiment
                var a = scanner.NextInt();
                var b = scanner.NextInt();

                if (ProcessExperiment(a, b, metalsCount))
                {
                    Success(metalsCount, writer);
                    return;
                }
            }

            if (Compact(metalsCount))
            {
                Success(metalsCount, writer);
                return;
            }
#if DEBUG
            if (_metals.Count > 0)
            {
                var bestSolutionLength = _metals.Max(m => m.Count);
                var bestSolution = _metals.First(m => m.Count == bestSolutionLength);
                Debug.WriteLine("best solutuion: " + string.Join(" ", bestSolution));
            }

#endif

            writer.WriteLine("back to the lab");

            writer.Flush();
        }

        private void Success(int expectedLength, StreamWriter writer)
        {
            Debug.WriteLine("Success");
            var solution = _metals.First(m => m.Count == expectedLength);

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
        }

        private string SolutionFor2(Scanner scanner)
        {
            return scanner.Next() + " " + scanner.Next();
        }

        private bool ProcessExperiment(int a, int b, int expectedLength)
        {
            foreach (var tuple in _cache)
            {
                if (tuple.Item1 == b)
                {
                    // a , b == Item1, Item 2
                    LinkedList<int> newChain = new LinkedList<int>();
                    newChain.AddFirst(a);
                    newChain.AddLast(b);
                    newChain.AddLast(tuple.Item2);

                    _metals.Add(newChain);
                    if (newChain.Count == expectedLength)
                        return true;
                }
                if (tuple.Item2 == a)
                {
                    // Item1, Item2 == a, b
                    LinkedList<int> newChain = new LinkedList<int>();
                    newChain.AddFirst(tuple.Item1);
                    newChain.AddLast(a);
                    newChain.AddLast(b);

                    _metals.Add(newChain);
                    if (newChain.Count == expectedLength)
                        return true;
                }
            }

            _cache.Add(new Tuple<int, int>(a, b));

            return false;
        }

        private bool Compact(int expectedLength)
        {
            bool wasChange = true;
            int loops = 0;
            HashSet<string> metalHashSet = new HashSet<string>(_metals.Select(m => string.Join(" ", m)));
            HashSet<string> tmp = new HashSet<string>();

            while (wasChange)
            {
                loops++;
                Debug.WriteLine("loop {0}, metals: {1}", loops, _metals.Count);
                wasChange = false;
                int count = _metals.Count;

                for (int index = 0; index < count; index++)
                {
                    var metal = _metals[index];
                    var metalHash = String.Join(" ", metal);

                    foreach (var tuple in _cache)
                    {
                        if (tuple.Item2 == metal.First.Value)
                        {
                            var hash = tuple.Item1 + " " + metalHash;

                            if (!metalHashSet.Contains(hash) && !tmp.Contains(hash))
                            {
                                var cpy = new int[metal.Count + 1];
                                cpy[0] = tuple.Item1;
                                //metal.AddFirst(tuple.Item1);
                                metal.CopyTo(cpy, 1);

                                tmp.Add(hash);
                                _metals.Add(new LinkedList<int>(cpy));
                                wasChange = true;

                                if (cpy.Length == expectedLength)
                                    return true;
                            }
                        }
                        if (tuple.Item1 == metal.Last.Value)
                        {
                            var hash = metalHash + " " + tuple.Item2;

                            if (!metalHashSet.Contains(hash) && !tmp.Contains(hash))
                            {
                                var cpy = new int[metal.Count + 1];
                                cpy[cpy.Length - 1] = tuple.Item2;
                                //metal.AddLast(tuple.Item2);
                                metal.CopyTo(cpy, 0);

                                tmp.Add(hash);
                                _metals.Add(new LinkedList<int>(cpy));
                                wasChange = true;

                                if (cpy.Length == expectedLength)
                                    return true;
                            }
                        }
                    }
                }

                metalHashSet = tmp;
                tmp.Clear();
                _metals.RemoveRange(0, count);
            }
            Debug.WriteLine("While loops {0}", loops);
            return false;
        }

        private bool MatchNewChain(LinkedList<int> newChain, int expectedLength)
        {
            if (newChain.Count == expectedLength)
            {
                _metals.Add(newChain);
                return true;
            }

            foreach (var metal in _metals)
            {
                if (metal.First.Value == newChain.Last.Previous.Value && metal.First.Next.Value == newChain.Last.Value)
                {
                    // match
                    var node = newChain.Last.Previous.Previous;
                    while (node != null)
                    {
                        metal.AddFirst(node.Value);
                        node = node.Previous;
                    }

                    if (metal.Count == expectedLength)
                        return true;
                }
                if (metal.Last.Value == newChain.First.Next.Value && metal.Last.Previous.Value == newChain.First.Value)
                {
                    // match
                    var node = newChain.First.Next.Next;
                    while (node != null)
                    {
                        metal.AddLast(node.Value);
                        node = node.Next;
                    }

                    if (metal.Count == expectedLength)
                        return true;
                }
            }

            return false;
        }
    }
}

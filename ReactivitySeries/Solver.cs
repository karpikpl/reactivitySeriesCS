using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Kattis.IO;

namespace ReactivitySeries
{
    class Solver
    {
        private readonly HashSet<Tuple<int, int>> _cache = new HashSet<Tuple<int, int>>();

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

                _cache.Add(new Tuple<int, int>(a, b));
            }

            // NEW !!
            var newResult = Solve2(metalsCount, _cache);

            writer.WriteLine(newResult == null ? "back to the lab" : string.Join(" ", newResult));

            writer.Flush();
        }

        private string SolutionFor2(Scanner scanner)
        {
            return scanner.Next() + " " + scanner.Next();
        }

        private static void Solve2(int expectedLength, List<Tuple<int, int>> rules)
        {
            LinkedList<int> solution = new LinkedList<int>();

            for (int i = 0; i < rules.Count; i++)
            {
                var rule = rules[i];
                var node = solution.First;
                var processed = false;

                while (node != null && !processed)
                {
                    if (node.Value == rule.Item2)
                    {
                        node.List.AddBefore(node, rule.Item1);
                        processed = true;
                    }
                    if (node.Value == rule.Item1)
                    {
                        node.List.AddAfter(node, rule.Item2);
                        processed = true;
                    }

                    node = node.Next;
                }

                if (!processed)
                {
                    solution.AddLast(rule.Item1);
                    solution.AddLast(rule.Item2);
                }
            }
        }

        private static int[] Solve2(int n, HashSet<Tuple<int, int>> rules)
        {
            int[] solution = new int[n];
            Dictionary<int, int> indexes = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                solution[i] = n - 1 - i;
                indexes[n - 1 - i] = i;
            }

            return CheckRules(solution, rules, indexes);
        }

        private static int[] CheckRules(int[] solution, HashSet<Tuple<int, int>> rules, Dictionary<int, int> indexes)
        {
            bool allOk = false;
            int swaps = 0;

            while (!allOk)
            {
                allOk = true;

                foreach (var rule in rules)
                {
                    if (indexes[rule.Item1] > indexes[rule.Item2])
                    {
                        //swap
                        solution[indexes[rule.Item1]] = rule.Item2;
                        solution[indexes[rule.Item2]] = rule.Item1;

                        var tmpIndex = indexes[rule.Item1];
                        indexes[rule.Item1] = indexes[rule.Item2];
                        indexes[rule.Item2] = tmpIndex;

                        // we just made a change - start over
                        allOk = false;
                        swaps++;
                        //break;
                    }
                }
            }

            Debug.WriteLine("{0} swaps made", swaps);

            for (int i = 0; i < solution.Length - 1; i++)
            {
                Tuple<int, int> tupleToFind = new Tuple<int, int>(solution[i], solution[i + 1]);

                if (!rules.Contains(tupleToFind))
                {
                    Debug.WriteLine("solution {0} not unique, the rule {1} is missing", string.Join(" ", solution), tupleToFind);
                    return null;
                }
            }

            return solution;
        }
    }
}

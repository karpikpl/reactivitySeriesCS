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

        public void Solve(Stream inStream, Stream outStream)
        {
            HashSet<Tuple<int, int>> cache = new HashSet<Tuple<int, int>>();
            Scanner scanner = new Scanner(inStream);
            StreamWriter writer = new StreamWriter(outStream);

            int metalsCount = scanner.NextInt();
            int experimentsCount = scanner.NextInt();

            if (experimentsCount < metalsCount - 1)
            {
                // not enough data to create unique order
                writer.WriteLine("back to the lab");
                writer.Flush();
                return;
            }

            if (metalsCount == 2)
            {
                // two metals - hadle it right away
                var result = SolutionFor2(scanner);
                writer.WriteLine(result);
                writer.Flush();
                return;
            }

            if (metalsCount == 1)
            {
                // one metal - solution is 0
                writer.WriteLine(0);
                writer.Flush();
                return;
            }

            for (int i = 0; i < experimentsCount; i++)
            {
                // read the experiment
                var a = scanner.NextInt();
                var b = scanner.NextInt();

                var rule = new Tuple<int, int>(a, b);

                cache.Add(rule);
            }

            var newResult = FindSolution(metalsCount, cache);

            writer.WriteLine(newResult == null ? "back to the lab" : string.Join(" ", newResult));
            writer.Flush();
        }

        private string SolutionFor2(Scanner scanner)
        {
            return scanner.Next() + " " + scanner.Next();
        }

        private static int[] CreateInitSolution(HashSet<Tuple<int, int>> rules, int n)
        {
            HashSet<int> metals = new HashSet<int>(Enumerable.Range(0, n));
            LinkedList<int> initSolution = new LinkedList<int>();

            foreach (var rule in rules)
            {
                if (metals.Count == 0)
                    return initSolution.ToArray();

                var node = initSolution.First;
                var processed = false;

                bool canAdd1 = metals.Contains(rule.Item1);
                bool canAdd2 = metals.Contains(rule.Item2);

                if (!canAdd1 && !canAdd2)
                    continue;

                while (node != null && !processed)
                {
                    if (node.Value == rule.Item2 && canAdd1)
                    {
                        metals.Remove(rule.Item1);
                        node.List.AddBefore(node, rule.Item1);
                        processed = true;
                    }
                    if (node.Value == rule.Item1 && canAdd2)
                    {
                        metals.Remove(rule.Item2);
                        node.List.AddAfter(node, rule.Item2);
                        processed = true;
                    }

                    node = node.Next;
                }
            }

            Debug.WriteLine("adding extra {0} at the end", metals.Count);
            foreach (var metal in metals)
            {
                initSolution.AddLast(metal);
            }

            return initSolution.ToArray();
        }

        private static int[] FindSolution(int n, HashSet<Tuple<int, int>> rules)
        {
            int[] solution = CreateInitSolution(rules, n);
            Dictionary<int, int> indexes = new Dictionary<int, int>();

            for (int i = 0; i < n; i++)
            {
                indexes[solution[i]] = i;
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

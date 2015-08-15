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
        /// <summary>
        /// Metal dict (name -> reactivity)
        /// </summary>
        Dictionary<int, float> metals = new Dictionary<int, float>();

        public void Solve(Stream inStream, Stream outStream)
        {
            Scanner scanner = new Scanner(inStream);

            StreamWriter writer = new StreamWriter(outStream);

            int metalsCount = scanner.NextInt();
            int experimentsCount = scanner.NextInt();

            for (int i = 0; i < experimentsCount; i++)
            {
                // read the experiment
                int a = scanner.NextInt();
                int b = scanner.NextInt();

                if (metals.ContainsKey(a) ^ metals.ContainsKey(b))
                {
                    // we know one of the metals

                    if (metals.ContainsKey(a))
                    {
                        metals.Add(b, metals[a] + 1);
                    }
                    else
                    {
                        metals.Add(a, metals[b] - 1);
                    }
                }
                else
                    if (!metals.ContainsKey(a) && !metals.ContainsKey(b))
                    {
                        // we don't know any of the metals
                        metals.Add(a, 0f);
                        metals.Add(b, 1f);
                    }
                    else
                    {
                        // we know both of the metals
                        if (metals[a] < metals[b])
                        {
                            // ok - we don't have to do anything
                        }
                        else if(Math.Abs(metals[a] - metals[b]) < 0.0001)
                        {
                            // equal -> make a smaller
                            metals[a] /= 2f;
                        }
                        else
                        {
                            throw new InvalidOperationException("data is not consistent");
                        }
                    }
            }


            float prev = -1;
            StringBuilder result = new StringBuilder();
            foreach (var metal in metals.OrderBy(m => m.Value))
            {
                if (Math.Abs(prev - metal.Value) < 0.0001)
                {
                    result.Clear();
                    result.Append("back to the lab");
                    break;
                }

                result.Append(metal.Key).Append(" ");
                prev = metal.Value;
            }

            writer.WriteLine(result.ToString().TrimEnd());

            writer.Flush();
        }
    }
}

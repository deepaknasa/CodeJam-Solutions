using CodeJam_Solutions.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2008.QualificationRound
{
    /// <summary>
    /// The urban legend goes that if you go to the Google homepage and search for "Google", the universe will implode. We have a secret to share... It is true! Please don't try it, or tell anyone. All right, maybe not. We are just kidding.
    ///  The same is not true for a universe far far away.In that universe, if you search on any search engine for that search engine's name, the universe does implode!. To combat this, people came up with an interesting solution.All queries are pooled together.They are passed to a central system that decides which query goes to which search engine. The central system sends a series of queries to one search engine, and can switch to another at any time.Queries must be processed in the order they're received. The central system must never send a query to a search engine whose name matches the query. In order to reduce costs, the number of switches should be minimized.
    ///  Your task is to tell us how many times the central system will have to switch between search engines, assuming that we program it optimally.
    ///Input
    /// The first line of the input file contains the number of cases, N.N test cases follow.
    /// Each case starts with the number S -- the number of search engines.The next S lines each contain the name of a search engine. Each search engine name is no more than one hundred characters long and contains only uppercase letters, lowercase letters, spaces, and numbers. There will not be two search engines with the same name.
    /// The following line contains a number Q -- the number of incoming queries.The next Q lines will each contain a query.Each query will be the name of a search engine in the case.
    ///Output
    ///For each input case, you should output:
    ///Case #X: Y
    ///where X is the number of the test case and Y is the number of search engine switches. Do not count the initial choice of a search engine as a switch.
    ///Limits
    ///0 < N ≤ 20
    ///Small dataset
    ///2 ≤ S ≤ 10
    ///0 ≤ Q ≤ 100
    ///Large dataset
    ///2 ≤ S ≤ 100
    ///0 ≤ Q ≤ 1000    
    /// </summary>
    public class ASavingtheUniverse : Base.BaseProblem
    {
        private class EachCase
        {
            public EachCase() { }
            public IEnumerable<string> Queries;
            public Int16 ServersCount;

            public void ProcessCase()
            {
                if (Queries.Count() == 0 || Queries.Count() == 1)
                {
                    return; //default Output = 0 will be used.
                }

                HashSet<string> querySet = new HashSet<string>();

                foreach (var q in Queries)
                {
                    if (!querySet.Contains(q))
                    {
                        if (querySet.Count == ServersCount - 1)
                        {
                            Output++;
                            querySet = new HashSet<string>();
                        }
                        querySet.Add(q);
                    }
                }
            }

            public int Output { get; set; } = 0;
        }

        List<EachCase> puzzleCases;

        protected ASavingtheUniverse()
        {
            SolvePuzzle(smallInputFileName, smallOutputFileName);
            SolvePuzzle(largeInputFileName, largeOutputFileName);
        }

        public ASavingtheUniverse(SolutionMode mode)
        {
            switch (mode)
            {
                case SolutionMode.Small:
                    SolvePuzzle(smallInputFileName, smallOutputFileName);
                    break;
                case SolutionMode.Large:
                    SolvePuzzle(largeInputFileName, largeOutputFileName);
                    break;
                case SolutionMode.Both:
                    SolvePuzzle(smallInputFileName, smallOutputFileName);
                    SolvePuzzle(largeInputFileName, largeOutputFileName);
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        protected override void LoadInputData(string inputFileName, string outputFileName)
        {
            relativePath = GetCallingMethodPath();
            puzzleCases = new List<EachCase>();
            GetCases(Path.Combine(relativePath, inputFileName));
            OutputData(Path.Combine(relativePath, outputFileName));
            puzzleCases = null;
        }


        protected override void GetCases(string input)
        {
            Func<StreamReader, Int16, IEnumerable<string>> getLines = (StreamReader sr, Int16 lineCount) =>
            {
                List<string> result = new List<string>();
                for (int i = 0; i < lineCount; i++)
                {
                    if (sr.Peek() >= 0)
                    {
                        result.Add(sr.ReadLine());
                    }
                    else
                    {
                        return result;
                    }
                }
                return result;
            };

            using (var sr = new StreamReader(input))
            {
                Int16 NCases = Convert.ToInt16(sr.ReadLine());
                for (int i = 0; i < NCases; i++)
                {
                    var newCase = new EachCase();
                    newCase.ServersCount = Convert.ToInt16(sr.ReadLine());
                    getLines(sr, newCase.ServersCount);  //ignore server names as it will not be required in the solution.

                    Int16 queriesCount = Convert.ToInt16(sr.ReadLine());
                    newCase.Queries = getLines(sr, queriesCount);

                    puzzleCases.Add(newCase);
                }
            }

            DoCalculations();
        }

        protected override void DoCalculations()
        {
            foreach (var c in puzzleCases.Select((val, i) => new { i, val }))
            {
                c.val.ProcessCase();
            }
        }

        protected override void OutputData(string outputPath)
        {
            using (var sw = new StreamWriter(outputPath))
            {
                foreach (var c in puzzleCases.Select((val, i) => new { i, val }))
                {
                    Console.WriteLine($"Case #{c.i + 1}: {c.val.Output}");
                    sw.WriteLine($"Case #{c.i + 1}: {c.val.Output}");
                }
            }
        }
    }
}

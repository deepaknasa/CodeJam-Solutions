using CodeJam_Solutions.Base;
using CodeJam_Solutions.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2008.Round1C
{
    class BUglyNumbers : BaseProblem<BUglyNumbersCase>
    {
        public BUglyNumbers()
        {
        }

        public BUglyNumbers(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => $"B{base.smallInputFileName}";

        protected override string largeInputFileName => $"B{base.largeInputFileName}";

        protected override string smallOutputFileName => $"B{base.smallOutputFileName}";

        protected override string largeOutputFileName => $"B{base.largeOutputFileName}";

        protected override bool showOutputDescription => false;

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void GetCases(string input)
        {
            using (var stream = new StreamReader(input))
            {
                var cases = stream.GetLines(1).FirstOrDefault().ToLong();
                for (long i = 0; i < cases; i++)
                {
                    var eachCase = new BUglyNumbersCase();

                    var data = stream.GetLines(1).FirstOrDefault();
                    eachCase.dataString = data;
                    puzzleCases.Add(eachCase);
                }
            }
            base.GetCases(input);
        }

        protected override void RunSolution(string inputFileName, string outputFileName)
        {
            relativePath = GetCallingMethodPath();
            base.RunSolution(inputFileName, outputFileName);
        }
    }

    public class BUglyNumbersCase : IndividualCase
    {
        internal string dataString;
        internal long output;
        private const int MOD = 2 * 3 * 5 * 7;

        public BUglyNumbersCase()
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string OutputCase()
        {
            return $"{output}";
        }

        public override string OutputCaseDesc()
        {
            return base.OutputCaseDesc();
        }

        public override void ProcessCase()
        {
            long[,] dyn = new long[41, MOD];
            dyn[0, 0] = 1;
            for (int i = 0; i < dataString.Length; i++)
                for (int sgn = (i == 0) ? 1 : -1; sgn <= 1; sgn += 2)
                {
                    int cur = 0;
                    for (int j = i; j < dataString.Length; j++)
                    {
                        cur = (cur * 10 + dataString[j] - '0') % MOD;
                        for (int x = 0; x < MOD; x++)
                            dyn[j + 1, (x + sgn * cur + MOD) % MOD] += dyn[i, x];
                    }
                }
            output = 0;
            for (int x = 0; x < MOD; x++)
                if (x % 2 == 0 || x % 3 == 0 || x % 5 == 0 || x % 7 == 0)
                    output += dyn[dataString.Length, x];
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

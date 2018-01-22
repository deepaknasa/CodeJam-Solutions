using CodeJam_Solutions.Base;
using CodeJam_Solutions.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace CodeJam_Solutions._2008.Round1A
{
    public class CNumbers : BaseProblem<NumberCase>
    {
        public CNumbers(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => $"C{base.smallInputFileName}";

        protected override string largeInputFileName => $"C{base.largeInputFileName}";

        protected override string smallOutputFileName => $"C{base.smallOutputFileName}";

        protected override string largeOutputFileName => $"C{base.largeOutputFileName}";

        protected override bool showOutputDescription => base.showOutputDescription;

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
            using (var sr = new StreamReader(input))
            {
                Int16 cases = Convert.ToInt16(sr.GetLines(1).FirstOrDefault());
                for (int i = 0; i < cases; i++)
                {
                    var newCase = new NumberCase
                    {
                        N = Convert.ToInt32(sr.GetLines(1).FirstOrDefault())
                    };
                    puzzleCases.Add(newCase);
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

    public class NumberCase : IndividualCase
    {
        public int N = 2;
        public string Result { get; set; }
        public override string OutputCase()
        {
            return $"{Result}";
        }

        public override string OutputCaseDesc()
        {
            return $"{Result}";
        }

        public override void ProcessCase()
        {
            int[,] c = new int[2, 2] { { 6, -4 }, { 1, 0 } };

            var b = Pow(c, N);

            var res = 6 * b[1, 0] + 2 * b[1, 1];

            res = (res + 1999) % 1000;

            if (res < 100)
            {
                if (res < 10)
                {
                    Result = $"00{res}";
                }
                else
                {
                    Result = $"0{res}";
                }
            }
            else
            {
                Result = $"{res % 1000}";
            }
        }

        private static int[,] Pow(int[,] a, int n)
        {
            if (n == 1)
            {
                return a;
            }

            if (n % 2 == 0)
            {
                return Pow(Multiply(a, a), n / 2);
            }
            else
            {
                return Multiply(Pow(Multiply(a, a), n / 2), a);
            }
        }

        private static int[,] Multiply(int[,] a, int[,] b)
        {
            int[,] result = new int[2, 2];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    result[i, j] = 0;

                    for (int k = 0; k < 2; k++)
                    {
                        result[i, j] += a[i, k] * b[k, j];
                        result[i, j] = (result[i, j] + 1000) % 1000;
                    }
                }
            }
            return result;
        }
    }
}

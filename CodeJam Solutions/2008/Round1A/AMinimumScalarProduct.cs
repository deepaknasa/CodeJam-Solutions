using CodeJam_Solutions.Base;
using CodeJam_Solutions.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2008.Round1A
{
    public class AMinimumScalarProduct : BaseProblem<MinScaler>
    {
        public AMinimumScalarProduct()
        {
        }

        public AMinimumScalarProduct(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => "A-small-practice.in";
        protected override string largeInputFileName => "A-large-practice.in";

        protected override string smallOutputFileName => "A-small-practice.out";
        protected override string largeOutputFileName => "A-large-practice.out";

        protected override bool showOutputDescription => base.showOutputDescription;

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
                    var newCase = new MinScaler
                    {
                    };
                    var numbers = sr.ReadLine();
                    newCase.n = Convert.ToInt16(numbers);

                    var x1Cases = getLines(sr, 1);
                    var x2Cases = getLines(sr, 1);
                    newCase.x1 = (from s in x1Cases.First().Split(' ') select Convert.ToInt64(s)).ToArray();
                    newCase.x2 = (from s in x2Cases.First().Split(' ') select Convert.ToInt64(s)).ToArray();

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

    public class MinScaler : IndividualCase
    {
        public Int64[] x1;
        public Int64[] x2;
        public int n;
        public Int64 output;
        public override string OutputCase()
        {
            return $"{output}";
        }

        public override string OutputCaseDesc()
        {
            return $"N: {n}.\tX1: {string.Join(",", x1)}\tY1: {string.Join(",", x2)}";
        }

        public override void ProcessCase()
        {
            //X1: sort in asc 
            Array.Sort<Int64>(x1, new ArrComparer());

            //X2: sort in desc
            Array.Sort<Int64>(x2, new ArrComparer(SortOrder.Desc));
            output = 0;
            for (int i = 0; i < n; i++)
            {
                output += x1[i] * x2[i];
            }
        }
    }
}

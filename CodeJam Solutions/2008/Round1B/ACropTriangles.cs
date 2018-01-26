using CodeJam_Solutions.Base;
using CodeJam_Solutions.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2008.Round1B
{
    public class ACropTriangles : BaseProblem<CropCase>
    {
        public ACropTriangles()
        {
        }

        public ACropTriangles(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => $"A{base.smallInputFileName}";

        protected override string largeInputFileName => $"A{base.largeInputFileName}";

        protected override string smallOutputFileName => $"A{base.smallOutputFileName}";

        protected override string largeOutputFileName => $"A{base.largeOutputFileName}";

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
            using (var stream = new StreamReader(input))
            {
                var cases = stream.GetLines(1).First().ToInt();
                for (int i = 0; i < cases; i++)
                {
                    CropCase newCase = new CropCase();
                    var data = stream.GetLines(1).First().Split(' ');
                    newCase.n = data[0].ToInt();
                    newCase.A = data[1].ToUInt64();
                    newCase.B = data[2].ToUInt64();
                    newCase.C = data[3].ToUInt64();
                    newCase.D = data[4].ToUInt64();
                    newCase.x0 = data[5].ToUInt64();
                    newCase.y0 = data[6].ToUInt64();
                    newCase.M = data[7].ToUInt64();
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

    public class CropCase : IndividualCase
    {
        public UInt64 Result { get; set; }
        public int n { get; set; }
        public UInt64 M { get; set; }
        public UInt64 A { get; set; }
        public UInt64 B { get; set; }
        public UInt64 C { get; set; }
        public UInt64 D { get; set; }
        public UInt64 x0 { get; set; }
        public UInt64 y0 { get; set; }

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
            return $"{Result}";
        }

        public override string OutputCaseDesc()
        {
            return $"{Result}";
        }

        public override void ProcessCase()
        {
            UInt64[] bucket = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            bucket[((Int64)x0 % 3) * 3 + (Int64)y0 % 3]++;
            for (int i = 1; i < n; i++)
            {
                x0 = (A * x0 + B) % M;
                y0 = (C * y0 + D) % M;
                bucket[((Int64)x0 % 3) * 3 + (Int64)y0 % 3]++;
            }
            Result = 0;
            for (int i = 0; i < 9; i++)
            {
                Result += (UInt64)(bucket[i] * (bucket[i] - 1) * (bucket[i] - 2) / 6);
            }
            for (int i = 0; i < 9; i++)
            {
                for (int j = i + 1; j < 9; j++)
                {
                    for (int k = j + 1; k < 9; k++)
                    {
                        if ((i / 3 + j / 3 + k / 3) % 3 == 0 &&
                            (i % 3 + j % 3 + k % 3) % 3 == 0)
                        {
                            Result += (UInt64)(bucket[i] * bucket[j] * bucket[k]);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}

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
    public class CIncreasingSpeedLimitsCase : IndividualCase
    {
        public long Result;
        public long n, m, X, Y, Z;
        public long[] A, intervals;
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
            Result = 0L;
            Result = fasterSolution();
        }

        long fasterSolution()
        {
            long[] sortedIntervals = (long[])intervals.Clone();
            Array.Sort(sortedIntervals);
            long[] f = new long[n];
            int fc = 0;
            for (int i = 0; i < n; i++)
            {
                if (i == 0 || sortedIntervals[i] > sortedIntervals[i - 1])
                    f[fc++] = sortedIntervals[i];
            }

            FenwickTree ft = new FenwickTree(fc);

            for (int i = 0; i < n; i++)
            {
                int r = 1;
                int j = Array.BinarySearch(f, 0, fc, intervals[i]);
                j += 1;
                if (j > 1)
                    r = (r + ft.Sum(j - 1)) % 1000000007;
                ft.AddValue(j, r);
            }
            return ft.Sum(fc);
        }

        class FenwickTree
        {
            int n;
            int[] a;

            public FenwickTree(int n)
            {
                n += 1;
                this.n = n;
                a = new int[n];
            }

            // Returns sum of elements in range [1, i];
            public int Sum(int i)
            {
                int res = 0;
                while (i > 0)
                {
                    res = (res + a[i]) % 1000000007;
                    i -= i & (-i);
                }
                return res;
            }

            // Adds x to the i-th element
            public void AddValue(int i, int x)
            {
                while (i < n)
                {
                    a[i] = (a[i] + x) % 1000000007;
                    i += i & (-i);
                }
            }
        }

        long SlowSolution()
        {
            long[] sum = new long[n];

            for (long i = (n - 1); i >= 0; i--)
            {
                sum[i] = 1;
                for (long j = i + 1; j < n; j++)
                {
                    if (intervals[i] < intervals[j])
                    {
                        sum[i] = (sum[i] + sum[j]) % 1000000007;
                    }
                }
                Result = (sum[i] + Result) % 1000000007;
            }

            return Result;
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }


    public class CIncreasingSpeedLimits : BaseProblem<CIncreasingSpeedLimitsCase>
    {
        public CIncreasingSpeedLimits()
        {
        }

        public CIncreasingSpeedLimits(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => $"C{base.smallInputFileName}";

        protected override string largeInputFileName => $"C{base.largeInputFileName}";

        protected override string smallOutputFileName => $"C{base.smallOutputFileName}";

        protected override string largeOutputFileName => $"C{base.largeOutputFileName}";

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
                    var eachCase = new CIncreasingSpeedLimitsCase();

                    var data = stream.GetLines(1).FirstOrDefault().Split(' ');
                    eachCase.n = data[0].ToLong();
                    eachCase.m = data[1].ToLong();
                    eachCase.X = data[2].ToLong();
                    eachCase.Y = data[3].ToLong();
                    eachCase.Z = data[4].ToLong();
                    eachCase.A = new long[eachCase.m];
                    eachCase.intervals = new long[eachCase.n];
                    for (long j = 0; j < eachCase.m; j++)
                    {
                        eachCase.A[j] = stream.GetLines(1).FirstOrDefault().ToLong();
                    }

                    for (long a = 0; a < eachCase.n; a++)
                    {
                        eachCase.intervals[a] = eachCase.A[a % eachCase.m];
                        eachCase.A[a % eachCase.m] = (eachCase.X * eachCase.A[a % eachCase.m] + eachCase.Y * (a + 1)) % eachCase.Z;
                    }
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
}

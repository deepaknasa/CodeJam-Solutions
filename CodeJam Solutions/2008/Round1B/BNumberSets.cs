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
    public class NumberSetCase : IndividualCase
    {
        public long A;
        public long B;
        public long P;
        public long Result;

        //public UInt64 A;
        //public UInt64 B;
        //public UInt64 P;
        //public UInt64 Result;
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
            long N = B - A + 1;
            Result = N;
            if (P >= N) return;

            //find prime numbers less than N {length of interval}
            var primes = new List<long>();
            bool isPrime = true;
            for (long i = P; i < N; i++)
            {
                isPrime = true;
                for (int j = 2; j * j <= i; j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                    }
                }
                if (isPrime)
                {
                    primes.Add(i);
                }
            }

            //var numCheck = new Dictionary<long, long>();
            long[] taken = new long[N];
            for (int i = 0; i < N; i++)
            {
                taken[i] = i;
            }

            foreach (var prime in primes)
            {
                var offset = prime - A % prime;
                if (offset == prime)    offset = 0L;

                var startingPoint = A + offset;
                var parentPrime = prime;
                var lowestParent = N;
                for (long i = startingPoint; i <= B; i += prime)
                {
                    long currentIndex = i - A;
                    while (taken[currentIndex] != currentIndex) currentIndex = taken[currentIndex];
                    lowestParent = Math.Min(lowestParent, currentIndex);
                }

                for (long i = startingPoint; i <= B; i += prime)
                {
                    long currentIndex = i - A;
                    while (taken[currentIndex] != currentIndex) currentIndex = taken[currentIndex];
                    taken[currentIndex] = lowestParent;
                }
            }

            bool[] setInfo = new bool[N];

            for (long i = A; i <= B; i += 1)
            {
                long currentIndex = i - A;
                while (taken[currentIndex] != currentIndex) currentIndex = taken[currentIndex];
                setInfo[currentIndex] = true;
            }

            Result = 0;

            for (long i = 0; i < N; i += 1)
            {
                if (setInfo[i]) Result++;
            }


        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class PrimeTree
    {
        //public PrimeTree()
        //{
        //    PrimeNodes = new List<PrimeNode>();
        //}

        //public List<PrimeNode> PrimeNodes;

        //public long GetSetCount()
        //{
        //    var result = (from p in PrimeNodes
        //                  let setCount = p.Nodes.Count() - 1
        //                  select setCount).Sum();

        //    return result;
        //}

        //public void CheckPrimeFactor(long num)
        //{
        //    var mergedNodes = new List<PrimeNode>();

        //    PrimeNode factorFound = null;
        //    foreach (var prime in PrimeNodes)
        //    {
        //        if (prime.checkFactor(num))
        //        {
        //            prime.Nodes.Add(num);

        //            if (factorFound != null)
        //            {
        //                mergedNodes.Add(MergeSet(factorFound, prime));
        //            }
        //            else
        //            {
        //                factorFound = prime;
        //            }

        //        }
        //    }

        //    foreach (var node in mergedNodes)
        //    {
        //        PrimeNodes.Remove(node);
        //    }
        //}

        //public PrimeNode MergeSet(PrimeNode prev, PrimeNode curr)
        //{
        //    prev.PrimesList.AddRange(curr.PrimesList);
        //    foreach (var item in curr.Nodes)
        //    {
        //        if (!prev.Nodes.Contains(item))
        //        {
        //            prev.Nodes.Add(item);
        //        }
        //    }
        //    return curr;
        //}
    }

    public class PrimeNode
    {
        public PrimeNode()
        {
            PrimesList = new List<long>();
            Nodes = new HashSet<long>();
        }

        public List<long> PrimesList { get; set; }
        public HashSet<long> Nodes { get; set; }

        internal void findFactor(long a, long b)
        {

        }
    }

    public class BNumberSets : BaseProblem<NumberSetCase>
    {
        public BNumberSets()
        {
        }

        public BNumberSets(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => $"B{base.smallInputFileName}";

        protected override string largeInputFileName => $"B{base.largeInputFileName}";

        protected override string smallOutputFileName => $"B{base.smallOutputFileName}";

        protected override string largeOutputFileName => $"B{base.largeOutputFileName}";

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
                var cases = stream.GetLines(1).FirstOrDefault().ToInt();
                for (int i = 0; i < cases; i++)
                {
                    var numberCase = new NumberSetCase();
                    var data = stream.GetLines(1).FirstOrDefault().Split(' ');
                    //numberCase.A = data[0].ToUInt64();
                    //numberCase.B = data[1].ToUInt64();
                    //numberCase.P = data[2].ToUInt64();

                    numberCase.A = long.Parse(data[0]);
                    numberCase.B = long.Parse(data[1]);
                    numberCase.P = long.Parse(data[2]);

                    puzzleCases.Add(numberCase);
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

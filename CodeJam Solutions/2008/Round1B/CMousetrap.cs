using CodeJam_Solutions.Base;
using CodeJam_Solutions.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2008.Round1B
{
    public class MousetrapCase : IndividualCase
    {
        public int K;
        public int n;
        public int[] d;
        public int[] Result;
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
            return $"{string.Join(" ", Result)}";
        }

        public override string OutputCaseDesc()
        {
            return $"{string.Join(" ", Result)}";
        }

        public override void ProcessCase()
        {
            SuggestedSolution();
        }

        public void SuggestedSolution()
        {
            int[] answers = new int[n];

            for (int i = 0; i < n; i++)
            {
                answers[i] = -1;
            }

            for (int i = 1, pos = 0; i <= K; i++)
            {
                // Compute the next position, after wrap-around.
                pos = (pos + i - 1) % (K - i + 1);
                for (int j = 0; j < n; j++)
                    if (answers[j] < 0)
                    {
                        if (d[j + 1] == pos + 1)
                        {
                            d[j + 1] = -1; answers[j] = i;
                        }
                        else if (d[j + 1] > pos + 1)
                        {
                            // The effect of deleting the next position.
                            d[j + 1]--;
                        }
                    }
            }

            Result = answers;
        }

        public void SlowSolution()
        {
            if (K == 1)
            {
                Result = new int[] { 1 };
                return;
            }

            Node startPoint = new Node { index = 1 };
            Node prevNode = startPoint;
            for (int i = 2; i < K + 1; i++)
            {
                var eachNode = new Node { index = i };
                prevNode.nextNode = eachNode;
                eachNode.prevNode = prevNode;
                prevNode = eachNode;
                if (i == K)
                {
                    eachNode.nextNode = startPoint;
                    startPoint.prevNode = eachNode;
                }
            }

            int[] perfectDeck = new int[K + 1];
            int availableIndices = K;
            bool[] findAll = new bool[n + 1];
            findAll[0] = true;

            int recount = 1;//reset the step count
            Node node = startPoint;
            perfectDeck[node.index] = 1;
            availableIndices--;
            node.RemoveNode();

            for (int cardNumber = 2; cardNumber < K + 1; cardNumber++)
            {
                if (cardNumber > availableIndices)
                {
                    //recount = cardNumber - availableIndices;
                    recount = cardNumber % availableIndices;
                    if (recount == 0)
                    {
                        recount = availableIndices;
                    }
                }
                else
                {
                    recount = cardNumber;
                }

                while (recount > 0)
                {
                    node = node.nextNode;
                    recount--;
                }

                //node.cardValue = cardNumber;
                perfectDeck[node.index] = cardNumber;
                if (d.Contains(node.index))
                {
                    findAll[Array.IndexOf(d.ToArray(), node.index)] = true;
                }
                if (findAll.All(a => a))
                {
                    break;
                }
                node.RemoveNode();
                availableIndices--;
                Debug.WriteLine("CardNumber {0}: placed at index {1}", cardNumber, node.index);
            }

            Result = new int[n];
            for (int i = 0; i < n; i++)
            {
                Result[i] = perfectDeck[d[i + 1]];
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }

        class Node
        {
            public int index;
            public Node nextNode = null;
            public Node prevNode = null;

            public Node RemoveNode()
            {
                this.prevNode.nextNode = this.nextNode;
                this.nextNode.prevNode = this.prevNode;
                return this.nextNode;
            }
        }
    }

    public class CMousetrap : BaseProblem<MousetrapCase>
    {
        public CMousetrap()
        {
        }

        public CMousetrap(SolutionMode mode) : base(mode)
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
            using (var stream = new StreamReader(input))
            {
                var cases = stream.GetLines(1).FirstOrDefault().ToLong();
                for (long i = 0; i < cases; i++)
                {
                    var eachCase = new MousetrapCase();
                    eachCase.K = stream.GetLines(1).FirstOrDefault().ToInt();
                    var indices = stream.GetLines(1).FirstOrDefault().Split(' ');
                    eachCase.n = indices[0].ToInt();
                    eachCase.d = new int[eachCase.n + 1];
                    eachCase.Result = new int[eachCase.n];
                    for (int j = 1; j <= eachCase.n; j++)
                    {
                        eachCase.d[j] = indices[j].ToInt();
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

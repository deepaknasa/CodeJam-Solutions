using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions.Base
{
    public class ExecutionBase : IDisposable
    {
        Stopwatch watch;

        public ExecutionBase()
        {
            watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            watch.Stop();
            Console.WriteLine($"\n************************************************Program took {watch.ElapsedMilliseconds}ms*******************************");
        }
    }

    public abstract class IndividualCase
    {
        public abstract void ProcessCase();
        public virtual string OutputCase()
        {
            return string.Empty;
        }

        public virtual string OutputCaseDesc()
        {
            return string.Empty;
        }
    }

    public abstract class BaseProblem<T>: ExecutionBase where T: IndividualCase 
    {
        #region Local variables
        protected virtual string smallInputFileName => "-small-practice.in";
        protected virtual string largeInputFileName => "-large-practice.in";

        protected virtual string smallOutputFileName => "-small-practice.out";
        protected virtual string largeOutputFileName => "-large-practice.out";

        protected string relativePath;

        protected List<T> puzzleCases;
        protected virtual bool showOutputDescription => true;
        #endregion

        public BaseProblem(): this(SolutionMode.Both)
        {
        }

        public BaseProblem(SolutionMode mode)
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

        protected void SolvePuzzle(string inputFileName, string outputFileName)
        {
            RunSolution(inputFileName, outputFileName);
        }

        protected virtual void RunSolution(string inputFileName, string outputFileName) {
            puzzleCases = new List<T>();
            GetCases(Path.Combine(relativePath, inputFileName));
            OutputData(Path.Combine(relativePath, outputFileName));
            puzzleCases = null;
        }

        protected virtual void GetCases(string input) {
            DoCalculations();
        }

        protected void DoCalculations() {
            foreach (var c in puzzleCases.Select((val, i) => new { i, val }))
            {
                c.val.ProcessCase();
            }
        }

        protected void OutputData(string outputPath) {
            using (var sw = new StreamWriter(outputPath))
            {
                Func<int, Func<string>, string> outputter = (index, outputFormat) =>
                {
                    return $"Case #{index + 1}: {outputFormat()}";
                };

                foreach (var c in puzzleCases.Select((val, i) => new { i, val }))
                {
                    if (showOutputDescription)
                    {
                        Console.WriteLine(outputter(c.i, c.val.OutputCaseDesc));
                    }
                    sw.WriteLine(outputter(c.i, c.val.OutputCase));
                }
            }
        }



        public string GetCallingMethodPath()
        {
            var stack = new StackTrace();
            var method = stack.GetFrame(1).GetMethod();
            var type = method.ReflectedType;
            var paths = (from p in type.FullName.Split('.').Skip(1).Take(2)
                         select p.TrimStart('_')).ToList();
            paths.Add("Data");
            return Path.Combine(paths.ToArray());
        }
    }

    public enum SolutionMode
    {
        Small,
        Large,
        Both
    }
}

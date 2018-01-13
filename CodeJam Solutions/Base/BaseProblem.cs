using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions.Base
{
    public abstract class BaseProblem
    {
        protected const string smallInputFileName = "A-small-practice.in";
        protected const string largeInputFileName = "A-large-practice.in";

        protected const string smallOutputFileName = "A-small-practice.out";
        protected const string largeOutputFileName = "A-large-practice.out";

        protected string relativePath;

        protected void SolvePuzzle(string inputFileName, string outputFileName)
        {
            LoadInputData(inputFileName, outputFileName);
        }

        protected virtual void LoadInputData(string inputFileName, string outputFileName) { }

        protected virtual void GetCases(string input) {
            DoCalculations();
        }

        protected virtual void DoCalculations() {
            OutputData(string.Empty);
        }

        protected virtual void OutputData(string outputPath) { }



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

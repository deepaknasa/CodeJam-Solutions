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
    public class BMilkshakes : BaseProblem<MilkshakesCase>
    {
        public BMilkshakes()
        {
        }

        public BMilkshakes(SolutionMode mode) : base(mode)
        {
        }

        protected override string smallInputFileName => $"B{base.smallInputFileName}";

        protected override string largeInputFileName => $"B{base.largeInputFileName}";

        protected override string smallOutputFileName => $"B{base.smallOutputFileName}";

        protected override string largeOutputFileName => $"B{base.largeOutputFileName}";

        protected override bool showOutputDescription => base.showOutputDescription;

        protected override void GetCases(string input)
        {
            using (var sr = new StreamReader(input))
            {
                Int16 NCases = Convert.ToInt16(sr.ReadLine());
                for (int i = 0; i < NCases; i++)
                {
                    var newCase = new MilkshakesCase
                    {
                        TotalShakes = Convert.ToInt32(sr.ReadLine()),
                        TotalCustomers = Convert.ToInt32(sr.ReadLine())
                    };

                    var custData = sr.GetLines(newCase.TotalCustomers);

                    for (int cust = 0; cust < newCase.TotalCustomers; cust++)
                    {
                        var prefLine = custData[cust].Split(' ');
                        List<Preference> choices = new List<Preference>();
                        for (int choice = 0; choice < Convert.ToInt32(prefLine[0]); choice++)
                        {
                            choices.Add(new Preference { ShakeNumber = prefLine[2 * choice + 1].ToInt(), IsMalted = prefLine[2 * choice + 2].ToBool() });
                        }
                        newCase.custoPreferences.Add(choices);
                    }
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

    public class MilkshakesCase : IndividualCase
    {
        public MilkshakesCase()
        {
            custoPreferences = new List<List<Preference>>();
        }

        public int TotalShakes;
        public int TotalCustomers;
        public List<List<Preference>> custoPreferences;
        public string result;
        public string Impossible = "IMPOSSIBLE";

        public override string OutputCase()
        {
            return $"{result}";
        }

        public override string OutputCaseDesc()
        {
            return $"{result}";
        }

        public override void ProcessCase()
        {
            List<int> maltedShakes = new List<int>();
            List<string> strResult = new List<string>();

            for (int i = 0; i < TotalShakes; i++)
            {
                maltedShakes = custoPreferences
                .Where(c => c.Count == 1 && c.First().IsMalted)
                .SelectMany(cp => cp)
                .Select(c => c.ShakeNumber)
                .Distinct()
                .ToList();

                for (int m = 0; m < TotalCustomers; m++)
                {
                    custoPreferences[m].RemoveAll(p => maltedShakes.Contains(p.ShakeNumber) && !p.IsMalted);
                }

                if (custoPreferences.Any(c => c.Count == 0))
                {
                    result = Impossible;
                    return;
                    //break;
                }

                if (maltedShakes.Count == TotalShakes)
                {
                    break;
                }
            }



            for (int shake = 1; shake <= TotalShakes; shake++)
            {
                strResult.Add(maltedShakes.Contains(shake) ? "1" : "0");
            }

            result = string.Join(" ", strResult);
        }
    }

    public class Preference
    {
        public int ShakeNumber { get; set; }
        public bool IsMalted { get; set; }
    }
}

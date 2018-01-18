using CodeJam_Solutions.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2008.QualificationRound
{
    /// <summary>
    /// What are your chances of hitting a fly with a tennis racquet?
    /// To start with, ignore the racquet's handle. Assume the racquet is a perfect ring, of outer radius R and thickness t (so the inner radius of the ring is R−t).
    /// The ring is covered with horizontal and vertical strings. Each string is a cylinder of radius r. Each string is a chord of the ring (a straight line connecting two points of the circle). There is a gap of length g between neighbouring strings. The strings are symmetric with respect to the center of the racquet i.e. there is a pair of strings whose centers meet at the center of the ring.
    /// The fly is a sphere of radius f. Assume that the racquet is moving in a straight line perpendicular to the plane of the ring. Assume also that the fly's center is inside the outer radius of the racquet and is equally likely to be anywhere within that radius. Any overlap between the fly and the racquet (the ring or a string) counts as a hit.
    /// Input
    /// One line containing an integer N, the number of test cases in the input file.
    /// The next N lines will each contain the numbers f, R, t, r and g separated by exactly one space. Also the numbers will have at most 6 digits after the double point.
    /// Output
    /// N lines, each of the form "Case #k: P", where k is the number of the test case and P is the probability of hitting the fly with a piece of the /// racquet.
    /// Answers with a relative or absolute error of at most 10-6 will be considered correct.
    /// Limits
    /// f, R, t, r and g will be positive and smaller or equal to 10000.
    /// t < R
    /// f < R
    /// r < R
    /// Small dataset
    /// 1 ≤ N ≤ 30
    /// The total number of strings will be at most 60 (so at most 30 in each direction).
    /// Large dataset
    /// 1 ≤ N ≤ 100
    /// The total number of strings will be at most 2000 (so at most 1000 in each direction).
    /// </summary>
    public class CFlySwatter : BaseProblem<FlySwatterCase>
    {
        public CFlySwatter() { }

        public CFlySwatter(SolutionMode mode) : base(mode) { }

        protected override string smallInputFileName => "C-small-practice.in";
        protected override string largeInputFileName => "C-large-practice.in";

        protected override string smallOutputFileName => "C-small-practice.out";
        protected override string largeOutputFileName => "C-large-practice.out";

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
                    var newCase = new FlySwatterCase();

                    var data = sr.ReadLine().Split(' ');
                    newCase.f = Convert.ToDouble(data[0]);
                    newCase.R = Convert.ToDouble(data[1]);
                    newCase.t = Convert.ToDouble(data[2]);
                    newCase.r = Convert.ToDouble(data[3]);
                    newCase.g = Convert.ToDouble(data[4]);

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

    public class FlySwatterCase : IndividualCase
    {
        public double f { get; set; }
        public double R { get; set; }
        public double t { get; set; }
        public double r { get; set; }
        public double g { get; set; }

        public double Probability { get; set; }

        public override string OutputCase()
        {
            return $"{Probability}";
        }

        public override string OutputCaseDesc()
        {
            return $"f: {f}\t R: {R}\t t: {t}\t r: {r}\t g: {g}\t Probability: {Probability}";
        }

        public override void ProcessCase()
        {
            double radius = R - t - f;
            double ar = 0.0d;
            double r2 = Math.Pow(radius, 2);
            for (double x1 = r + f; x1 < R - t - f; x1 += g + 2 * r)
            {
                for (double y1 = r + f; y1 < R - t - f; y1 += g + 2 * r)
                {
                    double x2 = x1 + g - 2 * f;
                    double y2 = y1 + g - 2 * f;
                    if (x2 <= x1 || y2 <= y1) continue;
                    if (x1 * x1 + y1 * y1 >= radius * radius) continue;

                    if (x2 * x2 + y2 * y2 <= radius * radius)
                    {
                        /* (x1,y1) (x1,y2) (x2,y1) (x2,y2) */
                        ar += (x2 - x1) * (y2 - y1);
                    }
                    else if (x1 * x1 + y2 * y2 >= radius * radius && x2 * x2 + y1 * y1 >= radius * radius)
                    {
                        /* (x1,y1) */
                        double v1 = Math.Sqrt(r2 - x1 * x1) - y1;
                        double v2 = (Math.Sqrt(r2 - y1 * y1) - x1)/2;
                        double cos = Math.Acos(x1 / radius);
                        double sin = Math.Asin(y1 / radius);
                        double v3 = Circle_Segment(radius, cos - sin);

                        ar += v1 * v2 + v3;
                    }
                    else if (x1 * x1 + y2 * y2 >= radius * radius)
                    {
                        /* (x1,y1) (x2,y1) */
                        double cos1 = Math.Acos(x1 / radius);
                        double cos2 = Math.Acos(x2 / radius);
                        double v1 = Circle_Segment(radius, cos1 - cos2);
                        double v2 = x2 - x1;
                        double sq1 = Math.Sqrt(r2 - x1 * x1);
                        double sq2 = Math.Sqrt(r2 - x2 * x2);
                        double v3 = (sq1 - y1 + sq2 - y1);

                        ar += v1 + v2 * v3 / 2;
                    }
                    else if (x2 * x2 + y1 * y1 >= radius * radius)
                    {
                        /* (x1,y1) (x1,y2) */
                        double v1 = Circle_Segment(radius, Math.Asin(y2 / radius) - Math.Asin(y1 / radius));
                        double v2 = (y2 - y1);
                        double sq1 = Math.Sqrt(r2 - y1 * y1);
                        double sq2 = Math.Sqrt(r2 - y2 * y2);
                        double v3 = (sq1 - x1 + sq2 - x1);

                        ar += v1 + v2 * v3 / 2;
                    }
                    else
                    {
                        /* (x1,y1) (x1,y2) (x2,y1) */
                        double v1 = Circle_Segment(radius, Math.Asin(y2 / radius) - Math.Acos(x2 / radius));
                        double v2 = (x2 - x1);
                        double v3 = (y2 - y1);
                        double sq1 = Math.Sqrt(r2 - x2 * x2);
                        double sq2 = Math.Sqrt(r2 - y2 * y2);
                        double v4 = (y2 - sq1);
                        double v5 = (x2 - sq2);

                        ar += v1 + v2 * v3 - v4 * v5 / 2;
                    }
                }
            }
            double PI = 3.14159265358979323846d;
            double p = Math.Round(ar / (PI * R * R / 4), 6);
            Probability = 1.0D - p;
        }

        private double Circle_Segment(double radius, double theta)
        {
            return Math.Pow(radius, 2) * (theta - Math.Sin(theta)) / 2;
        }

    }
}

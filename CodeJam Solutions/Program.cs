using CodeJam_Solutions._2008.QualificationRound;
using CodeJam_Solutions._2008.Round1A;
using CodeJam_Solutions.Base;
using CodeJam_Solutions.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions
{
    class Program
    {
        public static object CSkySwatter { get; private set; }

        static void Main(string[] args)
        {
            //ASavingtheUniverse aSavingtheUniverse = new ASavingtheUniverse();
            //BTrainTimetable bTrainTimetable = new BTrainTimetable();
            //using (CFlySwatter cSkySwatter = new CFlySwatter(SolutionMode.Small)) { }
            //using (AMinimumScalarProduct minScaler = new AMinimumScalarProduct(SolutionMode.Both)) { }
            using (BMilkshakes shakes = new BMilkshakes(SolutionMode.Small)) { }
        }
    }
}

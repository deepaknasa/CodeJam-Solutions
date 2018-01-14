using CodeJam_Solutions._2008.QualificationRound;
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
        static void Main(string[] args)
        {
            //ASavingtheUniverse aSavingtheUniverse = new ASavingtheUniverse();
            
            using (var checkExe = new CheckExecution($"{nameof(BTrainTimetable)}-Small"))
            {
                BTrainTimetable bTrainTimetable = new BTrainTimetable(SolutionMode.Small);
            }

            using (var checkExe = new CheckExecution($"{nameof(BTrainTimetable)}-Large"))
            {
                BTrainTimetable bTrainTimetable = new BTrainTimetable(SolutionMode.Large);
            }
            Console.ReadLine();
        }
    }
}

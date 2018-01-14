using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeJam_Solutions.Base;
using System.IO;

namespace CodeJam_Solutions._2008.QualificationRound
{
    /// <summary>
    /// A train line has two stations on it, A and B. Trains can take trips from A to B or from B to A multiple times during a day. When a train arrives at B from A (or arrives at A from B), it needs a certain amount of time before it is ready to take the return journey - this is the turnaround time. For example, if a train arrives at 12:00 and the turnaround time is 0 minutes, it can leave immediately, at 12:00.
    ///  A train timetable specifies departure and arrival time of all trips between A and B.The train company needs to know how many trains have to start the day at A and B in order to make the timetable work: whenever a train is supposed to leave A or B, there must actually be one there ready to go.There are passing sections on the track, so trains don't necessarily arrive in the same order that they leave. Trains may not travel on trips that do not appear on the schedule.
    ///Input
    ///  The first line of input gives the number of cases, N.N test cases follow.
    ///Each case contains a number of lines.The first line is the turnaround time, T, in minutes.The next line has two numbers on it, NA and NB.NA is the number of trips from A to B, and NB is the number of trips from B to A. Then there are NA lines giving the details of the trips from A to B.
    ///  Each line contains two fields, giving the HH:MM departure and arrival time for that trip. The departure time for each trip will be earlier than the arrival time.All arrivals and departures occur on the same day.The trips may appear in any order - they are not necessarily sorted by time.The hour and minute values are both two digits, zero-padded, and are on a 24-hour clock (00:00 through 23:59).
    ///After these NA lines, there are NB lines giving the departure and arrival times for the trips from B to A.
    ///Output
    ///For each test case, output one line containing "Case #x: " followed by the number of trains that must start at A and the number of trains that must start at B.
    ///Limits
    ///1 ≤ N ≤ 100
    ///Small dataset
    ///0 ≤ NA, NB ≤ 20
    ///0 ≤ T ≤ 5
    ///Large dataset
    ///0 ≤ NA, NB ≤ 100
    ///0 ≤ T ≤ 60
    /// </summary>
    public class BTrainTimetable : BaseProblem<TrainTimeTableCase>
    {
        public BTrainTimetable() { }

        public BTrainTimetable(SolutionMode mode) : base(mode) { }

        protected override string smallInputFileName => "B-small-practice.in";
        protected override string largeInputFileName => "B-large-practice.in";

        protected override string smallOutputFileName => "B-small-practice.out";
        protected override string largeOutputFileName => "B-large-practice.out";
        protected override bool showOutputDescription => false;

        protected override void RunSolution(string inputFileName, string outputFileName)
        {
            relativePath = GetCallingMethodPath();
            base.RunSolution(inputFileName, outputFileName);
        }

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
                    var newCase = new TrainTimeTableCase
                    {
                        Turnaround = Convert.ToInt16(sr.ReadLine())
                    };
                    var trainNumbers = sr.ReadLine().Split(' ');
                    newCase.NA = Convert.ToInt16(trainNumbers[0]);
                    newCase.NB = Convert.ToInt16(trainNumbers[1]);

                    newCase.FromATimes = getLines(sr, newCase.NA);
                    newCase.FromBTimes = getLines(sr, newCase.NB);

                    puzzleCases.Add(newCase);
                }
            }

            base.GetCases(input);
        }
    }

    public class TrainTimeTableCase : IndividualCase
    {
        public Int16 Turnaround { get; set; }
        public Int16 NA { get; set; }
        public Int16 NB { get; set; }

        public IEnumerable<string> FromATimes { get; set; }
        public IEnumerable<string> FromBTimes { get; set; }

        public Int16 OutputA { get; set; } = -1;
        public Int16 OutputB { get; set; } = -1;

        public TrainTimeTableCase() { }

        public override void ProcessCase()
        {
            OutputA = NA;
            OutputB = NB;

            if (NA == 0 && NB == 0)
            {
                return;
            }

            if (NA == 0 && NB != 0)
            {
                return;
            }

            if (NA != 0 && NB == 0)
            {
                return;
            }


            Func<IEnumerable<string>, IEnumerable<string>, Int16, Int16> func = (station1, station2, station1Count) =>
            {
                var aDepartTimes = (from time in station1
                                    let Adeparture = time.Split(' ')[0]
                                    let departMins = Array.ConvertAll(Adeparture.Split(':'), t => Convert.ToInt16(t))
                                                          .Aggregate((accumalted, next) => (Int16)(accumalted * 60 + next))
                                    select departMins).ToArray();

                var aArrivalTimes = (from time in station2
                                     let AArrival = time.Split(' ')[1]
                                     let arrivalMins = Array.ConvertAll(AArrival.Split(':'), t => Convert.ToInt16(t))
                                                           .Aggregate((accumalted, next) => (Int16)(accumalted * 60 + next))
                                     select (Int16)(arrivalMins + Turnaround)).ToArray();

                Array.Sort(aDepartTimes);
                Array.Sort(aArrivalTimes);


                Queue<Int16> arrivalQueue = new Queue<short>(aArrivalTimes);
                if (arrivalQueue.Count == 0)
                {
                    return station1Count;
                }

                Int16 readyToDepartTime = arrivalQueue.Dequeue();
                Int16 output = 0;
                foreach (var departTime in aDepartTimes)
                {
                    station1Count--; //reduce the number of station 1 trains to depart.
                    if (departTime >= readyToDepartTime)
                    {
                        //perfect match found for upcoming departure and a waiting train.
                        if (arrivalQueue.Count == 0)
                        {
                            //no more waiting trains.
                            output += station1Count;

                            //no point continuing this loop as we have reached a point when there's no more train arriving at station 1, 
                            //so train company has to arrange new trains for remaining time table.
                            break;
                        }
                        else
                        {
                            readyToDepartTime = arrivalQueue.Dequeue();
                        }
                        
                    }
                    else
                    {
                        output++;
                    }

                }
                return output;

            };

            OutputA = func(FromATimes, FromBTimes, OutputA);
            OutputB = func(FromBTimes, FromATimes, OutputB);
        }

        public override string OutputCase()
        {
            return $"{OutputA} {OutputB}";
        }

        public override string OutputCaseDesc()
        {
            return $"\n\tA Total:{NA}\tA Output: {OutputA}\tB Total:{NB}\tB Output: {OutputB}\t{Turnaround}\n{string.Join(", ", FromATimes)}\n\n{string.Join(", ", FromBTimes)}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeJam_Solutions._2009.QualificationRound
{
    public class BWatersheds
    {
        int T;
        int[,] altitudes = new int[100, 100];
        string[] input;
        string fileName;
        public BWatersheds()
        {
            fileName = "B-small-practice";
            //string fileName = "B-large-practice";
            input = File.ReadAllLines($"2009/QualificationRound/{fileName}.in");
            ProcessPuzzle();
        }

        public class Map
        {
            int H, W;
            int[,] coordinates;
            int[,] labelling;
            int label = 65;
            public Map(int h, int w, string[] input, ref int i)
            {
                H = h;
                W = w;
                coordinates = new int[H, W];
                labelling = new int[H, W];
                for (int x = 0; x < H; x++)
                {
                    var points = input[++i].Split(new char[] { ' ' });
                    for (int y = 0; y < W; y++)
                    {
                        coordinates[x, y] = Convert.ToInt32(points[y]);
                    }
                }
            }

            public enum Direction
            {
                North,
                South,
                East,
                West,
                None
            }

            public class Directions
            {
                int altitude;
                public Directions(int x, int y, int H, int W, int[,] coordinates)
                {
                    altitude = coordinates[x, y];

                    North = South = East = West = -1;
                    if (x != 0)
                        North = coordinates[x - 1, y];

                    if (x != H - 1)
                        South = coordinates[x + 1, y];

                    if (y != 0)
                        West = coordinates[x, y - 1];

                    if (y != W - 1)
                        East = coordinates[x, y + 1];
                }
                public int North { get; set; }
                public int South { get; set; }
                public int East { get; set; }
                public int West { get; set; }

                public bool IsOutFlowing
                {
                    get
                    {
                        if ((North != -1 && altitude > North) ||
                            (South != -1 && altitude > South) ||
                            (East != -1 && altitude > East) ||
                            (West != -1 && altitude > West))
                            return true;
                        else
                            return false;
                    }
                }

                public Direction OutFlowingCell
                {
                    get
                    {
                        var alts = new int[] { North, South, East, West };
                        var direction = (from alt in alts
                                         where alt != -1
                                         select alt).Min();
                        if (direction == North)
                            return Direction.North;
                        if (direction == West)
                            return Direction.West;
                        if (direction == East)
                            return Direction.East;
                        if (direction == South)
                            return Direction.South;


                        return Direction.None;
                    }
                }

                public Direction InFlowingCell
                {
                    get
                    {
                        var alts = new int[] { North, South, East, West };
                        var direction = (from alt in alts
                                         where alt != -1
                                         select alt).Min();
                        if (direction == North)
                            return Direction.North;
                        if (direction == South)
                            return Direction.South;
                        if (direction == East)
                            return Direction.East;
                        if (direction == West)
                            return Direction.West;

                        return Direction.None;
                    }
                }

                public bool IsInFlowing
                {
                    get
                    {
                        if ((North != -1 && altitude < North) ||
                            (South != -1 && altitude < South) ||
                            (East != -1 && altitude < East) ||
                            (West != -1 && altitude < West))
                            return true;
                        else
                            return false;
                    }
                }

                public bool IsSameLevel
                {
                    get
                    {
                        if ((North != -1 && altitude == North) &&
                            (South != -1 || altitude == South) &&
                            (East != -1 || altitude == East) &&
                            (West != -1 || altitude == West))
                            return true;
                        else
                            return false;
                    }
                }
            }

            //9 6 3
            //5 9 6
            //3 5 9

            //a 
            public void ProcessAltitudes()
            {
                for (int x = 0; x < H; x++)
                    for (int y = 0; y < W; y++)
                        labelling[x, y] = -1;

                for (int x = 0; x < H; x++)
                {
                    for (int y = 0; y < W; y++)
                    {
                        if (labelling[x, y] != -1) continue;

                        Directions directions = new Directions(x, y, H, W, coordinates);
                        if (directions.IsSameLevel)
                        {

                        }
                        Direction cell = Direction.None;
                        if (directions.IsOutFlowing)
                            cell = directions.OutFlowingCell;
                        else if (directions.IsInFlowing)
                            cell = directions.InFlowingCell;
                        switch (cell)
                        {
                            case Direction.North:
                                if (labelling[x - 1, y] == -1)
                                {
                                    labelling[x, y] = label++;
                                    labelling[x - 1, y] = labelling[x, y];
                                }
                                else
                                {
                                    labelling[x, y] = labelling[x - 1, y];
                                }
                                break;
                            case Direction.South:
                                if (labelling[x + 1, y] == -1)
                                {
                                    labelling[x, y] = label++;
                                    labelling[x + 1, y] = labelling[x, y];
                                }
                                else
                                {
                                    labelling[x, y] = labelling[x + 1, y];
                                }
                                break;
                            case Direction.East:
                                if (labelling[x, y + 1] == -1)
                                {
                                    labelling[x, y] = label++;
                                    labelling[x, y + 1] = labelling[x, y];
                                }
                                else
                                {
                                    labelling[x, y] = labelling[x, y + 1];
                                }
                                break;
                            case Direction.West:
                                if (labelling[x, y - 1] == -1)
                                {
                                    labelling[x, y] = label++;
                                    labelling[x, y - 1] = labelling[x, y];
                                }
                                else
                                {
                                    if (directions.IsOutFlowing)
                                    {
                                        labelling[x, y] = labelling[x, y - 1];
                                    }
                                    else
                                    {
                                        labelling[x, y] = label++;
                                    }
                                    
                                }
                                break;
                            case Direction.None:
                                break;
                            default:
                                break;
                        }
                    }
                }

                for (int x = 0; x < H; x++)
                {
                    for (int y = 0; y < W; y++)
                    {
                        Console.Write("{0} ", (char)labelling[x, y]);
                    }
                    Console.WriteLine();
                }
            }
        }

        private void ProcessPuzzle()
        {

            int i = 0;
            int cases = Convert.ToInt32(input[i]);
            for (int c = 0; c < cases; c++)
            {
                var hw = input[++i].Split(new char[] { ' ' });
                int H = Convert.ToInt32(hw[0]);
                int W = Convert.ToInt32(hw[1]);
                Map map = new Map(H, W, input, ref i);

                map.ProcessAltitudes();
            }
        }
    }
}

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

        private void ProcessPuzzle()
        {
            
        }
    }
}

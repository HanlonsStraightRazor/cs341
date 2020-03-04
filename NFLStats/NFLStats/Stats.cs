using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLStats
{
    public class Stats
    {
        public int PassingYds { get; set; }
        public int PassingTds { get; set; }
        public int RushingYds { get; set; }
        public int RushingTds { get; set; }
        public int RecYds { get; set; }
        public int RecTds { get; set; }
        public int Catches { get; set; }
        public int Fumbles{ get; set; }
        public int Interceptions { get; set; }

        public Stats()
        {

        }

        public Stats(int passingYds, int passingTds, int rushingYds, int rushingTds, int recYds, int recTds, int catches, int fumbles, int interceptions)
        {
            PassingYds = passingYds;
            PassingTds = passingTds;
            RushingYds = rushingYds;
            RushingTds = rushingTds;
            RecYds = recYds;
            RecTds = recTds;
            Catches = catches;
            Fumbles = fumbles;
            Interceptions = interceptions;
        }
    }
}

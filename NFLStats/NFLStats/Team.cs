using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLStats
{
    public class Team
    {
        public string Name { get; set; }
        public int Interceptions { get; set; }
        public int Sacks { get; set; }
        public int Points { get; set; }
        public int TD { get; set; }

        public Team(string name, int interceptions, int sacks, int points, int tD)
        {
            Name = name;
            Interceptions = interceptions;
            Sacks = sacks;
            Points = points;
            TD = tD;
        }

        public Team()
        {
        }
    }
}

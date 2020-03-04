using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLStats
{
    public class Game
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public int Score1 { get; set; }
        public int Score2 { get; set; }
        public int Week { get; set; }
        public int Id { get; set; }

        public Game(string team1, string team2, int score1, int score2, int week, int id)
        {
            Team1 = team1;
            Team2 = team2;
            Score1 = score1;
            Score2 = score2;
            Week = week;
            Id = id;
        }
    }
}

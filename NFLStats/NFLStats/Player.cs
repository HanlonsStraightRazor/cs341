using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLStats
{
    public class Player
    {
        public string Name { get; set; }
        public string Team { get; set; }
        public string Position { get; set; }
        public int Id { get; set; }
        public Stats SeasonStats { get; set; }

        public Player()
        {
        }
        public Player(string newName, string newTeam, string newPostion, int newId, Stats newStats)
        {
            Name = newName;
            Team = newTeam;
            Position = newPostion;
            Id = newId;
            SeasonStats = newStats;
        }
        public Boolean HasPassStats()
        {
            if(this.SeasonStats.PassingYds == 0 && this.SeasonStats.PassingTds == 0 && this.SeasonStats.Interceptions == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean HasRushingStats()
        {
            if (this.SeasonStats.RushingYds == 0 && this.SeasonStats.RushingTds == 0 && this.SeasonStats.Fumbles == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Boolean HasReceivingStats()
        {
            if (this.SeasonStats.RecYds == 0 && this.SeasonStats.RecYds == 0 && this.SeasonStats.Catches == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}

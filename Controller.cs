using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NFLStats
{
    class Controller
    {
        Database db = new Database();

        //sorts the players by name we can make it sort it some other way
        /*if key equals:
         * 1 Name
         * 2 Team
         * 3 Position
         * 4 PassingYds
         * 5 PassingTds
         * 6 RushingYds
         * 7 RushingTds
         * 8 RecYds
         * 9 RecTds
         * 10 Catches
         * 11 Fumbles
         * 12 Interceptions
        */
        public List<Player> PlayerSort(int key)
        {
            Player[] temp = db.GetPlayers();
            var play = new List<Player>();
            //adds all the players into the array list
            foreach(Player pl in temp)
            {
                play.Add(pl);
            }
            switch (key)
            {
                //sorts by Name
                case 1:
                    Comparison<Player> compOne = (x, y) => x.Name.CompareTo(y.Name);
                    play.Sort(compOne);
                    break;
                //sorts by Team
                case 2:
                    Comparison<Player> compTwo = (x, y) => x.Team.CompareTo(y.Team);
                    play.Sort(compTwo);
                    break;
                //sorts by Position
                case 3:
                    Comparison<Player> compThree = (x, y) => x.Position.CompareTo(y.Position);
                    play.Sort(compThree);
                    break;
                //sorts by PassingYds
                case 4:
                    Comparison<Player> compFour = (x, y) => x.SeasonStats.PassingYds.CompareTo(y.SeasonStats.PassingYds);
                    play.Sort(compFour);
                    break;
                //sorts by PassingTds
                case 5:
                    Comparison<Player> compFive = (x, y) => x.SeasonStats.PassingTds.CompareTo(y.SeasonStats.PassingTds);
                    play.Sort(compFive);
                    break;
                //sorts by RushingYds
                case 6:
                    Comparison<Player> compSix = (x, y) => x.SeasonStats.RushingYds.CompareTo(y.SeasonStats.RushingYds);
                    play.Sort(compSix);
                    break;
                //sorts by RushingTds
                case 7:
                    Comparison<Player> compSeven = (x, y) => x.SeasonStats.RushingTds.CompareTo(y.SeasonStats.RushingTds);
                    play.Sort(compSeven);
                    break;
                //sorts by RecYds
                case 8:
                    Comparison<Player> compEight = (x, y) => x.SeasonStats.RecYds.CompareTo(y.SeasonStats.RecYds);
                    play.Sort(compEight);
                    break;
                //sorts by RecTds
                case 9:
                    Comparison<Player> compNine = (x, y) => x.SeasonStats.RecTds.CompareTo(y.SeasonStats.RecTds);
                    play.Sort(compNine);
                    break;
                //sorts by Catches
                case 10:
                    Comparison<Player> compTen = (x, y) => x.SeasonStats.Catches.CompareTo(y.SeasonStats.Catches);
                    play.Sort(compTen);
                    break;
                //sorts by Fumbles
                case 11:
                    Comparison<Player> compEleven = (x, y) => x.SeasonStats.Fumbles.CompareTo(y.SeasonStats.Fumbles);
                    play.Sort(compEleven);
                    break;
                //sorts by Interceptions
                case 12:
                    Comparison<Player> compTwelve = (x, y) => x.SeasonStats.Interceptions.CompareTo(y.SeasonStats.Interceptions);
                    play.Sort(compTwelve);
                    break;
            }
            return play;
        }

        //passes a new player into the database
        public void AddPlayer(Player p)
        {
            db.LoadPlayers(p);
        }

        //deletes defence
        public void DeleteDefence()
        {
            db.DeleteDenfense();
        }

        //updates a player in the database
        public void UpdateSeasonStats(int id, Stats s)
        {
            db.updateSeasonStats(id, s);
        }

        //checks if a player exists
        public Boolean CheckIfExists(int id)
        {
            return db.CheckIfExists(id);
        }


    }
}

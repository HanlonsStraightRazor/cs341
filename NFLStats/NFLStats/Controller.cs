using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace NFLStats
{
    public class Controller
    {
        IDataBase db = new Database();
        //test
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
        public List<Team> TeamSort(int key)
        {
            Team[] temp = db.GetTeams();
            var team = new List<Team>();
            foreach (Team tm in temp)
            {
                team.Add(tm);
            }
            switch (key)
            {
                case 1:
                    Comparison<Team> compOne = (x, y) => x.Name.CompareTo(y.Name);
                    team.Sort(compOne);
                    break;
                case 2:
                    Comparison<Team> compTwo = (x, y) => x.Interceptions.CompareTo(y.Interceptions);
                    team.Sort(compTwo);
                    break;
                case 3:
                    Comparison<Team> compThree = (x, y) => x.Sacks.CompareTo(y.Sacks);
                    team.Sort(compThree);
                    break;
                case 4:
                    Comparison<Team> compFour = (x, y) => x.Points.CompareTo(y.Points);
                    team.Sort(compFour);
                    break;
                case 5:
                    Comparison<Team> compFive = (x, y) => x.TD.CompareTo(y.TD);
                    break;
            }
            return team;
        }

        public List<Player> PlayerSort(int key)
        {
            Player[] temp = db.GetPlayers();
            var play = new List<Player>();
            //adds all the players into the array list
            foreach (Player pl in temp)
            {
                if (pl.HasPassStats() || pl.HasReceivingStats() || pl.HasRushingStats())
                {
                    play.Add(pl);
                }
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

        //checks if a player exists
        public Boolean CheckIfExists(int id)
        {
            return db.CheckIfExists(id);
        }

        //returns a singular player
        public Player GetPlayer(string name)
        {
            return db.GetPlayer(name);
        }
        public Boolean CheckLoginInfo(string username, int passHash) //validates login info
        {
            return db.CheckLoginInfo(username, passHash);
        }
        public Boolean UserExists(string registerUser) //checks if the requested username exists
        {
            return db.UserExists(registerUser);
        }

        public void AddUser(string username, int pashHash, string favTeam) //add valid user to db
        {
            db.AddUser(username, pashHash, favTeam);
        }
        public void DelUser(string username) //add valid user to db
        {
            db.DelUser(username);
        }

        public Team[] GetTeamsForRegister() //returns list of teams for the register page drop down menu
        {
            return db.GetTeams();
        }
        public void UpdateUserPassword(User user, string newPass)
        { //whoever calls this from settings must check for password validity as in Register.xaml.cs
            db.UpdateUserPassword(user, newPass); //do not hash password yet like Register does
        }

        public void UpdateUserTeam(User user, string newPass)
        {
            db.UpdateUserTeam(user, newPass);
        }

        public bool TeamExists(string team)
        {
            return db.TeamExists(team);
        }

        public int GetUserPasswordHash(User user) //returns the hash code of the password for this user
        {
            return db.GetUserPasswordHash(user);
        }

        public string GetFavTeam(string username)
        {
            return db.GetFavTeam(username);
        }

        public Game[] GetScores(int week)
        {
            Game[] weekGames = new Game[16];
            Game[] games = db.GetScores();
            int index = 0;
            for(int i = 0; i < games.Length; i++)
            {
                if(games[i].Week == week)
                {
                    weekGames[index] = games[i];
                    index++;
                }
            }
            return weekGames;
        }

        public Player[] GetFavoritePlayers(string team, int week)
        {
            Player[] favs = new Player[3];
            Player temp = db.GetPlayerId(team, "QB", "`Passing Yards`");
            temp.SeasonStats = db.GetWeeklyStats(week,temp.Id);
            favs[0] = temp;
            temp = db.GetPlayerId(team, "RB", "`Rushing Yards`");
            temp.SeasonStats = db.GetWeeklyStats(week, temp.Id);
            favs[1] = temp;
            temp = db.GetPlayerId(team, "WR", "`Receiving Yards`");
            temp.SeasonStats = db.GetWeeklyStats(week, temp.Id);
            favs[2] = temp;
            return favs;
        }
    }
}
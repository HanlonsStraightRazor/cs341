using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFLStats
{
    interface IDataBase
    {
        void LoadPlayers(Player p);

        void UpdateSeasonStats(int id, Stats s);

        Boolean CheckIfExists(int id);

        Player[] GetPlayers();

        Game[] GetScores();

        void LoadTeams(Team t);

        Team[] GetTeams();

        Player GetPlayer(string name);

        void UpdateTeams(Team t);

        Boolean CheckLoginInfo(string username, int passHash);

        Boolean UserExists(string username);

        void AddUser(string username, int passHash, string favTeam);

        void DelUser(string username);

        void UpdateUserPassword(User user, string newPass);

        void UpdateUserTeam(User user, string newTeam);

        int GetUserPasswordHash(User user);

        string GetFavTeam(string username);

        bool TeamExists(string team);

        Player GetPlayerId(string team, string pos,string stat);

        Stats GetWeeklyStats(int week, int id);
    }
}

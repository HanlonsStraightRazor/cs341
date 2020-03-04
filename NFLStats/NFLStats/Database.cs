using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace NFLStats
{
    class Database : IDataBase
    {
        private string connectionStringToDB = ConfigurationManager.ConnectionStrings["MySQLDB"].ConnectionString;
        private Player p;
        private Team t;
        private Stats s;
        Game g;

        public void LoadPlayers(Player p)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            string line = "INSERT INTO Player VALUES('" + p.Name + "', '" + p.Position + "', '" + p.Team + "', '" + p.Id + "')";
            MySqlCommand cmd = new MySqlCommand(line, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }
        public void LoadTeams(Team t)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            string line = $"INSERT INTO `Team` VALUES('{t.Name}','{t.Interceptions}','{t.Sacks}','{t.Points}','{t.TD}')";
            MySqlCommand cmd = new MySqlCommand(line, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }

        public void UpdateTeams(Team t)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            string line = $"UPDATE `Team` SET `Interceptions` = {t.Interceptions}, `Sacks` = {t.Sacks}, `Points` = {t.Points}, `TD` = {t.TD} WHERE `Team` = \"{t.Name}\"";
            MySqlCommand cmd = new MySqlCommand(line, conn);
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public async void UpdateSeasonStats(int id, Stats s)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            string line = "UPDATE Player SET `Passing Yards` = '" + s.PassingYds + "', `Passing TD` = '" + s.PassingTds + "', `Rushing Yards` = '" + s.RushingYds + "', `Rushing TD` = '" + s.RushingTds + "', `Receiving Yards` = '" + s.RecYds + "', `Receiving TD` = '" + s.RecTds + "', `Catches` = '" + s.Catches + "', `Fumbles` = '" + s.Fumbles + "', `Interceptions` = '" + s.Interceptions + "' WHERE `Id` = '" + id + "'";
            MySqlCommand cmd = new MySqlCommand(line, conn);
            await cmd.ExecuteNonQueryAsync();

            conn.Close();
        }
        public Boolean CheckIfExists(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            string line = "SELECT COUNT(*) from Player where Id  = '" + id + "'";
            conn.Open();
            MySqlCommand len = new MySqlCommand(line, conn);
            int length = Convert.ToInt32(len.ExecuteScalar());
            conn.Close();
            if (length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public Player[] GetPlayers()
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand len = new MySqlCommand("SELECT COUNT(*) from Player", conn);
            int length = Convert.ToInt32(len.ExecuteScalar());
            MySqlCommand cmd = new MySqlCommand("SELECT * from Player", conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            int count = reader.FieldCount;
            Player[] list = new Player[length];
            int i = 0;
            while (reader.Read())
            {
                s = new Stats((int)reader.GetValue(4), (int)reader.GetValue(5), (int)reader.GetValue(6), (int)reader.GetValue(7), (int)reader.GetValue(8), (int)reader.GetValue(9), (int)reader.GetValue(10), (int)reader.GetValue(11), (int)reader.GetValue(12));
                p = new Player(reader.GetValue(0).ToString(), reader.GetValue(2).ToString(), reader.GetValue(1).ToString(), (int)reader.GetValue(3), s);
                list[i] = p;
                i++;
            }
            conn.Close();


            return list;
        }
        public Team[] GetTeams()
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand len = new MySqlCommand("SELECT COUNT(*) from Team", conn);
            int length = Convert.ToInt32(len.ExecuteScalar());
            MySqlCommand cmd = new MySqlCommand("SELECT * from Team", conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            int count = reader.FieldCount;
            Team[] list = new Team[length];
            int i = 0;
            while (reader.Read())
            {              
                t = new Team(reader.GetValue(0).ToString(), (int)reader.GetValue(1), (int)reader.GetValue(2), (int)reader.GetValue(3), (int)reader.GetValue(4));
                list[i] = t;
                i++;
            }
            conn.Close();


            return list;
        }

        public void UpdateGame(string team1, string team2, int score1, int score2, int week, int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            string line = $"DELETE FROM `Game` WHERE ID = {id}";
            MySqlCommand cmd = new MySqlCommand(line, conn);
            cmd.ExecuteNonQuery();
            line = "INSERT INTO Game VALUES('" + team1 + "', '" + score1 + "', '" + team2 + "', '" + score2 + "', '" + week + "', '" + id + "')";
            cmd = new MySqlCommand(line, conn);
            cmd.ExecuteNonQuery();

            conn.Close();

        }
        public Game[] GetScores()
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand len = new MySqlCommand("SELECT COUNT(*) from Game", conn);
            int length = Convert.ToInt32(len.ExecuteScalar());
            MySqlCommand cmd = new MySqlCommand("SELECT * from Game", conn);

            MySqlDataReader reader = cmd.ExecuteReader();
            int count = reader.FieldCount;
            Game[] list = new Game[length];
            int i = 0;
            while (reader.Read())
            {             
                g = new Game(reader.GetValue(0).ToString(), reader.GetValue(2).ToString(), (int)reader.GetValue(1), (int)reader.GetValue(3), (int)reader.GetValue(4), (int)reader.GetValue(5));
                list[i] = g;
                i++;
            }
            conn.Close();


            return list;
        }

        public void UpdateWeeklyStats(int week, int Id, Stats s)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand len = new MySqlCommand("SELECT COUNT(*) from WeeklyStats WHERE Id = " + Id + " AND Week = " + week, conn);
            int length = Convert.ToInt32(len.ExecuteScalar());
            string line;
            if (length == 0)
            {
                line = $"INSERT into WeeklyStats Values({week}, {Id}, {s.PassingYds}, {s.PassingTds}, {s.RushingYds}, {s.RushingTds}, {s.RecYds}, {s.RecTds}, {s.Fumbles}, {s.Interceptions}, {s.Catches})";
            }
            else
            {
                line = $"UPDATE WeeklyStats SET `Passing Yards` = '" + s.PassingYds + "', `Passing TD` = '" + s.PassingTds + "', `Rushing Yards` = '" + s.RushingYds + "', `Rushing TD` = '" + s.RushingTds + "', `Receiving Yards` = '" + s.RecYds + "', `Receiving TD` = '" + s.RecTds + "', `Catches` = '" + s.Catches + "', `Fumbles` = '" + s.Fumbles + "', `Interceptions` = '" + s.Interceptions + "' WHERE `Id` = '" + Id + $" AND Week = {week}'";
            }
            MySqlCommand cmd = new MySqlCommand(line, conn);
            cmd.ExecuteNonQueryAsync();

            conn.Close();
        }
        public Stats GetWeeklyStats(int week, int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();         
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM WeeklyStats WHERE Week = @week AND Id = @id", conn);
            cmd.Parameters.Add(new MySqlParameter("week", week));
            cmd.Parameters.Add(new MySqlParameter("id", id));

            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                s = new Stats((int)reader.GetValue(2), (int)reader.GetValue(3), (int)reader.GetValue(4), (int)reader.GetValue(5), (int)reader.GetValue(6), (int)reader.GetValue(7), (int)reader.GetValue(10), (int)reader.GetValue(8), (int)reader.GetValue(9));
                conn.Close();
                return s;
            }
            else
            {
                return new Stats(0,0,0,0,0,0,0,0,0);
            }
        }
        public Player GetPlayer(string name)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from Player WHERE Name = @input", conn);
            cmd.Parameters.Add(new MySqlParameter("input", name));
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            s = new Stats((int)reader.GetValue(4), (int)reader.GetValue(5), (int)reader.GetValue(6), (int)reader.GetValue(7), (int)reader.GetValue(8), (int)reader.GetValue(9), (int)reader.GetValue(10), (int)reader.GetValue(11), (int)reader.GetValue(12));
            p = new Player(reader.GetValue(0).ToString(), reader.GetValue(2).ToString(), reader.GetValue(1).ToString(), (int)reader.GetValue(3), s);
           conn.Close();
            return p;
        }
        public Player GetPlayerId(string team, string pos, string stat)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand($"SELECT `Id`, `Name` FROM `Player` WHERE {stat} = (SELECT MAX({stat}) FROM `Player` WHERE `Team` = '{team}') AND `Team` = '{team}'", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            Player p = new Player();
            p.Name = reader.GetString(1);
            p.Id = reader.GetInt16(0);
            reader.Close();
            conn.Close();
            return p;
        }

        public Boolean CheckLoginInfo(string username, int passHash) //validates login info
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from User", conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string dataUsers = reader.GetValue(0).ToString();
                int dataPass = (int)reader.GetValue(1);
                if (dataUsers.Equals(username) && dataPass.Equals(passHash))
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }
        
        public Boolean UserExists(string username)//checks if usernames exist when user registers
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * from User",conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                string dataUsers = reader.GetValue(0).ToString();
                if (dataUsers.Equals(username))
                {
                    return true;
                }
            }
            conn.Close();
            return false;
        }

        public void AddUser(string username, int pashHash, string favTeam) //adds a valid register user
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("INSERT INTO User VALUES ('" + username + "', '" + pashHash +"', '" + favTeam + "')",conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void DelUser(string username) //deletes a valid registered user
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("DELETE FROM `User` WHERE Username=@username", conn);
            cmd.Parameters.Add(new MySqlParameter("username", username));
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void UpdateUserPassword(User user, string newPass) //user can update password from settings
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();

            int hash = newPass.GetHashCode();
            user.PasswordHash = hash;
            MySqlCommand cmd = new MySqlCommand("UPDATE User SET Password=@pass WHERE Username=@username", conn);
            cmd.Parameters.Add(new MySqlParameter("pass", hash));
            cmd.Parameters.Add(new MySqlParameter("username", user.Username));
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public void UpdateUserTeam(User user, string newTeam) //user can update password from settings
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            
            MySqlCommand cmd = new MySqlCommand("UPDATE User SET FavoriteTeam=@newTeam WHERE Username=@username", conn);
            cmd.Parameters.Add(new MySqlParameter("newTeam", newTeam));
            cmd.Parameters.Add(new MySqlParameter("username", user.Username));
            cmd.ExecuteNonQuery();

            conn.Close();
        }

        public int GetUserPasswordHash(User user) //method to access password hash
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();

            int hashcode = 0;
            MySqlCommand cmd = new MySqlCommand("SELECT * from User WHERE Username=@username", conn);
            cmd.Parameters.Add(new MySqlParameter("username", user.Username));
            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            hashcode = (int)reader.GetValue(1);

            conn.Close();
            return hashcode;
        }

        public string GetFavTeam(string username)
        {
            string favTeam;

            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand("SELECT * from User WHERE Username=@username", conn);
            cmd.Parameters.Add(new MySqlParameter("username", username));
            MySqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            favTeam = reader.GetValue(2).ToString();

            conn.Close();

            return favTeam;
        }

        public bool TeamExists(string team)
        {
            MySqlConnection conn = new MySqlConnection(connectionStringToDB);
            conn.Open();
            string line = "SELECT COUNT(*) from Team where Team=@team";
            MySqlCommand cmd = new MySqlCommand(line, conn);
            cmd.Parameters.Add(new MySqlParameter("team", team));
            int length = Convert.ToInt32(cmd.ExecuteScalar());
            conn.Close();
            if (length > 0)
            {
                return true;
            }
            return false;
        }
    }

}
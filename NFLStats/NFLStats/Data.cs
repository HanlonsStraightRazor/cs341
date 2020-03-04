using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NFLStats
{
    class Data
    {
        private string url = "https://api.sleeper.app/v1/stats/nfl/regular/2019";
        private string urlScores = "http://www.nfl.com/liveupdate/scorestrip/ss.json";

        private void LoadPlayers()
        {

            var client = new WebClient();
            var response = client.DownloadString("https://api.sleeper.app/v1/players/nfl");

            System.Console.Write(response);

            JObject o = JObject.Parse(response);
            Player p = new Player();
            JToken x = o.First;

            while (x.Next != null) {
                try
                {
                    string id = x.Path;
                    p.Id = Int32.Parse(id);
                    string lastName = (string)o[id]["last_name"];
                    string firstName = (string)o[id]["first_name"];
                    p.Name = firstName + " " + lastName;
                    p.Position = (string)o[id]["position"];
                    p.Team = (string)o[id]["team"];

                    IDataBase d = new Database();
                    d.LoadPlayers(p);
                    x = x.Next;
                }
                catch(Exception)
                {
                    x = x.Next;
                }
            }
        }
        public void UpdateSeasonStats()
        {
            var client = new WebClient();
            var response = client.DownloadString(url);

            JObject o = JObject.Parse(response);
            Stats s = new Stats();
            JToken x = o.First;           
            while (x.Next != null)
            {
                try
                {
                    /*Task.Run(() =>*/ UpdateSeasonStatsHelper(o, x, s);
                }
                catch (Exception)
                {
                    x = x.Next;
                }
                x = x.Next;
            }

        }
        public void UpdateSeasonStatsHelper(JObject o, JToken x, Stats s)
        {
            Database d = new Database();
            string id = x.Path;
            int Id = Int32.Parse(id);
            if (d.CheckIfExists(Id))
            {


                if(o[id]["pass_yd"] != null) { s.PassingYds = (int)o[id]["pass_yd"]; } else { s.PassingYds = 0; }
                if(o[id]["pass_td"] != null) { s.PassingTds = (int)o[id]["pass_td"]; } else { s.PassingTds = 0; }
                if(o[id]["rush_yd"] != null) { s.RushingYds = (int)o[id]["rush_yd"]; } else { s.RushingYds = 0; }
                if(o[id]["rush_td"] != null) { s.RushingTds = (int)o[id]["rush_td"]; } else { s.RushingTds = 0; }
                if(o[id]["rec_yd"] != null) { s.RecYds = (int)o[id]["rec_yd"]; } else { s.RecYds = 0; }
                if(o[id]["rec_td"] != null) { s.RecTds = (int)o[id]["rec_td"]; } else { s.RecTds = 0; }
                if(o[id]["fum"] != null) { s.Fumbles = (int)o[id]["fum"]; } else { s.Fumbles = 0; }
                if(o[id]["pass_int"] != null) { s.Interceptions = (int)o[id]["pass_int"]; } else { s.Interceptions = 0; }
                if(o[id]["rec"] != null) { s.Catches = (int)o[id]["rec"]; } else { s.Catches = 0; }

                d.UpdateSeasonStats(Id, s);
            }
        }
        public void LoadTeams()
        {
            var client = new WebClient();
            var response = client.DownloadString("https://api.sleeper.app/v1/players/nfl");

            System.Console.Write(response);

            JObject o = JObject.Parse(response);
            Team t;
            JToken x = o.First;
            IDataBase d = new Database();
            int test;
            while (x.Next != null)
            {
                try
                {
                    string id = x.Path;
                    if (!int.TryParse(id, out test))
                    {
                        t = new Team(id, 0, 0, 0, 0);
                        d.LoadTeams(t);
                    }
                    x = x.Next;
                }
                catch (Exception)
                {
                    x = x.Next;
                }
            }
        }
        public void UpdateTeams()
        {
            var client = new WebClient();
            var response = client.DownloadString(url);

            JObject o = JObject.Parse(response);
            Team t = new Team();
            JToken x = o.First;
            IDataBase d = new Database();
            int test;

            while (x.Next != null)
            {
                try
                {
                    string id = x.Path;
                    if (!int.TryParse(id, out test))
                    {
                        t.Name = id;
                        try { t.Interceptions = (int)o[id]["int"]; } catch (Exception) { t.Interceptions = 0; }
                        try { t.Sacks = (int)o[id]["sack"]; } catch (Exception) { t.Sacks = 0; }
                        try { t.Points = (int)o[id]["pts_allow"]; } catch (Exception) { t.Points = 0; }
                        try { t.TD = (int)o[id]["def_td"]; } catch (Exception) { t.TD = 0; }

                        d.UpdateTeams(t);
                    }
                    x = x.Next;
                } 
                catch(Exception)
                {
                   x = x.Next;
                }
            }
        }
        public void UpdateLiveScores()
        {
            var client = new WebClient();
            var response = client.DownloadString(urlScores);

            JObject o = JObject.Parse(response);
            //Stats s = new Stats();
            JToken x = o.First.Next.First.First;
            int week = (int)o["w"];
            Database d = new Database();
            while (x != null)
            {
                try
                {
                    string Team1 = x["h"].ToString();
                    int Team1Score = (int)x["hs"];
                    string Team2 = x["v"].ToString();
                    int Team2Score = (int)x["vs"];
                    int id = (int)x["gsis"];
                    d.UpdateGame(Team1,Team2,Team1Score,Team2Score,week,id);
                    x = x.Next;
                }
                catch (Exception)
                {

                }
            }

        }

        public void UpdateWeeklyStats(int week, int id)
        {
            string weekUrl = "https://api.sleeper.app/v1/stats/nfl/regular/2019/" + week;

            var client = new WebClient();
            var response = client.DownloadString(weekUrl);

            JObject o = JObject.Parse(response);

            Stats s = new Stats();
            JToken x = o.First;            
            while (x.Next != null)
            {
                if (x.Path.ToString() == id.ToString())
                {
                    try
                    {
                        UpdateWeeklyStatsHelper(o, x, s, week, id);
                    }
                    catch (Exception)
                    {
                        x = x.Next;
                    }
                }
                x = x.Next;
            }



        }
        public void UpdateWeeklyStatsHelper(JObject o, JToken x, Stats s, int week, int playerId)
        {
            Database d = new Database();
            string id = x.Path;
            int Id = Int32.Parse(id);
            if (d.CheckIfExists(Id))
            {


                if (o[id]["pass_yd"] != null) { s.PassingYds = (int)o[id]["pass_yd"]; } else { s.PassingYds = 0; }
                if (o[id]["pass_td"] != null) { s.PassingTds = (int)o[id]["pass_td"]; } else { s.PassingTds = 0; }
                if (o[id]["rush_yd"] != null) { s.RushingYds = (int)o[id]["rush_yd"]; } else { s.RushingYds = 0; }
                if (o[id]["rush_td"] != null) { s.RushingTds = (int)o[id]["rush_td"]; } else { s.RushingTds = 0; }
                if (o[id]["rec_yd"] != null) { s.RecYds = (int)o[id]["rec_yd"]; } else { s.RecYds = 0; }
                if (o[id]["rec_td"] != null) { s.RecTds = (int)o[id]["rec_td"]; } else { s.RecTds = 0; }
                if (o[id]["fum"] != null) { s.Fumbles = (int)o[id]["fum"]; } else { s.Fumbles = 0; }
                if (o[id]["pass_int"] != null) { s.Interceptions = (int)o[id]["pass_int"]; } else { s.Interceptions = 0; }
                if (o[id]["rec"] != null) { s.Catches = (int)o[id]["rec"]; } else { s.Catches = 0; }

                d.UpdateWeeklyStats(week, Id, s);

            }
        }
        public int GetWeek()
        {
            var client = new WebClient();
            var response = client.DownloadString(urlScores);

            JObject o = JObject.Parse(response);
            JToken x = o.First.Next.First.First;
            int week = (int)o["w"];

            return week;
        }
    }
}

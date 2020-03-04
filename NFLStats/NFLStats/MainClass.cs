using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NFLStats
{
    public class MainClass
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Data d = new Data();
            Task.Run(() => d.UpdateLiveScores());
            if (DateTime.Now.DayOfWeek.ToString() == "Tuesday")
            {
                Task.Run(() => d.UpdateSeasonStats());
                Task.Run(() => d.UpdateTeams());
            }          
            var app = new App();
            app.Run(new LoginPage());
        }
    }
}

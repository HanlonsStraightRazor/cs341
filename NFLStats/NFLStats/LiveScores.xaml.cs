using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace NFLStats
{
    /// <summary>
    /// Interaction logic for LiveScores.xaml
    /// </summary>
    public partial class LiveScores : Window
    {
        Game[] games;
        Controller controller = new Controller();
        Data data = new Data();
        public LiveScores()
        {
            int week = data.GetWeek();
            games = controller.GetScores(week); 
            InitializeComponent();
            GameScore[] scores = new GameScore[games.Length];
            for (int i = 0; i < games.Length; i++)
            {
                string score = games[i].Score1 + "-" + games[i].Score2;
                GameScore temp = new GameScore(games[i].Team1, games[i].Team2,score);
                panel.Children.Add(temp);              
            }        
            InitializeComponent();
            /*DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += timer_Tick;
            timer.Start();*/
        }
        void timer_Tick(object sender, EventArgs e)
        {
            data.UpdateLiveScores();
            int week = data.GetWeek();
            games = controller.GetScores(week);
            GameScore[] scores = new GameScore[games.Length];
            for (int i = 0; i < games.Length; i++)
            {
                string score = games[i].Score1 + "-" + games[i].Score2;
                GameScore temp = new GameScore(games[i].Team1, games[i].Team2, score);
                panel.Children.RemoveAt(i);
                panel.Children.Insert(i,temp);
            }
            InitializeComponent();
        }

    }
}

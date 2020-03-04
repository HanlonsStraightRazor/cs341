using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Primary.xaml
    /// </summary>
    public partial class Primary : Window
    {
        Controller controller;
        Data data = new Data();
        Game[] games;
        int x = 0;
        int game = 0;
        Player[] p, p2;
        public User us { get; set; }
        int week;
        public bool IsClosed { get; private set; }
        System.Timers.Timer timer = new System.Timers.Timer(30000);
        private System.Timers.Timer timer2 = new System.Timers.Timer(10000);

        public Primary(User use, Controller controller)
        {
            this.controller = controller;
            us = use;
            week = data.GetWeek();
            games = controller.GetScores(week);
            InitializeComponent();
            GameScore[] scores = new GameScore[games.Length];
            //InitializeComponent();
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].Team1.Equals(us.FavTeam) || games[i].Team2.Equals(us.FavTeam))
                {
                    string score = games[i].Score1 + "-" + games[i].Score2;
                    GameScore temp = new GameScore(games[i].Team1, games[i].Team2, score);
                    panel.Children.Add(temp);
                    game = i;
                    break;
                }
            }
            //InitializeComponent();
            p = controller.GetFavoritePlayers(games[game].Team1, week);
            p2 = controller.GetFavoritePlayers(games[game].Team2, week);
            data.UpdateWeeklyStats(week, p[x].Id);
            data.UpdateWeeklyStats(week, p2[x].Id);
            p = controller.GetFavoritePlayers(games[game].Team1, week);
            p2 = controller.GetFavoritePlayers(games[game].Team2, week);
            player1.Content = p[x].Name;
            yd1.Text += " " + p[x].SeasonStats.PassingYds.ToString();
            td1.Text += " " + p[x].SeasonStats.PassingTds.ToString();
            misc1.Text += " " + p[x].SeasonStats.Interceptions.ToString();
            player2.Content = p2[x].Name;
            yd2.Text += "Passing Yards: " + p2[x].SeasonStats.PassingYds.ToString();
            td2.Text += "Touchdowns: " + p2[x].SeasonStats.PassingTds.ToString();
            misc2.Text += "Interceptions: " + p2[x].SeasonStats.Interceptions.ToString();
            x++;
            timer2.Elapsed += timer2_Tick;
            timer2.AutoReset = true;
            timer2.Enabled = true;
            timer.Elapsed += timer2_Tick;
            timer.AutoReset = true;
            timer.Enabled = true;           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            data.UpdateWeeklyStats(week, p[x].Id);
            data.UpdateWeeklyStats(week, p2[x].Id);
            p = controller.GetFavoritePlayers(games[game].Team1, week);
            p2 = controller.GetFavoritePlayers(games[game].Team2, week);
            Dispatcher.BeginInvoke((Action)delegate ()
            {
                if (x == 0)
                {
                    player1.Content = p[x].Name;
                    yd1.Text = "Passing Yards: " + p[x].SeasonStats.PassingYds.ToString();
                    td1.Text = "Touchdowns: " + p[x].SeasonStats.PassingTds.ToString();
                    misc1.Text = "Interceptions: " + p[x].SeasonStats.Interceptions.ToString();
                    player2.Content = p2[x].Name;
                    yd2.Text = "Passing Yards: " + p2[x].SeasonStats.PassingYds.ToString();
                    td2.Text = "Touchdowns: " + p2[x].SeasonStats.PassingTds.ToString();
                    misc2.Text = "Interceptions: " + p2[x].SeasonStats.Interceptions.ToString();
                    x++;
                }
                else if (x == 1)
                {
                    player1.Content = p[x].Name;
                    yd1.Text = "Rushing Yards: " + p[x].SeasonStats.RushingYds.ToString();
                    td1.Text = "Touchdowns: " + p[x].SeasonStats.RushingTds.ToString();
                    misc1.Text = "Fumbles: " + p[x].SeasonStats.Fumbles.ToString();
                    player2.Content = p2[x].Name;
                    yd2.Text = "Rushing Yards: " + p2[x].SeasonStats.RushingYds.ToString();
                    td2.Text = "Touchdowns: " + p2[x].SeasonStats.RushingTds.ToString();
                    misc2.Text = "Fumbles: " + p2[x].SeasonStats.Fumbles.ToString();
                    x++;
                }
                else if (x == 2)
                {
                    player1.Content = p[x].Name;
                    yd1.Text = "Receiving Yards: " + p[x].SeasonStats.RecYds.ToString();
                    td1.Text = "Touchdowns: " + p[x].SeasonStats.RecTds.ToString();
                    misc1.Text = "Catches: " + p[x].SeasonStats.Catches.ToString();
                    player2.Content = p2[x].Name;
                    yd2.Text = "Receiving Yards: " + p2[x].SeasonStats.RecYds.ToString();
                    td2.Text = "Touchdowns: " + p2[x].SeasonStats.RecTds.ToString();
                    misc2.Text = "Catches: " + p2[x].SeasonStats.Catches.ToString();
                    x = 0;
                }
            });
            if(IsClosed)
            {
                timer2.Enabled = false;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            data.UpdateLiveScores();
            int week = data.GetWeek();
            games = controller.GetScores(week);
            GameScore[] scores = new GameScore[games.Length];
            for (int i = 0; i < games.Length; i++)
            {
                if (games[i].Team1.Equals(us.FavTeam) || games[i].Team2.Equals(us.FavTeam))
                {
                    string score = games[i].Score1 + "-" + games[i].Score2;
                    GameScore temp = new GameScore(games[i].Team1, games[i].Team2, score);
                    panel.Children.Add(temp);
                    break;
                }
            }
            InitializeComponent();
            if (IsClosed)
            {
                timer2.Enabled = false;
            }
        }


        private void DisplayPlayerStats(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                MainWindow mainWindow = new MainWindow(controller);
                mainWindow.Show();

                mainWindow.Closed += (sender2, e2) =>
                    mainWindow.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        private void DisplayAllScores(object sender, RoutedEventArgs e)
        {
            Thread t = new Thread(() =>
            {
                LiveScores temp = new LiveScores();
                temp.Show();

                temp.Closed += (sender2, e2) =>
                    temp.Dispatcher.InvokeShutdown();

                System.Windows.Threading.Dispatcher.Run();
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }


        private void DisplaySettings(object sender, RoutedEventArgs e)
        {
            SettingsWindow setting = new SettingsWindow(this, controller);
            setting.Show();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myCanvas.Width = e.NewSize.Width;
            myCanvas.Height = e.NewSize.Height;

            double xChange = 1, yChange = 1;

            if (e.PreviousSize.Width != 0)
                xChange = (e.NewSize.Width / e.PreviousSize.Width);

            if (e.PreviousSize.Height != 0)
                yChange = (e.NewSize.Height / e.PreviousSize.Height);

            ScaleTransform scale = new ScaleTransform(myCanvas.LayoutTransform.Value.M11 * xChange, myCanvas.LayoutTransform.Value.M22 * yChange);
            myCanvas.LayoutTransform = scale;
            myCanvas.UpdateLayout();

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }
    }
}

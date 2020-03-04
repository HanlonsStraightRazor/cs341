using System;
using System.Collections.Generic;
using System.Windows;

namespace NFLStats
{
    /// <summary>
    /// The main window of the application.
    /// Displays tables of player stats.
    /// </summary>
    public partial class MainWindow : Window
    {
        Controller controller;
        public MainWindow(Controller controller) 
        {
            InitializeComponent();
            this.controller = controller;
        }

        /// <summary>
        /// Each method below displays the appropriate
        /// table that correponds to each MainWindow button.
        /// The only exception is the DisplayCompare method.
        /// That method displays the compare window.
        /// </summary>

        private void DisplayPassingStats(object sender, RoutedEventArgs e)
        {
            List<object> stats = new List<object>();
            if (searchBar.Text.Equals(""))
            {
                List<Player> players = controller.PlayerSort(4);
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    int passingYds = players[i].SeasonStats.PassingYds;
                    int passingTds = players[i].SeasonStats.PassingTds;
                    int interceptions = players[i].SeasonStats.Interceptions;
                    if (passingYds != 0 || passingTds != 0 || interceptions != 0)
                    {
                        stats.Add(new PassingStats(players[i].Name, passingYds, passingTds, interceptions));
                    }
                }
            }
            else
            {
                try
                {
                    Player player = controller.GetPlayer(searchBar.Text);
                    stats.Add(new PassingStats(player.Name, player.SeasonStats.PassingYds,
                        player.SeasonStats.PassingTds, player.SeasonStats.Interceptions));
                }
                catch (Exception)
                {
                    MessageBox.Show("Player not found", "Error");
                }
            }
            DataContext = new DataGridTable(stats);
        }

        private void DisplayRushingStats(object sender, RoutedEventArgs e)
        {
            List<object> stats = new List<object>();
            if (searchBar.Text.Equals(""))
            {
                List<Player> players = controller.PlayerSort(6);
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    int rushingYds = players[i].SeasonStats.RushingYds;
                    int rushingTds = players[i].SeasonStats.RushingTds;
                    int fumbles = players[i].SeasonStats.Fumbles;
                    if (rushingYds != 0 || rushingTds != 0 || fumbles != 0)
                    {
                        stats.Add(new RushingStats(players[i].Name, rushingYds, rushingTds, fumbles));
                    }
                }
            }
            else
            {
                try
                {
                    Player player = controller.GetPlayer(searchBar.Text);
                    stats.Add(new RushingStats(player.Name, player.SeasonStats.RushingYds,
                        player.SeasonStats.RushingTds, player.SeasonStats.Fumbles));
                }
                catch (Exception)
                {
                    MessageBox.Show("Player not found", "Error");
                }
            }
            DataContext = new DataGridTable(stats);
        }

        private void DisplayReceivingStats(object sender, RoutedEventArgs e)
        {
            List<object> stats = new List<object>();
            if (searchBar.Text.Equals(""))
            {
                List<Player> players = controller.PlayerSort(8);
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    int recYds = players[i].SeasonStats.RecYds;
                    int recTds = players[i].SeasonStats.RecTds;
                    int catches = players[i].SeasonStats.Catches;
                    if (recYds != 0 || recTds != 0 || catches != 0)
                    {
                        stats.Add(new ReceivingStats(players[i].Name, recYds, recTds, catches));
                    }
                }
            }
            else
            {
                try
                {
                    Player player = controller.GetPlayer(searchBar.Text);
                    stats.Add(new ReceivingStats(player.Name, player.SeasonStats.RecYds,
                        player.SeasonStats.RecTds, player.SeasonStats.Catches));
                }
                catch (Exception)
                {
                    MessageBox.Show("Player not found", "Error");
                }
            }
            DataContext = new DataGridTable(stats);
        }

        private void DisplayDefenseStats(object sender, RoutedEventArgs e)
        {
            List<Team> teams = controller.TeamSort(1);
            List<object> stats = new List<object>();
            bool foundSearch = false;
            if (searchBar.Text.Equals(""))
            {
                foundSearch = true;
                foreach (Team team in teams)
                {
                    int interceptions = team.Interceptions;
                    int sacks = team.Sacks;
                    int points = team.Points;
                    int tds = team.TD;
                    if (interceptions != 0 || sacks != 0 || points != 0 || tds != 0)
                    {
                        stats.Add(new DefenseStats(team.Name, interceptions, sacks, points, tds));
                    }
                }
            }
            else
            {
                foreach (Team team in teams)
                {
                    if (string.Equals(searchBar.Text, team.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        int interceptions = team.Interceptions;
                        int sacks = team.Sacks;
                        int points = team.Points;
                        int tds = team.TD;
                        stats.Add(new DefenseStats(team.Name, interceptions, sacks, points, tds));
                        foundSearch = true;
                        break;
                    }
                }

            }
            if (foundSearch)
            {
                DataContext = new DataGridTable(stats);
            }
            else
            {
                MessageBox.Show("Team not found", "Error");
            }
        }

        //Displays the compare window
        private void DisplayCompare(object sender, RoutedEventArgs e)
        {
            CompareWindow comp = new CompareWindow();
            comp.Show();
        }

        //Exit the window
        private void ExitStats(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    /// <summary>
    /// Below are just wrapper classes for
    /// data to be displayed in the data grid table.
    /// </summary>

    public class PassingStats
    {
        public string Name { get; set; }
        public int PassingYds { get; set; }
        public int PassingTds { get; set; }
        public int Interceptions { get; set; }
        public PassingStats(string name, int yds, int tds, int inter)
        {
            Name = name;
            PassingYds = yds;
            PassingTds = tds;
            Interceptions = inter;
        }
    }
    public class RushingStats
    {
        public string Name { get; set; }
        public int RushingYds { get; set; }
        public int RushingTds { get; set; }
        public int Fumbles { get; set; }
        public RushingStats(string name, int yds, int tds, int fum)
        {
            Name = name;
            RushingYds = yds;
            RushingTds = tds;
            Fumbles = fum;
        }
    }
    public class ReceivingStats
    {
        public string Name { get; set; }
        public int ReceivingYds { get; set; }
        public int ReceivingTds { get; set; }
        public int Catches { get; set; }
        public ReceivingStats(string name, int yds, int tds, int catches)
        {
            Name = name;
            ReceivingYds = yds;
            ReceivingTds = tds;
            Catches = catches;
        }
    }
    public class DefenseStats
    {
        public string Name { get; set; }
        public int Interceptions { get; set; }
        public int Sacks { get; set; }
        public int Points { get; set; }
        public int Tds { get; set; }
        public DefenseStats(string name, int ints, int sacks, int points, int tds)
        {
            Name = name;
            Interceptions = ints;
            Sacks = sacks;
            Points = points;
            Tds = tds;
        }
    }
}

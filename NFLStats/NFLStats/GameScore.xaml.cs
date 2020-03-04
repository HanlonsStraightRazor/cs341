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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NFLStats
{
    /// <summary>
    /// Interaction logic for GameScore.xaml
    /// </summary>
    public partial class GameScore : UserControl
    {

        public GameScore(string team1, string team2, string score)
        {
            InitializeComponent();
            team1Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/NFLStats;component/Resources/" + team1 + ".ico"));
            team2Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/NFLStats;component/Resources/" + team2 + ".ico"));
            Score.Content = score;
        }
        public GameScore()
        {
            /*Image team1Logo = new Image();
            Image team2Logo = new Image();
            team1Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/NFLStats;component/Resources/GB.ico"));
            team2Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/NFLStats;component/Resources/SF.ico"));*/


            InitializeComponent();
        }

        private void UpdateScore(String scores)
        {
            Score.Content = scores;
        }

        private void Team1Logo_Initialized(object sender, EventArgs e)
        {
            team1Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/NFLStats;component/Resources/GB.ico"));
        }

        private void Team2Logo_Initialized(object sender, EventArgs e)
        {
            team2Logo.Source = new BitmapImage(new Uri(@"pack://application:,,,/NFLStats;component/Resources/LA.ico"));
        }
    }
}

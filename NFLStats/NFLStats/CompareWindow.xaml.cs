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
using System.Collections;

namespace NFLStats
{
    public partial class CompareWindow : Window
    {
        Controller controller;
        List<Player> compareList;
        int countTemp = 0;
        public CompareWindow()
        {
            InitializeComponent();
            controller = new Controller();
            compareList = new List<Player>();
        }

        public class TextboxText
        {
            public string textdata { get; set; }

        }

        private void AddPlayer(object sender, RoutedEventArgs e)
        {
            try
            {
                Header1.DataContext = new TextboxText() { textdata = "Player" };
                Player p = controller.GetPlayer(searchBar.Text);
                //adds the player to the list
                if (compareList.Count < 5)
                {
                    compareList.Add(p);
                    countTemp++;
                    if (countTemp == 1)
                    {
                        OneA.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).Name };
                        Btn1.Visibility = System.Windows.Visibility.Visible;
                    }
                    else if (countTemp == 2)
                    {
                        TwoA.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).Name };
                        Btn2.Visibility = System.Windows.Visibility.Visible;
                    }
                    else if (countTemp == 3)
                    {
                        ThreeA.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).Name };
                        Btn3.Visibility = System.Windows.Visibility.Visible;
                    }
                    else if (countTemp == 4)
                    {
                        FourA.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).Name };
                        Btn4.Visibility = System.Windows.Visibility.Visible;
                    }
                    else if (countTemp == 5)
                    {
                        FiveA.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).Name };
                        Btn5.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    MessageBox.Show("Can only compare 5 players at a time.");
                }
                Clear();
            }
            catch(Exception)
            {
                MessageBox.Show("Not a valid player name.");
            }
            
        }

        private void DeletePlayer(object sender, RoutedEventArgs e)
        {
            int indexAt = -1;
            if (sender.Equals(Btn1))
            {
                indexAt = 0;
            } else if (sender.Equals(Btn2))
            {
                indexAt = 1;
            } else if (sender.Equals(Btn3))
            {
                indexAt = 2;
            }
            else if (sender.Equals(Btn4))
            {
                indexAt = 3;
            }
            else if (sender.Equals(Btn5))
            {
                indexAt = 4;
            }
            compareList.RemoveAt(indexAt);
            countTemp--;
            //resets all the names displayed
            OneA.DataContext = new TextboxText() { textdata = " " };
            TwoA.DataContext = new TextboxText() { textdata = " " };
            ThreeA.DataContext = new TextboxText() { textdata = " " };
            FourA.DataContext = new TextboxText() { textdata = " " };
            FiveA.DataContext = new TextboxText() { textdata = " " };
            Btn1.Visibility = Visibility.Hidden;
            Btn2.Visibility = Visibility.Hidden;
            Btn3.Visibility = Visibility.Hidden;
            Btn4.Visibility = Visibility.Hidden;
            Btn5.Visibility = Visibility.Hidden;

            int count = 1;
            //re populates each name into the correct slot
            foreach (Player item in compareList)
            {
                if (count == 1)
                {
                    OneA.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).Name };
                    Btn1.Visibility = System.Windows.Visibility.Visible;
                }
                else if (count == 2)
                {
                    TwoA.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).Name };
                    Btn2.Visibility = System.Windows.Visibility.Visible;
                }
                else if (count == 3)
                {
                    ThreeA.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).Name };
                    Btn3.Visibility = System.Windows.Visibility.Visible;
                }
                else if (count == 4)
                {
                    FourA.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).Name };
                    Btn4.Visibility = System.Windows.Visibility.Visible;
                }
                else if (count == 5)
                {
                    FiveA.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).Name };
                    Btn5.Visibility = System.Windows.Visibility.Visible;
                }
                count++;
            }
            Clear();

        }

        private void Passing(object sender, RoutedEventArgs e)
        {
            Header2.DataContext = new TextboxText() { textdata = "Passing Yds" };
            Header3.DataContext = new TextboxText() { textdata = "Passing Tds" };
            Header4.DataContext = new TextboxText() { textdata = "Interceptions" };
            int count = 1;
            foreach (Player item in compareList)
            {
                if (count == 1)
                {
                    OneA.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).Name };
                    OneB.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.PassingYds.ToString() };
                    OneC.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.PassingTds.ToString() };
                    OneD.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.Interceptions.ToString() };
                }
                else if (count == 2)
                {
                    TwoA.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).Name };
                    TwoB.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.PassingYds.ToString() };
                    TwoC.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.PassingTds.ToString() };
                    TwoD.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.Interceptions.ToString() };
                }
                else if (count == 3)
                {
                    ThreeA.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).Name };
                    ThreeB.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.PassingYds.ToString() };
                    ThreeC.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.PassingTds.ToString() };
                    ThreeD.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.Interceptions.ToString() };
                }
                else if (count == 4)
                {
                    FourA.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).Name };
                    FourB.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.PassingYds.ToString() };
                    FourC.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.PassingTds.ToString() };
                    FourD.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.Interceptions.ToString() };
                }
                else if (count == 5)
                {
                    FiveA.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).Name };
                    FiveB.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.PassingYds.ToString() };
                    FiveC.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.PassingTds.ToString() };
                    FiveD.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.Interceptions.ToString() };
                }
                count++;
            }
        }

        private void Rushing(object sender, RoutedEventArgs e)
        {
            Header2.DataContext = new TextboxText() { textdata = "Rushing Yds" };
            Header3.DataContext = new TextboxText() { textdata = "Rushing Tds" };
            Header4.DataContext = new TextboxText() { textdata = "Fumbles" };
            int count = 1;
            foreach (Player item in compareList)
            {
                if (count == 1)
                {
                    OneA.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).Name };
                    OneB.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.RushingYds.ToString() };
                    OneC.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.RushingTds.ToString() };
                    OneD.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.Fumbles.ToString() };
                }
                else if (count == 2)
                {
                    TwoA.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).Name };
                    TwoB.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.RushingYds.ToString() };
                    TwoC.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.RushingTds.ToString() };
                    TwoD.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.Fumbles.ToString() };
                }
                else if (count == 3)
                {
                    ThreeA.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).Name };
                    ThreeB.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.RushingYds.ToString() };
                    ThreeC.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.RushingTds.ToString() };
                    ThreeD.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.Fumbles.ToString() };
                }
                else if (count == 4)
                {
                    FourA.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).Name };
                    FourB.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.RushingYds.ToString() };
                    FourC.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.RushingTds.ToString() };
                    FourD.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.Fumbles.ToString() };
                }
                else if (count == 5)
                {
                    FiveA.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).Name };
                    FiveB.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.RushingYds.ToString() };
                    FiveC.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.RushingTds.ToString() };
                    FiveD.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.Fumbles.ToString() };
                }
                count++;
            }
        }

        private void Receiving(object sender, RoutedEventArgs e)
        {
            Header2.DataContext = new TextboxText() { textdata = "Rec Yds" };
            Header3.DataContext = new TextboxText() { textdata = "Rec Tds" };
            Header4.DataContext = new TextboxText() { textdata = "Catches" };
            int count = 1;
            foreach (Player item in compareList)
            {
                if(count == 1)
                {
                    OneA.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).Name};
                    OneB.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.RecYds.ToString()};
                    OneC.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.RecTds.ToString() };
                    OneD.DataContext = new TextboxText() { textdata = compareList.ElementAt(0).SeasonStats.Catches.ToString() };
                }
                else if (count == 2)
                {
                    TwoA.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).Name };
                    TwoB.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.RecYds.ToString() };
                    TwoC.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.RecTds.ToString() };
                    TwoD.DataContext = new TextboxText() { textdata = compareList.ElementAt(1).SeasonStats.Catches.ToString() };
                }
                else if (count == 3)
                {
                    ThreeA.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).Name };
                    ThreeB.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.RecYds.ToString() };
                    ThreeC.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.RecTds.ToString() };
                    ThreeD.DataContext = new TextboxText() { textdata = compareList.ElementAt(2).SeasonStats.Catches.ToString() };
                }
                else if (count == 4)
                {
                    FourA.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).Name };
                    FourB.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.RecYds.ToString() };
                    FourC.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.RecTds.ToString() };
                    FourD.DataContext = new TextboxText() { textdata = compareList.ElementAt(3).SeasonStats.Catches.ToString() };
                }
                else if (count == 5)
                {
                    FiveA.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).Name };
                    FiveB.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.RecYds.ToString() };
                    FiveC.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.RecTds.ToString() };
                    FiveD.DataContext = new TextboxText() { textdata = compareList.ElementAt(4).SeasonStats.Catches.ToString() };
                }
                count++;
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //clears the tables
        private void Clear()
        {
            Header2.DataContext = new TextboxText() { textdata = " " };
            Header3.DataContext = new TextboxText() { textdata = " " };
            Header4.DataContext = new TextboxText() { textdata = " " };

            OneB.DataContext = new TextboxText() { textdata = " " };
            OneC.DataContext = new TextboxText() { textdata = " " };
            OneD.DataContext = new TextboxText() { textdata = " " };

            TwoB.DataContext = new TextboxText() { textdata = " " };
            TwoC.DataContext = new TextboxText() { textdata = " " };
            TwoD.DataContext = new TextboxText() { textdata = " " };

            ThreeB.DataContext = new TextboxText() { textdata = " " };
            ThreeC.DataContext = new TextboxText() { textdata = " " };
            ThreeD.DataContext = new TextboxText() { textdata = " " };

            FourB.DataContext = new TextboxText() { textdata = " " };
            FourC.DataContext = new TextboxText() { textdata = " " };
            FourD.DataContext = new TextboxText() { textdata = " " };

            FiveB.DataContext = new TextboxText() { textdata = " " };
            FiveC.DataContext = new TextboxText() { textdata = " " };
            FiveD.DataContext = new TextboxText() { textdata = " " };
        }

    }
}

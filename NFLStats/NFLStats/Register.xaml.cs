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

namespace NFLStats
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        Controller controller;
        public Register() //fills drop down menu for user to select favorite team
        {
            controller = new Controller();
            InitializeComponent();
            favTeamDropDown.Items.Insert(0, "Select Favorite Team");
            favTeamDropDown.SelectedIndex = 0;

            Team[] teams = controller.GetTeamsForRegister();
            for (int i = 1; i < 33; i++)
            {
                favTeamDropDown.Items.Insert(i, teams[i - 1].Name);
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)//validates password and existence of requested username
        {
            if (favTeamDropDown.SelectedIndex != 0)
            {
                string registerUser = UsernameBox.Text;
                string registerPass = PasswordBoxBox.Password;
                if (registerPass.Length >= 8)
                {
                    if (registerPass.Any(char.IsDigit))
                    {
                        int passHash = registerPass.GetHashCode();
                        string teamSelected = favTeamDropDown.SelectedValue.ToString();
                        if (controller.UserExists(registerUser) == false)
                        {
                            controller.AddUser(registerUser, passHash, teamSelected);
                            User user = new User(registerUser, passHash, teamSelected);
                            Primary prim = new Primary(user, controller);
                            prim.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Username Already Exists!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Password Does NOT Contain A Number!");
                    }
                }
                else
                {
                    MessageBox.Show("Password Must Be At Least 8 Characters!");
                }
            }
            else
            {
                MessageBox.Show("Favorite Team is Not Selected!");
            }
        }

        private void BackClick(object sender, RoutedEventArgs e) //go back to login page
        {
            LoginPage log = new LoginPage();
            log.Show();
            this.Close();
        }
    }
}

using System.Linq;
using System.Windows;
using Microsoft.VisualBasic;

namespace NFLStats
{
    /// <summary>
    /// Tweaks user preferences and provides
    /// exit and log out buttons.
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private Window parent;
        private Controller controller;
        public SettingsWindow(Window parent, Controller controller)
        {
            InitializeComponent();
            this.parent = parent;
            this.controller = controller;
        }

        private void ChangeFavoriteTeam(object sender, RoutedEventArgs e)
        {
            string newTeam = Interaction.InputBox("New Favorite Team", "Change Favorite Team", "", -1, -1);
            if (controller.TeamExists(newTeam))
            {
                controller.UpdateUserTeam(((Primary)parent).us, newTeam);
                MessageBox.Show("Changes will appear upon next login.", "Success");
            }
            else if (!newTeam.Equals(""))
            {
                MessageBox.Show("Team does not exist", "Error");
            }
        }

        private void ChangePassword(object sender, RoutedEventArgs e)
        {
            string newPass = Interaction.InputBox("New Password", "Change Password", "", -1, -1);
            if (newPass.Any(char.IsDigit))
            {
                controller.UpdateUserTeam(((Primary)parent).us, newPass);
                MessageBox.Show("Changes will appear upon next login.", "Success");
            }
            else if (!newPass.Equals(""))
            {
                MessageBox.Show("Password must contain a number", "Error");
            }
        }

        private void DeleteAccount(object sender, RoutedEventArgs e)
        {
            controller.DelUser(((Primary)parent).us.Username);
            LogOut(null, null);
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            parent.Close();
            new LoginPage().Show();
            Close();
        }

        private void ExitSettings(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

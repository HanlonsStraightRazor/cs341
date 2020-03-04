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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        Controller controller;
        Boolean canLogIn = true;
        public LoginPage()
        {
            controller = new Controller();
            InitializeComponent();
        }

        private void LogInClick(object sender, RoutedEventArgs e)//opens program if valid login
        {
            try
            {
                if (canLogIn)
                {
                    Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                    string loginUser = UsernameBox.Text;
                    string loginPass = PasswordBoxBox.Password;
                    int passHash = loginPass.GetHashCode();
                    if (controller.CheckLoginInfo(loginUser, passHash))
                    {
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
                        string favTeam = controller.GetFavTeam(loginUser);
                        User user = new User(loginUser, passHash, favTeam);
                        Primary prim = new Primary(user, controller);
                        prim.Show();
                        Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
                        this.Close();
                    }
                    else
                    {

                        MessageBox.Show("Username or Password is Incorrect!");
                        canLogIn = false;
                        DispatcherTimer timer = new DispatcherTimer();
                        timer.Interval = TimeSpan.FromSeconds(10);
                        timer.Tick += timer_Tick;
                        timer.Start();
                    }
                }
                else
                {
                    MessageBox.Show("Incorrect Login Triggered 10 Second Delay!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to sign in!");
                Mouse.OverrideCursor = System.Windows.Input.Cursors.Arrow;
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            canLogIn = true;
        }

        private void RegisterClick(object sender, RoutedEventArgs e) //opens register window for new user
        {
            Register reg = new Register();
            reg.Show();
            this.Close();
        }
        private void PasswordBoxBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                LogInClick(this, new RoutedEventArgs());
            }
        }
        private void UsernameBox_GotFocus(object sender, RoutedEventArgs e)
        {
            UsernameBox.Text = "";
            UsernameBox.GotFocus -= UsernameBox_GotFocus;
        }
    }
}

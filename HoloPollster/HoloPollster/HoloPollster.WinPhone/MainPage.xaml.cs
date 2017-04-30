using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace HoloPollster.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
        //int count = 1;
        static public LoginData userdata;
        static public AllPollsCreated polls;
        static public string Username;
        static public string Password;
        Cloud cloud;
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            polls = new AllPollsCreated();
            Button.IsEnabled = false;
            userdata = new LoginData();
            cloud = new Cloud();

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.



            
        }

        private void newUser_Tapped(object sender, RoutedEventArgs e)
        {
            NewUserPopUp.IsOpen = true;
        }

        private void ClosePopUp_Click(object sender, RoutedEventArgs e)
        {
            NewUserPopUp.IsOpen = false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            var result = await Cloud.UsernameRetrieveFromCloud(Createusername.Text, userdata);
            if (userdata.username != "default" || result != null)
            {
                CreateAccount.Content = "Username already in use";
            }
            else if (Createpassword.Password == Createpassword2.Password)
            {
                CreateAccount.IsEnabled = false;
                userdata.username = Createusername.Text;
                userdata.password = Createpassword.Password;
                await Cloud.UsernameUploadToCloudSerialized(userdata);
                this.Frame.Navigate(typeof(HomeScreen));
            }
            else CreateAccount.Content = "Passwords don't match";
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var result = await Cloud.UsernameRetrieveFromCloud(username.Text, userdata);
            if (userdata.username == "default" || result == null)
            {
                Button.Content = "Incorrect username";
            }
            else if (userdata.password != password.Password)
            {
                Button.Content = "Incorrect password";
            }
            else
            {
                Button.IsEnabled = false;
                userdata = new LoginData();
                userdata.username = username.Text;
                userdata.password = password.Password;
                this.Frame.Navigate(typeof(HomeScreen));
            }
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (password.Password.Length >= 1 && username.Text.Length >= 1)
            {
                Button.IsEnabled = true;
            }
            else Button.IsEnabled = false;
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9a-zA-Z]", "");
            textboxSender.SelectionStart = cursorPosition;
            if (password.Password.Length >= 1 && username.Text.Length >= 1)
            {
                Button.IsEnabled = true;
            }
            else Button.IsEnabled = false;
        }

        private void Createusername_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart;
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9a-zA-Z]", "");
            textboxSender.SelectionStart = cursorPosition;
            if (Createpassword.Password.Length >= 1 && Createpassword2.Password.Length >= 1 && Createusername.Text.Length >= 1)
            {
                CreateAccount.IsEnabled = true;
            }
            else CreateAccount.IsEnabled = false;
        }

        private void Createpassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Createpassword.Password.Length >= 1 && Createpassword2.Password.Length >= 1 && Createusername.Text.Length >= 1)
            {
                CreateAccount.IsEnabled = true;
            }
            else CreateAccount.IsEnabled = false;
        }

        private void Createpassword2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Createpassword.Password.Length >= 1 && Createpassword2.Password.Length >= 1 && Createusername.Text.Length >= 1)
            {
                CreateAccount.IsEnabled = true;
            }
            else CreateAccount.IsEnabled = false;
        }
    }
}

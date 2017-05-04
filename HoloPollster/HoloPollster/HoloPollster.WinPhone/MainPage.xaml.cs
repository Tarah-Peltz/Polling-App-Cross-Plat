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
        {//Initializes content
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
        { //Event handler to open up fields for creating a new account
            NewUserPopUp.IsOpen = true;
        }

        private void ClosePopUp_Click(object sender, RoutedEventArgs e)
        {
            //Event handler for button that closes popup
            NewUserPopUp.IsOpen = false;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //The event handler for creating a new account
            var result = await Cloud.UsernameRetrieveFromCloud(Createusername.Text, userdata); 
            //Fetch data from cloud to check for collisions
            if (userdata.username != "default" || result != null)
            {
                //result would be null if a match is found. This shows a match is found so username is in use
                CreateAccount.Content = "Username already in use";
            }
            else if (Createpassword.Password == Createpassword2.Password)
            {
                //Their passwords match and the username is not in use. Allow account creation
                CreateAccount.IsEnabled = false; //Ensures user can't click button twice
                userdata.username = Createusername.Text;
                userdata.password = Createpassword.Password;
                await Cloud.UsernameUploadToCloudSerialized(userdata); 
                //Uploads user data to cloud. Default values are set in the classes
                this.Frame.Navigate(typeof(HomeScreen)); //Takes us to home screen
            }
            else CreateAccount.Content = "Passwords don't match"; //User didn't type the same password twice
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //The event handler for the buttn allowing users to login
            var result = await Cloud.UsernameRetrieveFromCloud(username.Text, userdata);
            if (userdata.username == "default" || result == null)
            { //result is null if a match isn't found. If no match is found, user provided incorrect username
                Button.Content = "Incorrect username";
            }
            else if (userdata.password != password.Password)
            { //User provided incorrect password
                Button.Content = "Incorrect password";
            }
            else
            {
                Button.IsEnabled = false; //This ensures a user doesn't double click the button
                userdata = new LoginData(); 
                //Creates local instance of LoginData, which we need in order to update user statistics to cloud later
                userdata.username = username.Text;
                userdata.password = password.Password;
                this.Frame.Navigate(typeof(HomeScreen)); //Navigates to the homescreen
            }
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        { //Event handler for when user types in the password field on the login screen
            if (password.Password.Length >= 1 && username.Text.Length >= 1)
            { //Ensures user can't submit a null field
                Button.IsEnabled = true; //Enables login button
            }
            else Button.IsEnabled = false;
        }

        private void username_TextChanged(object sender, TextChangedEventArgs e)
        { //Event handler for when user types in the username field
            var textboxSender = (TextBox)sender;
            var cursorPosition = textboxSender.SelectionStart; //Stores cursor position
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9a-zA-Z]", ""); //Ensures user only inputs alphanumerics
            textboxSender.SelectionStart = cursorPosition; //Restores cursor position
            if (password.Password.Length >= 1 && username.Text.Length >= 1)
            {
                Button.IsEnabled = true; //Ensures user can't login with null fields
            }
            else Button.IsEnabled = false;
        }

        private void Createusername_TextChanged(object sender, TextChangedEventArgs e)
        { //Event handler for when the user types in the create username field
            var textboxSender = (TextBox)sender; 
            var cursorPosition = textboxSender.SelectionStart; //Stores cursor position
            textboxSender.Text = Regex.Replace(textboxSender.Text, "[^0-9a-zA-Z]", ""); //Restricts username to alphanumerics
            textboxSender.SelectionStart = cursorPosition; //Restores cursor position after regex check for alphanumerics
            if (Createpassword.Password.Length >= 1 && Createpassword2.Password.Length >= 1 && Createusername.Text.Length >= 1)
            {
                CreateAccount.IsEnabled = true; //Ensures user can't create account with null fields
            }
            else CreateAccount.IsEnabled = false;
        }

        private void Createpassword_PasswordChanged(object sender, RoutedEventArgs e)
        { //Event handler for user typing in password1 creation field
            if (Createpassword.Password.Length >= 1 && Createpassword2.Password.Length >= 1 && Createusername.Text.Length >= 1)
            { //Ensures user can't try to submit a null field
                CreateAccount.IsEnabled = true;
            }
            else CreateAccount.IsEnabled = false;
        }

        private void Createpassword2_PasswordChanged(object sender, RoutedEventArgs e)
        { //Event handler for user typing in password2 creation field
            if (Createpassword.Password.Length >= 1 && Createpassword2.Password.Length >= 1 && Createusername.Text.Length >= 1)
            {//Ensures user can't try to submit a null field
                CreateAccount.IsEnabled = true;
            }
            else CreateAccount.IsEnabled = false;
        }
    }
}

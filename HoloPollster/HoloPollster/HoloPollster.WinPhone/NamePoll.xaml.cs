using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HoloPollster.WinPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NamePoll : Page
    {
        public PollsWithMetaData newPoll;
        private Cloud cloud;
        public NamePoll()
        {
            newPoll = new PollsWithMetaData();
            this.InitializeComponent();
            cloud = new Cloud();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            MainPage.userdata.pollsCreated += 1; //Gives user credit for creating a poll
            Cloud.UsernameUploadToCloudSerialized(MainPage.userdata); //uploads updated user data to cloud
            button.IsEnabled = false;
            newPoll.questions = MakeAPoll.questions; //Initializes values of newpoll
            newPoll.PollCreator = MainPage.userdata.username;
            newPoll.CreationTime = DateTime.Now;
            newPoll.PollName = textbox.Text;
            //no longer needed since we pull all polls from the cloud
            //MainPage.polls.CreatedPolls.Add(newPoll);
            await Cloud.UploadPollToCloudSerialized(newPoll); //Uploads new poll to cloud
            this.Frame.Navigate(typeof(HomeScreen)); //navigates home
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomeScreen));
        }
    }
}

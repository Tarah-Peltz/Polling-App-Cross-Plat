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
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            polls = new AllPollsCreated();


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
            Button.Click += delegate
            {
                userdata = new LoginData();
                /*polls = new AllPollsCreated();
                PollData demoq = new PollData();
                List<PollData> sampleList = new List<PollData>();
                PollsWithMetaData sample = new PollsWithMetaData();

                demoq.QuestionText = "This is a sample quiz";
                demoq.type = PollData.AnswerType.TextBox;
                sampleList.Add(demoq);
                sample.questions = sampleList;
                sample.CreationTime = DateTime.Now;
                sample.PollCreator = "Tarah";
                polls.CreatedPolls.Add(sample);
                userdata.password = password.Password.ToString();
                userdata.username = username.Text.ToString();*/


                this.Frame.Navigate(typeof(HomeScreen));

                Frame.Navigate(typeof(HomeScreen));

            };

            
        }
    }
}

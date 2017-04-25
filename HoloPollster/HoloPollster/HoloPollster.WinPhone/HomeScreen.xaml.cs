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
    public sealed partial class HomeScreen : Page
    {
        Cloud cloud;
        public HomeScreen()
        {
            this.InitializeComponent();
            this.ViewModel = new LoginData();
            ViewModel.username = MainPage.userdata.username;
            ViewModel.password = MainPage.userdata.password;
            cloud = new Cloud();
            //block.Text = "Welcome, " + ViewModel.username;
        }

        public LoginData ViewModel { get; set; }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            await Cloud.RetrieveFromCloud(MainPage.polls);
        }



        private void MakeAPoll_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MakeAPoll));
            //Oops. Accidentally named MakeAPage at first. This calls MakeAPoll, which is MakeAPage
        }
        private async void TakeAPoll_Click(object sender, RoutedEventArgs e)
        {
            
            this.Frame.Navigate(typeof(PickAPoll));
        }

        private void MyStats_Click(object sender, RoutedEventArgs e)
        {

        }

        private void block_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class PickAPoll : Page
    {
        int rowIndex;
        static public string picked; //Allows us to track which poll was selected by user
        public PickAPoll()
        {
            rowIndex = 0;
            this.InitializeComponent();


            try {
                foreach (PollsWithMetaData poll in MainPage.polls.CreatedPolls) //Iterates through all polls created
                {
                    RowDefinition newRow = new RowDefinition(); //Adds a row for every poll created
                    rowIndex += 1;
                    newRow.Height = GridLength.Auto;
                    newRow.MinHeight = 30;

                    grid.RowDefinitions.Add(newRow);
                    var MyButton = new Button(); //Each row contains a button displaying the name of one of the polls created
                    MyButton.Content = poll.PollName;
                    MyButton.Click += navigation; //Subscribes to navigation event handler
                    Grid.SetRow(MyButton, rowIndex); //adds new row
                    grid.Children.Add(MyButton); //Adds button as child to new row

                }
            }
           catch { //Handles event wherein no polls have been created yet
                //We need a try/catch because we can't just check for null. It crashes the app
                var tex = new TextBlock();
                tex.FontSize = 24;
                tex.TextWrapping = TextWrapping.WrapWholeWords;
                tex.HorizontalAlignment = HorizontalAlignment.Center;
                tex.Text = "No Polls Created Yet!"; //Informs user that no polls have been created
                RowDefinition row = new RowDefinition(); //Adds information to screen in form of new gridrow
                grid.RowDefinitions.Add(row);
                Grid.SetRow(tex, 1);
                grid.Children.Add(tex);
            }

        }




        private void navigation(object sender, RoutedEventArgs e)
        { //Event handler for taking the selected poll

            var selection = sender as Button;
            picked = selection.Content.ToString();
            Frame.Navigate(typeof(TakeAPoll));
                
        }


    /// <summary>
    /// Invoked when this page is about to be displayed in a Frame.
    /// </summary>
    /// <param name="e">Event data that describes how this page was reached.
    /// This parameter is typically used to configure the page.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
    }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        { //Event handler for navigating backwards
            MainPage.polls.CreatedPolls.Clear();
            this.Frame.Navigate(typeof(HomeScreen));
        }
    }
  }



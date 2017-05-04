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
    public sealed partial class TakeAPoll : Page
    {

        int rowIndex;
        PollsWithMetaData pickedPoll;
        public TakeAPoll()
        {
            this.InitializeComponent();
            rowIndex = 0;
            foreach (PollsWithMetaData selected in MainPage.polls.CreatedPolls) //Iterates through all polls created
            {
                if (selected.PollName == PickAPoll.picked)
                { //This is the poll the user wants to take
                    pickedPoll = selected;
                }
            }

            foreach (PollData question in pickedPoll) //Iterates through all the questions contained in the selected poll
            {
                RowDefinition newRow = new RowDefinition();
                RowDefinition newRow2 = new RowDefinition(); 
                //Add a row for the button and a row for the textbox
                rowIndex += 2; 
                newRow.Height = GridLength.Auto;
                newRow.MinHeight = 30;
                newRow2.Height = GridLength.Auto;
                newRow2.MinHeight = 30; //Formatting of rows
                grid.RowDefinitions.Add(newRow);
                grid.RowDefinitions.Add(newRow2); //Add the new rows to the page
                TextBlock ques = new TextBlock(); //Text containing the question
                TextBox ans = new TextBox(); 
                ques.Text = question.QuestionText; //set text of question to the questions found in the selected poll
                ans.Text = "Your answer here";
                Grid.SetRow(ques, rowIndex - 2); //Puts question and answer in the proper rows
                Grid.SetRow(ans, rowIndex - 1); //We subtract one because the relevant buttons are in the last row
                grid.Children.Add(ques);
                grid.Children.Add(ans); // add the question and answer fields as children of the grid rows
            }
            StackPanel buttons = new StackPanel();
            Button submit = new Button(); //Buttons to submit answers to poll and to navigate back to poll selection screen
            Button back = new Button();
            buttons.Orientation = Orientation.Horizontal;
            submit.Content = "Submit Poll";
            submit.Click += submitPoll; //Subscribes to submitPoll event handler on click
            back.Content = "Back";
            back.Click += GoBack; //Subscribes to goBack event handler on click
            RowDefinition newRow3 = new RowDefinition(); //create new row
            grid.RowDefinitions.Add(newRow3); //add new row of buttons
            Grid.SetRow(buttons, rowIndex); //place buttons in the new row
            buttons.Children.Add(back);
            buttons.Children.Add(submit);//Make back and submit children of the stackpanel
            grid.Children.Add(buttons); //Make stackpanel child of the new row
        }

        private async void submitPoll(object sender, RoutedEventArgs e)
        { //Event handler forsubmitting a poll
            MainPage.polls.CreatedPolls.Clear();
            MainPage.userdata.pollsTaken += 1;
            await Cloud.UsernameUploadToCloudSerialized(MainPage.userdata); 
            //Gives user credit for taking poll and uploads updated info to cloud
            this.Frame.Navigate(typeof(HomeScreen)); //Navigates to home screen
        }


        private void GoBack(object sender, RoutedEventArgs e)
        { //Event handler for navigating backwards
            this.Frame.Navigate(typeof(PickAPoll)); //Navigates back a page
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
    }
}

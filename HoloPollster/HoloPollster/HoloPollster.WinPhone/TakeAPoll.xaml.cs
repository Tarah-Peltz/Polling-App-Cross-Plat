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
            foreach (PollsWithMetaData selected in MainPage.polls.CreatedPolls)
            {
                if (selected.PollName == PickAPoll.picked)
                {
                    pickedPoll = selected;
                }
            }

            foreach (PollData question in pickedPoll)
            {
                RowDefinition newRow = new RowDefinition();
                RowDefinition newRow2 = new RowDefinition();
                rowIndex += 2;
                newRow.Height = GridLength.Auto;
                newRow.MinHeight = 30;
                newRow2.Height = GridLength.Auto;
                newRow2.MinHeight = 30;
                grid.RowDefinitions.Add(newRow);
                grid.RowDefinitions.Add(newRow2);
                TextBlock ques = new TextBlock();
                TextBox ans = new TextBox();
                ques.Text = question.QuestionText;
                ans.Text = "Your answer here";
                Grid.SetRow(ques, rowIndex - 2);
                Grid.SetRow(ans, rowIndex - 1);
                grid.Children.Add(ques);
                grid.Children.Add(ans);
            }
            Button submit = new Button();
            submit.Content = "Submit Poll";
            submit.Click += submitPoll;
            RowDefinition newRow3 = new RowDefinition();
            grid.RowDefinitions.Add(newRow3);
            Grid.SetRow(submit, rowIndex);
            grid.Children.Add(submit);
        }

        private void submitPoll(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomeScreen));
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
